using MetricsManagerHW.DAL.Client;
using MetricsManagerHW.DAL.Models;
using MetricsManagerHW.DAL.Repository;
using MetricsManagerHW.Interface;
using Quartz;

namespace MetricsManagerHW.Jobs;

public class CpuMetricJob : IJob
{
    private ICPUMetricsRepository _repository;
    private MetricsAgentClient _httpClientMetricsAgent;
    private AgentsRepository _agentsRepository;
    private ILogger<CpuMetricJob> _logger;
    public CpuMetricJob(ICPUMetricsRepository cpuRepository,
                        AgentsRepository agentsRepository,
                        MetricsAgentClient client,
                        ILogger<CpuMetricJob> logger)
    {
        _repository = cpuRepository;
        _httpClientMetricsAgent = client;
        _agentsRepository = agentsRepository;
        _logger = logger;
    }
    public Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("Init task job");
        var agents = _agentsRepository.GetAll(true);
        List<ResponseFromAgent<CpuMetric>> responseList = new();

        foreach (var agent in agents)
        {
            _logger.LogDebug("Run 1-st cycle");
            RequestToAgent request = new RequestToAgent()
            {
                ApiRoute = @"metrics/cpu",
                From = DateTime.Now.AddMinutes(-1),
                To = DateTime.Now,
                Agent = new()
                {
                    AgentId = agent.Id,
                    AgentAdress = agent.Adress! /*@"http://localhost:5056"*/
                }
            };
            responseList.Add(_httpClientMetricsAgent.GetMetricsFromAgent<CpuMetric>(request));
        }

        foreach (var response in responseList)
        {
            _logger.LogDebug("Run 2-st cycle");
            if (response is not null)
            {
                SendDataToDB(response.Collection, _repository);
            }
        }
        return Task.CompletedTask;

        void SendDataToDB<T>(IEnumerable<T> list, IRepository<T> repository) where T : class
        {
            _logger.LogDebug("Sending data to DB");
            foreach (var item in list)
            {
                repository.Create(item);
                _logger.LogDebug("Data sended to DB");
            }
        }
    }

}
