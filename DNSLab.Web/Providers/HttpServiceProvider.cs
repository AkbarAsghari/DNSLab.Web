using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using DNSLab.Web.AppSettings;
using DNSLab.Web.Exceptions;
using DNSLab.Web.Interfaces.Providers;

namespace DNSLab.Web.Providers
{
    public class HttpServiceProvider(HttpClient _HttpClient, HttpResponseExceptionHander _HttpResponseExceptionHander, ITokenProvider _TokenProvider, IHttpContextAccessor _HttpContextAccessor) : IHttpServiceProvider
    {
        string _BaseUrl = GlobalSettings.APIBaseAddress;

        private async Task<T?> GenerateHttpResponseWraper<T>(HttpResponseMessage httpResponse)
        {
            if (!httpResponse.IsSuccessStatusCode)
            {
                await _HttpResponseExceptionHander.HandlerExceptionAsync(httpResponse);
                return default;
            }
            else
            {
                var responseString = await httpResponse.Content.ReadAsStringAsync();

                T? response;
                if (typeof(T) == typeof(String))
                {
                    response = (T)(object)responseString;
                }
                else
                {
                    response = JsonSerializer.Deserialize<T>(responseString,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                }

                return response;
            }
        }

        public async Task CheckTokenAsync()
        {
            var token = await _TokenProvider.GetTokenAsync();
            if (!String.IsNullOrEmpty(token))
            {
                _HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            }
            else
            {
                _HttpClient.DefaultRequestHeaders.Authorization = null;
            }
        }

        private void SetIpAddressAsync()
        {
            var context = _HttpContextAccessor.HttpContext;

            var ip = context?.Connection.RemoteIpAddress?.ToString();

            if (!string.IsNullOrWhiteSpace(ip))
            {
                _HttpClient.DefaultRequestHeaders.Remove("X-Real-IP");
                _HttpClient.DefaultRequestHeaders.Add("X-Real-IP", ip);
            }
        }

        public StringContent GenerateStringContentFromObject<T>(T data)
        {
            return new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
        }

        public async Task<TResponse?> Delete<TResponse>(string url)
        {
             SetIpAddressAsync();
            await CheckTokenAsync();
            return await GenerateHttpResponseWraper<TResponse>(await _HttpClient.DeleteAsync(_BaseUrl + url));
        }

        public async Task<TResponse?> Get<TResponse>(string url, bool checkToken = true)
        {
            SetIpAddressAsync();
            if (checkToken)
            {
                await CheckTokenAsync();
            }
            return await GenerateHttpResponseWraper<TResponse>(await _HttpClient.GetAsync(_BaseUrl + url));

        }

        public async Task<TResponse?> Post<T, TResponse>(string url, T data)
        {
            SetIpAddressAsync();
            await CheckTokenAsync();
            return await GenerateHttpResponseWraper<TResponse>(await _HttpClient.PostAsync(_BaseUrl + url, GenerateStringContentFromObject(data)));
        }

        public async Task<TResponse?> Post<TResponse>(string url)
        {
            SetIpAddressAsync();
            await CheckTokenAsync();
            return await GenerateHttpResponseWraper<TResponse>(await _HttpClient.PostAsync(_BaseUrl + url, null));
        }

        public async Task<TResponse?> Put<T, TResponse>(string url, T data)
        {
            SetIpAddressAsync();
            await CheckTokenAsync();
            return await GenerateHttpResponseWraper<TResponse>(await _HttpClient.PutAsync(_BaseUrl + url, GenerateStringContentFromObject(data)));
        }
        public async Task<TResponse?> Put<TResponse>(string url)
        {
            SetIpAddressAsync();
            await CheckTokenAsync();
            return await GenerateHttpResponseWraper<TResponse>(await _HttpClient.PutAsync(_BaseUrl + url, null));
        }
    }
}
