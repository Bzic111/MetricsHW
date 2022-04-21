using MetricsAgent.Interfaces;
using Quartz;
using System.Diagnostics;

namespace MetricsAgent.Jobs;

public class RamMetricJob : IJob
{
    private ICPUMetricsRepository _repository;
    private PerformanceCounter _ramCounter;
    public RamMetricJob(ICPUMetricsRepository repository)
    {
        _repository = repository;
        _ramCounter = new PerformanceCounter("Memory", "Available MBytes");

    }
    public Task Execute(IJobExecutionContext context)
    {
        var cpuUsageInPercents = Convert.ToInt32(_ramCounter.NextValue());
        // Узнаем, когда мы сняли значение метрики
        var time = DateTime.Now;
        //TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
        // Теперь можно записать что-то посредством репозитория
        _repository.Create(new Models.CpuMetric
        {
            DateTime = time,
            Value = cpuUsageInPercents
        });
        return Task.CompletedTask;
    }
}