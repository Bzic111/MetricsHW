using MetricsManagerHW.DAL.Repository;
using MetricsManagerHW.Interface;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MetricsManagerHW.Jobs;
[System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Проверка совместимости платформы", Justification = "<Ожидание>")]
public class CpuMetricJob : IJob
{
    private ICPUMetricsRepository _repository;
    private HttpClient _httpClient;
    private AgentsRepository _agentsRepository;
    public CpuMetricJob(ICPUMetricsRepository repository, AgentsRepository agentsRepository, HttpClient client)
    {
        _repository = repository;
        _httpClient = client;
        _agentsRepository = agentsRepository;
    }
    public Task Execute(IJobExecutionContext context)
    {
        var agents = _agentsRepository.GetAll();
        foreach (var item in agents)
        {
            _httpClient.
        }
        _repository.Create(new Models.CpuMetric
        {
            DateTime = DateTime.Now,
            Value = Convert.ToInt32(_performanceCounter.NextValue())
        });
        return Task.CompletedTask;
    }
}
