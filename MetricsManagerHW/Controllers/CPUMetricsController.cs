using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManagerHW.Controllers;

[Route("api/metrics/cpu")]
[ApiController]
public class CPUMetricsController : ControllerBase
{
    public CPUMetricsController()
    {
    }

    #region Read

    [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
    public IActionResult GetCPUMetricsFromAgent([FromRoute] int agentId,
                                                [FromRoute] TimeSpan fromTime,
                                                [FromRoute] TimeSpan toTime)
    {
        return Ok();
    }

    [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}/percentiles/{percentile}")]
    public IActionResult GetCPUMetricsPercentileFromAgent([FromRoute] int agentId,
                                                          [FromRoute] int percentile,
                                                          [FromRoute] TimeSpan fromTime,
                                                          [FromRoute] TimeSpan toTime)
    {
        return Ok();
    }

    [HttpGet("cluster/from/{fromTime}/to/{toTime}/percentiles/{percentile}")]
    public IActionResult GetCPUMetricsPercentileFromAllCluster([FromRoute] int percentile, 
                                                               [FromRoute] TimeSpan fromTime,
                                                               [FromRoute] TimeSpan toTime)
    {
        return Ok();
    }

    [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
    public IActionResult GetCPUMetricsFromAllCluster([FromRoute] TimeSpan fromTime,
                                                     [FromRoute] TimeSpan toTime)
    {
        return Ok();
    }

    #endregion
}
