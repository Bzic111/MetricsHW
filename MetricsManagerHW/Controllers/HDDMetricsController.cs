using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManagerHW.Controllers;

[Route("api/metrics/hdd")]
[ApiController]
public class HDDMetricsController : ControllerBase
{
    public HDDMetricsController()
    {

    }

    #region Read

    [HttpGet("agent/{agentId}/left/from/{fromTime}/to/{toTime}")]
    public IActionResult GetHDDMetricsFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
    {
        return Ok();
    }

    [HttpGet("cluster/left/from/{fromTime}/to/{toTime}")]
    public IActionResult GetHDDMetricsFromAllCluster([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
    {
        return Ok();
    }

    #endregion
}
