using MetricsManagerHW.DAL.Response;
using MetricsManagerHW.DAL.Request;
using MetricsManagerHW.DAL.Models;
using System.Net.Http;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace MetricsManagerHW.DAL.Client;

public interface IMetricsAgentClient
{
    ResponseFromAgent<RamMetrics> GetRamMetrics(RequestToAgent request);
    ResponseFromAgent<HddMetrics> GetHddMetrics(RequestToAgent request);
    ResponseFromAgent<DotNetMetrics> GetDonNetMetrics(RequestToAgent request);
    ResponseFromAgent<CpuMetric> GetCpuMetrics(RequestToAgent request);
    ResponseFromAgent<NetworkMetrics> GetNetworkMetrics(RequestToAgent request);
}

public class RequestToAgent
{
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public string BaseAdress { get; set; }
    public string ApiRoute { get; set; }
}

public class ResponseFromAgent<T> where T:class
{
    public List<T> Collection { get; set; }
    public int AgentId { get; set; }
}    

public class MetricsAgentClient : IMetricsAgentClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger _logger;

    public MetricsAgentClient(HttpClient client, ILogger logger)
    {
        _httpClient = client;
        _logger = logger;
    }

    public ResponseFromAgent<RamMetrics> GetRamMetrics(RequestToAgent request)
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.BaseAdress}/api/{request.ApiRoute}/from/{request.From}/to/{request.To}");
        try
        {
            HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
            using (var responseStream = response.Content.ReadAsStreamAsync().Result)
            {
                return JsonSerializer.DeserializeAsync<ResponseFromAgent<RamMetrics>>(responseStream).Result!;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
        return null!;
    }

    public ResponseFromAgent<HddMetrics> GetHddMetrics(RequestToAgent request)
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.BaseAdress}/api/{request.ApiRoute}/from/{request.From}/to/{request.To}");
        try
        {
            HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
            using (var responseStream = response.Content.ReadAsStreamAsync().Result)
            {
                return JsonSerializer.DeserializeAsync<ResponseFromAgent<HddMetrics>>(responseStream).Result!;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
        return null!;
    }

    public ResponseFromAgent<DotNetMetrics> GetDonNetMetrics(RequestToAgent request)
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.BaseAdress}/api/{request.ApiRoute}/from/{request.From}/to/{request.To}");
        try
        {
            HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
            using (var responseStream = response.Content.ReadAsStreamAsync().Result)
            {
                return JsonSerializer.DeserializeAsync<ResponseFromAgent<DotNetMetrics>>(responseStream).Result!;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
        return null!;
    }

    public ResponseFromAgent<CpuMetric> GetCpuMetrics(RequestToAgent request)
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.BaseAdress}/api/{request.ApiRoute}/from/{request.From}/to/{request.To}");
        try
        {
            HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
            using (var responseStream = response.Content.ReadAsStreamAsync().Result)
            {
                return JsonSerializer.DeserializeAsync<ResponseFromAgent<CpuMetric>>(responseStream).Result!;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
        return null!;
    }

    public ResponseFromAgent<NetworkMetrics> GetNetworkMetrics(RequestToAgent request)
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.BaseAdress}/api/{request.ApiRoute}/from/{request.From}/to/{request.To}");
        try
        {
            HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
            using (var responseStream = response.Content.ReadAsStreamAsync().Result)
            {
                return JsonSerializer.DeserializeAsync<ResponseFromAgent<NetworkMetrics>>(responseStream).Result!;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
        return null!;
    }
}
