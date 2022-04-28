using MetricsAgent.Interfaces;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MetricsAgent.Jobs;
[System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Проверка совместимости платформы", Justification = "<Ожидание>")]
public class CpuMetricJob : IJob
{
    private ICPUMetricsRepository _repository;
    private PerformanceCounter _performanceCounter;
    public CpuMetricJob(ICPUMetricsRepository repository)
    {
        _repository = repository;
        _performanceCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");

    }
    public Task Execute(IJobExecutionContext context)
    {
        _repository.Create(new Models.CpuMetric
        {
            DateTime = DateTime.Now,
            Value = Convert.ToInt32(_performanceCounter.NextValue())
        });
        return Task.CompletedTask;
    }
}
