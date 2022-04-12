using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManagerHW.Controllers
{
    [Route("api/metrics/dotnet")]
    [ApiController]
    public class DotNetMetricsController : ControllerBase
    {
        private readonly ILogger<DotNetMetricsController> _logger;
        public DotNetMetricsController(ILogger<DotNetMetricsController> logger)
        {
            _logger = logger;
        }

        #region Read

        [HttpGet("agent/{agentId}/errors-count/from/{fromTime}/to/{toTime}")]
        public IActionResult GetDotNetMetricsFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"Get DotNet metrics from agent {agentId}");
            return Ok();
        }

        [HttpGet("cluster/errors-count/from/{fromTime}/to/{toTime}")]
        public IActionResult GetDotNetMetricsFromAllCluster([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"Get DotNet metrics from cluster");
            return Ok();
        }

        #endregion
    }
}
