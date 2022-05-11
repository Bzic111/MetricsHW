using MetricsManagerHW.DAL.Models;

namespace MetricsManagerHW.DAL.Client;

public interface IMetricsAgentClient
{
    ResponseFromAgent<T> GetMetricsFromAgent<T>(RequestToAgent request) where T : class, IMetric;
    //ResponseFromAgent<RamMetrics> GetRamMetrics(RequestToAgent request);
    //ResponseFromAgent<HddMetrics> GetHddMetrics(RequestToAgent request);
    //ResponseFromAgent<DotNetMetrics> GetDonNetMetrics(RequestToAgent request);
    //ResponseFromAgent<CpuMetric> GetCpuMetrics(RequestToAgent request);
    //ResponseFromAgent<NetworkMetrics> GetNetworkMetrics(RequestToAgent request);
}