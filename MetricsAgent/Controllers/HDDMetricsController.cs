using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers;

[Route("api/metrics/hdd")]
[ApiController]
public class HDDMetricsController : ControllerBase
{
    public HDDMetricsController()
    {

    }

    #region Read

    [HttpGet("left/from/{fromTime}/to/{toTime}")]
    public IActionResult GetHDDMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
    {
        return Ok();
    }

    #endregion
}
