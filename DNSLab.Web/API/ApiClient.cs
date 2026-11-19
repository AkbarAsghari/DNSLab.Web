using DNSLab.Web.API.Abstractions;
using DNSLab.Web.API.Common.Result;
using DNSLab.Web.Interfaces.Providers;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;

namespace DNSLab.Web.API;

internal sealed class ApiClient(
    HttpClient httpClient,
    ITokenProvider accessTokenProvider) : IApiClient
{
    public Task<Result<TResponse>> PostAsync<TRequest, TResponse>(
        string endpoint,
        TRequest request,
        ApiRequestOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        return SendAsync<TResponse>(
            HttpMethod.Post,
            endpoint,
            options,
            cancellationToken,
            request);
    }

    public Task<Result<TResponse>> PutAsync<TRequest, TResponse>(
        string endpoint,
        TRequest request,
        ApiRequestOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        return SendAsync<TResponse>(
            HttpMethod.Put,
            endpoint,
            options,
            cancellationToken,
            request);
    }

    public async Task<Result> DeleteAsync(
        string endpoint,
        ApiRequestOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        using var request = await CreateRequestAsync(
            HttpMethod.Delete,
            endpoint,
            options,
            cancellationToken);

        using var response = await httpClient.SendAsync(
            request,
            cancellationToken);

        if (response.IsSuccessStatusCode)
            return Result.Success();

        return Result.Failure(
            await CreateErrorAsync(response, cancellationToken));
    }

    private async Task<Result<T>> SendAsync<T>(
        HttpMethod method,
        string endpoint,
        ApiRequestOptions? options,
        CancellationToken cancellationToken,
        object? body = null)
    {
        using var request = await CreateRequestAsync(
            method,
            endpoint,
            options,
            cancellationToken);

        if (body is not null)
            request.Content = JsonContent.Create(body);

        using var response = await httpClient.SendAsync(
            request,
            cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            return Result<T>.Failure(
                await CreateErrorAsync(response, cancellationToken));
        }

        if (response.StatusCode is HttpStatusCode.NoContent
            or HttpStatusCode.ResetContent)
        {
            return Result<T>.Failure(new Error(
                "EMPTY_RESPONSE",
                "The API returned an empty response."));
        }

        var value = await response.Content.ReadFromJsonAsync<Result<T>>(
            cancellationToken: cancellationToken);

        if (value is null)
        {
            return Result<T>.Failure(new Error(
                "EMPTY_RESPONSE",
                "The API returned an empty response."));
        }

        return value;
    }

    private async Task<Result> SendWithoutValueAsync(
       HttpMethod method,
       string endpoint,
       ApiRequestOptions? options,
       CancellationToken cancellationToken,
       object? body = null)
    {
        using var request = await CreateRequestAsync(
            method,
            endpoint,
            options,
            cancellationToken);

        if (body is not null)
            request.Content = JsonContent.Create(body);

        using var response = await httpClient.SendAsync(
            request,
            cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            return Result.Failure(
                await CreateErrorAsync(response, cancellationToken));
        }

        if (response.StatusCode is HttpStatusCode.NoContent
            or HttpStatusCode.ResetContent)
        {
            return Result.Failure(new Error(
                "EMPTY_RESPONSE",
                "The API returned an empty response."));
        }

        var value = await response.Content.ReadFromJsonAsync<Result>(
            cancellationToken: cancellationToken);

        if (value is null)
        {
            return Result.Failure(new Error(
                "EMPTY_RESPONSE",
                "The API returned an empty response."));
        }

        return value;
    }

    private async Task<HttpRequestMessage> CreateRequestAsync(
        HttpMethod method,
        string endpoint,
        ApiRequestOptions? options,
        CancellationToken cancellationToken)
    {
        var request = new HttpRequestMessage(method, endpoint);

        if (options?.RequiresAuthentication != false)
        {
            var accessToken = await accessTokenProvider.GetTokenAsync();

            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                request.Headers.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue(
                        "Bearer",
                        accessToken);
            }
        }

        return request;
    }

    private static async Task<Error> CreateErrorAsync(
        HttpResponseMessage response,
        CancellationToken cancellationToken)
    {
        var error = await response.Content.ReadFromJsonAsync<Error>(
            cancellationToken: cancellationToken);

        if (error is not null)
            return error;

        return new Error(
            $"HTTP_{(int)response.StatusCode}",
            response.ReasonPhrase ?? "An unexpected API error occurred.");
    }

    public Task<Result> PostAsync<TRequest>(
        string endpoint,
        TRequest request,
        ApiRequestOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        return SendWithoutValueAsync(
            HttpMethod.Post,
            endpoint,
            options,
            cancellationToken,
            request);
    }

    public Task<Result> PutAsync<TRequest>(
        string endpoint,
        TRequest request,
        ApiRequestOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        return SendWithoutValueAsync(
            HttpMethod.Put,
            endpoint,
            options,
            cancellationToken,
            request);
    }

    public Task<Result<TResponse>> GetAsync<TRequest, TResponse>(
    string endpoint,
    TRequest? request,
    ApiRequestOptions? options = null,
    CancellationToken cancellationToken = default)
    {
        return SendAsync<TResponse>(
            HttpMethod.Get,
            endpoint,
            options,
            cancellationToken,
            request);
    }

    public Task<Result> GetAsync<TRequest>(
        string endpoint,
        TRequest? request,
        ApiRequestOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        return SendWithoutValueAsync(
            HttpMethod.Get,
            endpoint,
            options,
            cancellationToken,
            request);
    }

    public Task<Result<TResponse>> GetAsync<TResponse>(
        string endpoint,
        ApiRequestOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        return SendAsync<TResponse>(
            HttpMethod.Get,
            endpoint,
            options,
            cancellationToken);
    }

    public Task<Result> GetAsync(
        string endpoint,
        ApiRequestOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        return SendWithoutValueAsync(
             HttpMethod.Get,
             endpoint,
             options,
             cancellationToken);
    }
}
