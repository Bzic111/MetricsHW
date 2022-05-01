using Microsoft.AspNetCore.Mvc;
using MetricsManagerHW.Interface;
using MetricsManagerHW.DAL.Request;

namespace MetricsManagerHW.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CPUMetricsController : ControllerBase
    {
        private readonly ILogger<CPUMetricsController> _logger;
        private readonly ICPUMetricsRepository _repository;

        public CPUMetricsController(ILogger<CPUMetricsController> logger, ICPUMetricsRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        #region Create

        [HttpPost("create")]
        public IActionResult CreateMetric([FromBody] CpuMetricCreateRequest req)
        {
            _logger.LogInformation($"Create new CPU metric with value = {req.Value}, date = {req.Date}");
            _repository.Create(new() { Id = 0, DateTime = req.Date, Value = req.Value });
            return Ok();
        }

        #endregion

        #region Read

        [HttpGet("all")]
        public IActionResult GetAllCPUMetrics()
        {
            _logger.LogInformation($"Get all CPU metrics");
            var result = _repository.GetAll();
            return Ok(result);
        }
        [HttpGet("all/agent/{id}")]
        public void GetAllCPUMetricsFromAgent([FromRoute] int id)
        {
            _logger.LogInformation($"Get all CPU metrics from agent {id}");
            var result = _repository.GetAllOfAgent(id);
        }

        [HttpGet("id/{id}")]
        public IActionResult GetCPUMetricById([FromRoute] int id)
        {
            _logger.LogInformation($"Get CPU metrics by id = {id}");
            return Ok(_repository.GetById(id));
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetCPUMetricsFromAgent([FromRoute] int agentId,
                                                    [FromRoute] TimeSpan fromTime,
                                                    [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"Get CPU metrics from agent {agentId}");
            return Ok();
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}/percentiles/{percentile}")]
        public IActionResult GetCPUMetricsPercentileFromAgent([FromRoute] int agentId,
                                                              [FromRoute] int percentile,
                                                              [FromRoute] TimeSpan fromTime,
                                                              [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"Get CPU metrics percentile {percentile} from agent {agentId}");
            return Ok();
        }

        [HttpGet("cluster/from/{fromTime}/to/{toTime}/percentiles/{percentile}")]
        public IActionResult GetCPUMetricsPercentileFromAllCluster([FromRoute] int percentile,
                                                                   [FromRoute] TimeSpan fromTime,
                                                                   [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"Get CPU metrics percentiles {percentile} from cluster");
            return Ok();
        }

        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetCPUMetricsFromAllCluster([FromRoute] TimeSpan fromTime,
                                                         [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"Get CPU metrics from cluster");
            return Ok();
        }

        #endregion
    }
}