using Microsoft.AspNetCore.Mvc;
using MetricsManagerHW.Interface;
using MetricsManagerHW.DAL.Request;
namespace MetricsManagerHW.Controllers
{
    [Route("api/metrics/dotnet")]
    [ApiController]
    public class DotNetMetricsController : ControllerBase
    {
        private readonly ILogger<DotNetMetricsController> _logger;
        private readonly IDotNetMetricsRepository _repository;
        public DotNetMetricsController(ILogger<DotNetMetricsController> logger, IDotNetMetricsRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        #region Read
        [HttpGet("all")]
        public IActionResult GetAllDotNetMetrics()
        {
            _logger.LogInformation($"Get all DotNet metrics");
            var result = _repository.GetAll();
            return Ok(result);
        }
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
