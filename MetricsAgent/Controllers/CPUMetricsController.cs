using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers;

[Route("api/metrics/cpu")]
[ApiController]
public class CPUMetricsController : ControllerBase
{
    public CPUMetricsController()
    {
    }

    #region Read

    [HttpGet("from/{fromTime}/to/{toTime}")]
    public IActionResult GetCPUMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
    {
        return Ok();
    }

    [HttpGet("from/{fromTime}/to/{toTime}/percentiles/{percentile}")]
    public IActionResult GetCPUMetricsPercentile([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
    {
        return Ok();
    }

    #endregion
}
