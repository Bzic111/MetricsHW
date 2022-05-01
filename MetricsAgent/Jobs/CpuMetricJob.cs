using MetricsAgent.Interfaces;
using MetricsAgent.DAL.Models;
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
    private readonly ILogger<CpuMetricJob> _logger;
    public CpuMetricJob(ICPUMetricsRepository repository, ILogger<CpuMetricJob> logger)
    {
        _repository = repository;
        _performanceCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        _logger = logger;
    }
    public Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("Init task job");
        _repository.Create(new CpuMetric
        {
            DateTime = DateTime.Now,
            Value = Convert.ToInt32(_performanceCounter.NextValue())
        });
        return Task.CompletedTask;
    }
}
