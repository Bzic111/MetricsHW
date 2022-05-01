using MetricsAgent.Interfaces;
using Quartz;
using MetricsAgent.DAL.Models;
using System.Diagnostics;

namespace MetricsAgent.Jobs;
[System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Проверка совместимости платформы", Justification = "<Ожидание>")]
public class RamMetricJob : IJob
{
    private IRamMetricsRepository _repository;
    private PerformanceCounter _performanceCounter;
    public RamMetricJob(IRamMetricsRepository repository)
    {
        _repository = repository;
        _performanceCounter = new PerformanceCounter("Memory", "Available MBytes");
    }
    public Task Execute(IJobExecutionContext context)
    {
        _repository.Create(new RamMetrics
        {
            DateTime = DateTime.Now,
            Value = Convert.ToInt32(_performanceCounter.NextValue())
        });
        return Task.CompletedTask;
    }
}