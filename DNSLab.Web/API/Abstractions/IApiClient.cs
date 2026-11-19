using DNSLab.Web.API.Common.Result;

namespace DNSLab.Web.API.Abstractions;

public interface IApiClient
{
    Task<Result<T>> GetAsync<T>(
        string endpoint,
        ApiRequestOptions? options = null,
        CancellationToken cancellationToken = default);

    Task<Result<TResponse>> PostAsync<TRequest, TResponse>(
        string endpoint,
        TRequest request,
        ApiRequestOptions? options = null,
        CancellationToken cancellationToken = default);

    Task<Result> PostAsync<TRequest>(
        string endpoint,
        TRequest request,
        ApiRequestOptions? options = null,
        CancellationToken cancellationToken = default);

    Task<Result<TResponse>> PutAsync<TRequest, TResponse>(
        string endpoint,
        TRequest request,
        ApiRequestOptions? options = null,
        CancellationToken cancellationToken = default);

    Task<Result> PutAsync<TRequest>(
        string endpoint,
        TRequest request,
        ApiRequestOptions? options = null,
        CancellationToken cancellationToken = default);

    Task<Result> DeleteAsync(
        string endpoint,
        ApiRequestOptions? options = null,
        CancellationToken cancellationToken = default);
}