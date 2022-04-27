using MetricsAgent.Interfaces;
using Quartz;
using System.Diagnostics;

namespace MetricsAgent.Jobs;
[System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Проверка совместимости платформы", Justification = "<Ожидание>")]
public class NetworkMetricJob : IJob
{
    private INetworkMetricsRepository _repository;
    private PerformanceCounter _performanceCounter;
    public NetworkMetricJob(INetworkMetricsRepository repository)
    {
        _repository = repository;
        _performanceCounter = new PerformanceCounter("Сетевой адаптер", "Всего байт/с", "Realtek PCIe FE Family Controller");

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