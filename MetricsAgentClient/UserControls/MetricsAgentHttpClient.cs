using MetricsAgentClient.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;

namespace MetricsAgentClient.UserControls;

public class MetricsAgentHttpClient
{
    private readonly HttpClient _httpClient;
    public List<Metric> LastList { get; set; }
    public int LastMetric { get; set; }
    public MetricsAgentHttpClient(HttpClient client)
    {
        _httpClient = client;
    }

    public string GetRequest()
    {
        var httpRequest = new HttpRequestMessage(
                                        HttpMethod.Get,
                                        $@"http://localhost:5056/api/metrics/cpu/all");
        LastList = GetMetricsList(httpRequest);
        StringBuilder sb = new StringBuilder();
        foreach (var item in LastList)
        {
            sb.Append($"{item.Id}\t{item.DateTime}\t{item.Value}\n");
        }
        LastMetric = LastList[^1].Value;
        var result = sb.ToString();
        return result;
    }

    public List<Metric> GetMetricsList(HttpRequestMessage httpRequest)
    {
        try
        {
            HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
            StringBuilder sb = new StringBuilder();

            var resp = response.Content.ReadFromJsonAsync<List<Metric>>().Result!;
            return resp.Count >= 10 ? resp.GetRange(resp.Count - 10, 10) : resp;

        }
        catch (Exception ex)
        {
            throw;
        }
    }
    //public ResponseFromAgent<T> GetMetricsFromAgent<T>(RequestToAgent request) where T : class, IMetric
    //{
    //    var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.Agent.AgentAdress}/api/{request.ApiRoute}/from/{request.From.ToString("s")}/to/{request.To.ToString("s")}");
    //    try
    //    {
    //        HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
    //        var resp = response.Content.ReadFromJsonAsync<List<T>>().Result;
    //        if (resp is not null && resp.Count > 0)
    //        {
    //            resp.ForEach(metric => metric.AgentId = request.Agent.AgentId);
    //            ResponseFromAgent<T> result = new ResponseFromAgent<T>()
    //            {
    //                AgentId = request.Agent.AgentId,
    //                Collection = resp
    //            };
    //            return result;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex.Message);
    //    }
    //    return null!;
    //}
}