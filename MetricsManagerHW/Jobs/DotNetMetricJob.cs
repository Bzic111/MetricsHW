using MetricsManagerHW.Interface;
using Quartz;
using System.Diagnostics;

namespace MetricsManagerHW.Jobs;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Проверка совместимости платформы", Justification = "<Ожидание>")]
public class DotNetMetricJob : IJob
{
    private IDotNetMetricsRepository _repository;
    public DotNetMetricJob(IDotNetMetricsRepository repository)
    {
        _repository = repository;
        
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
