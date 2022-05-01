using MetricsAgent.DAL.Models;
using MetricsAgent.Interfaces;
using Quartz;
using System.Diagnostics;

namespace MetricsAgent.Jobs;
[System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Проверка совместимости платформы", Justification = "<Ожидание>")]
public class HddMetricJob : IJob
{
    private IHddMetricsRepository _repository;
    private PerformanceCounter _performanceCounter;
    public HddMetricJob(IHddMetricsRepository repository)
    {
        _repository = repository;
        _performanceCounter = new PerformanceCounter("Логический диск", "Свободно мегабайт", "_Total");
    }
    public Task Execute(IJobExecutionContext context)
    {
        _repository.Create(new HddMetrics
        {
            DateTime = DateTime.Now,
            Value = Convert.ToInt32(_performanceCounter.NextValue())
        });
        return Task.CompletedTask;
    }
}
