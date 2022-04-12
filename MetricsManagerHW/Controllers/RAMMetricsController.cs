using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManagerHW.Controllers;

[Route("api/metrics/ram")]
[ApiController]
public class RAMMetricsController : ControllerBase
{
    private readonly ILogger<RAMMetricsController> _logger;
    public RAMMetricsController(ILogger<RAMMetricsController> logger)
    {
        _logger = logger;
    }

    #region Read

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
