using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers;

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

    [HttpGet("available/from/{fromTime}/to/{toTime}")]
    public IActionResult GetRAMMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
    {
        _logger.LogInformation($"Get RAM metrics by period from {fromTime} to {toTime}");
        return Ok();
    }

    #endregion
}
