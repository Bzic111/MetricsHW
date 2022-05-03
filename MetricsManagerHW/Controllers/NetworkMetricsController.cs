using Microsoft.AspNetCore.Mvc;
using MetricsManagerHW.Interface;

namespace MetricsManagerHW.Controllers
{
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase
    {
        private readonly ILogger<NetworkMetricsController> _logger;
        private readonly INetworkMetricsRepository _repository;
        public NetworkMetricsController(ILogger<NetworkMetricsController> logger, INetworkMetricsRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        #region Read
        [HttpGet("all")]
        public IActionResult GetAllNetworkMetrics()
        {
            _logger.LogInformation($"Get all Network metrics");
            var result = _repository.GetAll();
            return Ok(result);
        }
        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetNetworkMetricsFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"Get Network metrics from agent {agentId}");
            return Ok();
        }

        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetNetworkMetricsFromAllCluster([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"Get Network metrics from cluster");
            return Ok();
        }

        #endregion
    }
}
