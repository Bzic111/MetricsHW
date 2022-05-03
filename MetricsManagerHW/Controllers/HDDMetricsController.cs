using Microsoft.AspNetCore.Mvc;
using MetricsManagerHW.Interface;

namespace MetricsManagerHW.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HDDMetricsController : ControllerBase
    {
        private readonly ILogger<HDDMetricsController> _logger;
        private readonly IHddMetricsRepository _repository;
        public HDDMetricsController(ILogger<HDDMetricsController> logger, IHddMetricsRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        #region Read

        [HttpGet("all")]
        public IActionResult GetAllHDDMetrics()
        {
            _logger.LogInformation($"Get all HDD metrics");
            var result = _repository.GetAll();
            return Ok(result);
        }
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