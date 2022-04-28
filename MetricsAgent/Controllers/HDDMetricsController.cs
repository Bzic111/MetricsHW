using Microsoft.AspNetCore.Mvc;
using MetricsAgent.Interfaces;
using MetricsAgent.Models;

namespace MetricsAgent.Controllers;

[Route("api/metrics/hdd")]
[ApiController]
public class HDDMetricsController : ControllerBase
{
    private readonly ILogger<HDDMetricsController> _logger;
    private readonly IHddMetricsRepository _repository;

    public HDDMetricsController(ILogger<HDDMetricsController> logger, IHddMetricsRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    #region Create

    [HttpPost("new/datetime/{date}/value/{value}")]
    public IActionResult CreateMetric([FromRoute] DateTime date, [FromRoute] int value)
    {
        _logger.LogInformation($"Create new HDD metric with value = {value}, date = {date}");
        _repository.Create(new() { DateTime = date, Value = value });
        return Ok();
    }

    #endregion

    #region Read

    [HttpGet("left/from/{fromTime}/to/{toTime}")]
    public IActionResult GetHDDMetrics([FromRoute] DateTime fromTime, [FromRoute] DateTime toTime)
    {
        _logger.LogInformation($"Get HDD metrics by period from {fromTime} to {toTime}");
        var result = _repository.GetByTimeFilter(fromTime, toTime);
        return Ok(result);
    }

    [HttpGet("all")]
    public IActionResult GetAllHDDMetrics()
    {
        _logger.LogInformation($"Get all HDD metrics");
        var result = _repository.GetAll();
        return Ok(result);
    }

    [HttpGet("id/{id}")]
    public IActionResult GetHDDMetricById([FromRoute] int id)
    {
        _logger.LogInformation($"Get HDD metrics by id = {id}");
        return Ok(_repository.GetById(id));
    }

    #endregion

    #region Update

    [HttpPut("update/id/{id}/value/{value}/datetime/{datetime}")]
    public IActionResult UpdateMetric([FromRoute] int id, [FromRoute] int value, [FromRoute] DateTime datetime)
    {
        _logger.LogInformation($"Update HDD metric id = {id} with value = {value}, date = {datetime}");
        _repository.Update(new HddMetrics() { Id = id, Value = value, DateTime = datetime });
        return Ok();
    }

    #endregion

    #region Delete

    [HttpDelete("delete/id/{id}")]
    public IActionResult DeleteMetric([FromRoute] int id)
    {
        _logger.LogInformation($"Delete HDD metric id = {id}");
        _repository.Delete(id);
        return Ok();
    }

    #endregion
}
