using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers;

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

    [HttpGet("from/{fromTime}/to/{toTime}")]
    public IActionResult GetCPUMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
    {
        _logger.LogInformation($"Get CPU metrics by period from {fromTime} to {toTime}");
        return Ok();
    }

    [HttpGet("from/{fromTime}/to/{toTime}/percentiles/{percentile}")]
    public IActionResult GetCPUMetricsPercentile([FromRoute] int procentile, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
    {
        _logger.LogInformation($"Get CPU metrics procentile {procentile} by period from {fromTime} to {toTime}");
        return Ok();
    }

    #endregion
}
