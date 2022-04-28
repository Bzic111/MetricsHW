using MetricsManagerHW.Interface;
using Quartz;
using System.Diagnostics;

namespace MetricsManagerHW.Jobs;
[System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Проверка совместимости платформы", Justification = "<Ожидание>")]
public class NetworkMetricJob : IJob
{
    private INetworkMetricsRepository _repository;
    public NetworkMetricJob(INetworkMetricsRepository repository)
    {
        _repository = repository;

    }
    public Task Execute(IJobExecutionContext context)
    {
        _repository.Create(new Models.NetworkMetrics
        {
            DateTime = DateTime.Now,
            Value = Convert.ToInt32(_performanceCounter.NextValue())
        });
        return Task.CompletedTask;
    }
}