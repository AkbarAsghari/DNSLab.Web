using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using MudBlazor;
using System;
using System.Net;
using System.Text.Json;
using DNSLab.Web.Enums;

namespace DNSLab.Web.Exceptions
{
    public class HttpResponseExceptionHander(NavigationManager _NavigationManager, ISnackbar _Snackbar)
    {
        public async Task HandlerExceptionAsync(HttpResponseMessage httpResponseMessage)
        {
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                if (await CheckIsReponseExceptionHandler(httpResponseMessage))
                    return;

                var statusCode = httpResponseMessage.StatusCode;

                switch (statusCode)
                {

                    case HttpStatusCode.Forbidden:
                    case HttpStatusCode.BadRequest:
                    case HttpStatusCode.Conflict:
                        break;
                    case HttpStatusCode.TooManyRequests:
                        _Snackbar.Add("محدودیت تعداد درخواست", Severity.Info);
                        break;
                    case HttpStatusCode.Unauthorized:
                        _NavigationManager.NavigateTo($"/Accounts/Login", true);
                        break;
                    case HttpStatusCode.NotFound:
                        _NavigationManager.NavigateTo("/Errors/404", true);
                        break;
                    case HttpStatusCode.InternalServerError:
                        _NavigationManager.NavigateTo("/Errors/500", true);
                        break;
                    case HttpStatusCode.ServiceUnavailable:
                        _NavigationManager.NavigateTo("/Errors/503", true);
                        break;
                }
            }
        }

        private async Task<bool> CheckIsReponseExceptionHandler(HttpResponseMessage httpResponseMessage)
        {
            string content = await httpResponseMessage.Content!.ReadAsStringAsync();
            if (String.IsNullOrEmpty(content))
                return false;

            var error = JsonSerializer.Deserialize<Propblem>(content!, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (error != null)
            {
                _Snackbar.Add(message: error.Messages == null ? error.Detail : error.Messages[ExceptionLanguageEnum.Fa].ToString(),
                    severity: (error.Status >= 400 && error.Status < 500 ? Severity.Warning : error.Status >= 500 ? Severity.Error : Severity.Info));
                return true;
            }
            return false;
        }
    }

    class Propblem : ProblemDetails
    {
        public Dictionary<ExceptionLanguageEnum, string>? Messages { get; set; }
    }
}
