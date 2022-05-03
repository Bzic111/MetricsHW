using Microsoft.AspNetCore.Mvc;
using MetricsManagerHW.Interface;

namespace MetricsManagerHW.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RAMMetricsController : ControllerBase
    {
        private readonly ILogger<RAMMetricsController> _logger;
        private readonly IRamMetricsRepository _repository;

        public RAMMetricsController(ILogger<RAMMetricsController> logger, IRamMetricsRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        #region Read
        [HttpGet("all")]
        public IActionResult GetAllNetworkMetrics()
        {
            _logger.LogInformation($"Get all RAM metrics");
            var result = _repository.GetAll();
            return Ok(result);
        }
        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetRAMMetricsFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"Get RAM metrics from agent {agentId}");
            return Ok();
        }

        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetRAMMetricsFromAllCluster([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"Get RAM metrics from cluster");
            return Ok();
        }

        #endregion
    }
}