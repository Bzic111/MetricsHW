using MetricsAgent.Interfaces;
using Quartz;
using System.Diagnostics;

namespace MetricsAgent.Jobs;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Проверка совместимости платформы", Justification = "<Ожидание>")]
public class DotNetMetricJob : IJob
{
    private IDotNetMetricsRepository _repository;
    private PerformanceCounter _performanceCounter;
    public DotNetMetricJob(IDotNetMetricsRepository repository)
    {
        _repository = repository;
        _performanceCounter = new PerformanceCounter("Приложения ASP.NET", "Общее число ошибок", "__Total__");

    }

    public Task Execute(IJobExecutionContext context)
    {
        _repository.Create(new Models.DotNetMetrics
        {
            DateTime = DateTime.Now,
            Value = Convert.ToInt32(_performanceCounter.NextValue())
        });
        return Task.CompletedTask;
    }
}
