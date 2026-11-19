using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using DNSLab.Web.Interfaces.Providers;
using DNSLab.Web.Interfaces.Repositories;
using DNSLab.Web.API.Authentication.DTOs;

namespace DNSLab.Web.Providers
{
    public class JWTAuthenticationStateProvider : AuthenticationStateProvider, IAuthenticationProvider
    {
        private AuthenticationState Anonymouse =>
            new AuthenticationState(new ClaimsPrincipal());

        private readonly IHttpServiceProvider _ServiceProvider;
        private readonly ITokenProvider _TokenProvider;
        public JWTAuthenticationStateProvider(ITokenProvider tokenProvider, IHttpServiceProvider serviceProvider)
        {
            _ServiceProvider = serviceProvider;
            _TokenProvider = tokenProvider;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                await _ServiceProvider.CheckTokenAsync();
                var token = await _TokenProvider.GetTokenAsync();
                if (!String.IsNullOrEmpty(token))
                {
                    return BuildAuthenticationState(token);
                }
            }
            catch (Exception ex) { }

            return Anonymouse;
        }

        public AuthenticationState BuildAuthenticationState(string token)
        {
            var claims = ParsClaimsFromJWT(token);

            // Checks the exp field of the token
            var expiry = claims.Where(claim => claim.Type.Equals("exp")).FirstOrDefault();
            if (expiry == null)
                return Anonymouse;

            // The exp field is in Unix time
            var datetime = DateTimeOffset.FromUnixTimeSeconds(long.Parse(expiry.Value));
            if (datetime.UtcDateTime <= DateTime.UtcNow)
                return Anonymouse;

            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt")));
        }

        private IEnumerable<Claim> ParsClaimsFromJWT(string token)
        {
            var claims = new List<Claim>();
            var payload = token.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);

            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            keyValuePairs.TryGetValue("role", out object roles);

            if (roles != null)
            {
                if (roles.ToString().Trim().StartsWith("["))
                {
                    var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString());

                    foreach (var paresRole in parsedRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, paresRole));
                    }
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, roles.ToString()));
                }

                keyValuePairs.Remove(ClaimTypes.Role);
            }

            claims.Add(new Claim(ClaimTypes.Name, keyValuePairs["unique_name"].ToString()));
            claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));

            return claims;

        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            string converted = base64.Replace('-', '+').Replace('_', '/');

            switch (converted.Length % 4)
            {
                case 2: converted += "=="; break;
                case 3: converted += "="; break;
            }

            return Convert.FromBase64String(converted);
        }

        public async Task Login(AuthenticateResponse model)
        {
            try
            {
                await _TokenProvider.SetTokenAsync(model);
                var authState = BuildAuthenticationState(model.Token);
                NotifyAuthenticationStateChanged(Task.FromResult(authState));
            }
            catch { }
        }

        public async Task Logout()
        {
            try
            {
                await _TokenProvider.DeleteTokenAsync();
                NotifyAuthenticationStateChanged(Task.FromResult(Anonymouse));
            }
            catch { }
        }
    }
}
