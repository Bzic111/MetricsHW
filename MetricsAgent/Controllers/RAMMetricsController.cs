using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers;

[Route("api/metrics/ram")]
[ApiController]
public class RAMMetricsController : ControllerBase
{
    public RAMMetricsController()
    {

    }

    #region Read

    [HttpGet("available/from/{fromTime}/to/{toTime}")]
    public IActionResult GetRAMMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
    {
        return Ok();
    }

    #endregion
}
