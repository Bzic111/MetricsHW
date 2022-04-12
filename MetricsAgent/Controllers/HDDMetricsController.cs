using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers;

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

    [HttpGet("left/from/{fromTime}/to/{toTime}")]
    public IActionResult GetHDDMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
    {
        _logger.LogInformation($"Get HDD metrics by period from {fromTime} to {toTime}");
        return Ok();
    }

    #endregion
}
