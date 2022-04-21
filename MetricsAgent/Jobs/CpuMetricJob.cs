using MetricsAgent.Interfaces;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MetricsAgent.Jobs;

public class CpuMetricJob : IJob
{
    private ICPUMetricsRepository _repository;
    private PerformanceCounter _cpuCounter;
    public CpuMetricJob(ICPUMetricsRepository repository)
    {
        _repository = repository;
        _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");

    }
    public Task Execute(IJobExecutionContext context)
    {
        var cpuUsageInPercents = Convert.ToInt32(_cpuCounter.NextValue());
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
