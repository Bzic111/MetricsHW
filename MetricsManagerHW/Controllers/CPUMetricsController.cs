using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManagerHW.Controllers;

[Route("api/metrics/cpu")]
[ApiController]
public class CPUMetricsController : ControllerBase
{
    private readonly ILogger<CPUMetricsController> _logger;
    public CPUMetricsController(ILogger<CPUMetricsController> logger)
    {
        _logger = logger;
    }

    #region Read

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
