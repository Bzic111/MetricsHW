using MetricsManagerHW.DAL.Repository;
using Quartz;

namespace MetricsManagerHW.Jobs;

public class AgentsJob : IJob
{
    private AgentsRepository _repository;
    private HttpClient _httpClient;
    public AgentsJob(AgentsRepository repository, HttpClient client)
    {
        _repository = repository;
        _httpClient = client;
    }
    public Task Execute(IJobExecutionContext context)
    {
        throw new NotImplementedException();
    }
}