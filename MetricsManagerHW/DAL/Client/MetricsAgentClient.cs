using MetricsManagerHW.DAL.Models;
//using System.Text.Json;

namespace MetricsManagerHW.DAL.Client;

public class MetricsAgentClient : IMetricsAgentClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<MetricsAgentClient> _logger;

    public MetricsAgentClient(HttpClient client, ILogger<MetricsAgentClient> logger)
    {
        _httpClient = client;
        _logger = logger;
    }

    public ResponseFromAgent<T> GetMetricsFromAgent<T>(RequestToAgent request) where T : class, IMetric
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.Agent.AgentAdress}/api/{request.ApiRoute}/from/{request.From.ToString("s")}/to/{request.To.ToString("s")}");
        try
        {
            HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
            var resp = response.Content.ReadFromJsonAsync<List<T>>().Result;
            if (resp is not null && resp.Count > 0)
            {
                resp.ForEach(metric => metric.AgentId = request.Agent.AgentId);
                ResponseFromAgent<T> result = new ResponseFromAgent<T>()
                {
                    AgentId = request.Agent.AgentId,
                    Collection = resp
                };
                return result;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
        return null!;
    }
}
//public ResponseFromAgent<RamMetrics> GetRamMetrics(RequestToAgent request)
//{
//    var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.BaseAdress}/api/{request.ApiRoute}/from/{request.From}/to/{request.To}");
//    try
//    {
//        HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
//        using (var responseStream = response.Content.ReadAsStreamAsync().Result)
//        {
//            return JsonSerializer.DeserializeAsync<ResponseFromAgent<RamMetrics>>(responseStream).Result!;
//        }
//    }
//    catch (Exception ex)
//    {
//        _logger.LogError(ex.Message);
//    }
//    return null!;
//}

//public ResponseFromAgent<HddMetrics> GetHddMetrics(RequestToAgent request)
//{
//    var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.BaseAdress}/api/{request.ApiRoute}/from/{request.From}/to/{request.To}");
//    try
//    {
//        HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
//        using (var responseStream = response.Content.ReadAsStreamAsync().Result)
//        {
//            return JsonSerializer.DeserializeAsync<ResponseFromAgent<HddMetrics>>(responseStream).Result!;
//        }
//    }
//    catch (Exception ex)
//    {
//        _logger.LogError(ex.Message);
//    }
//    return null!;
//}

//public ResponseFromAgent<DotNetMetrics> GetDonNetMetrics(RequestToAgent request)
//{
//    var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.BaseAdress}/api/{request.ApiRoute}/from/{request.From}/to/{request.To}");
//    try
//    {
//        HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
//        using (var responseStream = response.Content.ReadAsStreamAsync().Result)
//        {
//            return JsonSerializer.DeserializeAsync<ResponseFromAgent<DotNetMetrics>>(responseStream).Result!;
//        }
//    }
//    catch (Exception ex)
//    {
//        _logger.LogError(ex.Message);
//    }
//    return null!;
//}

//public ResponseFromAgent<CpuMetric> GetCpuMetrics(RequestToAgent request)
//{
//    var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.BaseAdress}/api/{request.ApiRoute}/from/{request.From}/to/{request.To}");
//    try
//    {
//        HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
//        using (var responseStream = response.Content.ReadAsStreamAsync().Result)
//        {
//            return JsonSerializer.DeserializeAsync<ResponseFromAgent<CpuMetric>>(responseStream).Result!;
//        }
//    }
//    catch (Exception ex)
//    {
//        _logger.LogError(ex.Message);
//    }
//    return null!;
//}

//public ResponseFromAgent<NetworkMetrics> GetNetworkMetrics(RequestToAgent request)
//{
//    var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.BaseAdress}/api/{request.ApiRoute}/from/{request.From}/to/{request.To}");
//    try
//    {
//        HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
//        using (var responseStream = response.Content.ReadAsStreamAsync().Result)
//        {
//            return JsonSerializer.DeserializeAsync<ResponseFromAgent<NetworkMetrics>>(responseStream).Result!;
//        }
//    }
//    catch (Exception ex)
//    {
//        _logger.LogError(ex.Message);
//    }
//    return null!;
//}
