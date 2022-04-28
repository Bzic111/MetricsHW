using Microsoft.AspNetCore.Mvc;

namespace MetricsManagerHW.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HDDMetricsController : ControllerBase
    {
        private readonly ILogger<HDDMetricsController> _logger;
        public HDDMetricsController(ILogger<HDDMetricsController> logger)
        {
            _logger = logger;
        }

        #region Read

        [HttpGet("agent/{agentId}/left/from/{fromTime}/to/{toTime}")]
        public IActionResult GetHDDMetricsFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"Get HDD metrics from agent {agentId}");
            return Ok();
        }

        [HttpGet("cluster/left/from/{fromTime}/to/{toTime}")]
        public IActionResult GetHDDMetricsFromAllCluster([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"Get HDD metrics from cluster");
            return Ok();
        }

        #endregion
    }
}