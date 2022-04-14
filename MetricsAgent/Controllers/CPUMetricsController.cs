﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MetricsAgent.Repositoryes;
using MetricsAgent.Interfaces;
using MetricsAgent.Models;

namespace MetricsAgent.Controllers;

[Route("api/metrics/cpu")]
[ApiController]
public class CPUMetricsController : ControllerBase
{
    private readonly ILogger<CPUMetricsController> _logger;
    private readonly CPUMetricsRepository _repository;
    public CPUMetricsController(ILogger<CPUMetricsController> logger, CPUMetricsRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    #region Create

    [HttpPost("new/datetime/{date}/value/{value}")]
    public IActionResult CreateMetric([FromRoute] DateTime date,[FromRoute] int value)
    {
        _logger.LogInformation($"Create new CPU metric with value = {value}, date = {date}");
        _repository.Create(new() { Time = date, Value = value });
        return Ok();
    }

    #endregion

    #region Read

    [HttpGet("all")]
    public IActionResult GetAllCPUMetrics()
    {
        _logger.LogInformation($"Get all CPU metrics");
        var result = _repository.GetAll();
        return Ok(result);
    }

    [HttpGet("id/{id}")]
    public IActionResult GetCPUMetricById([FromRoute]int id)
    {
        _logger.LogInformation($"Get CPU metrics by id = {id}");
        return Ok(_repository.GetById(id));
    }

    [HttpGet("from/{fromTime}/to/{toTime}")]/*from/{fromTime}/to/{toTime}*/
    public IActionResult GetCPUMetrics([FromRoute] DateTime fromTime, [FromRoute] DateTime toTime)//[FromBody] DateTime fromTime, [FromBody] DateTime toTime
    {
        _logger.LogInformation($"Get CPU metrics by period from {fromTime} to {toTime}");
        var result = _repository.GetByTimeFilter(fromTime,toTime);
        return Ok(result);
    }

    [HttpGet("from/{fromTime}/to/{toTime}/percentile/{procentile}")]
    public IActionResult GetCPUMetricsPercentile([FromRoute] int procentile,
                                                 [FromRoute] DateTime fromTime,
                                                 [FromRoute] DateTime toTime)
    {
        _logger.LogInformation($"Get CPU metrics procentile {procentile} by period from {fromTime} to {toTime}");
        var result = _repository.GetByTimeFilterWithPercentile(procentile, fromTime, toTime);
        return Ok(result);
    }

    #endregion

    #region Update

    [HttpPut("update/id/{id}/value/{value}/datetime/{datetime}")]
    public IActionResult UpdateMetric([FromRoute] int id,[FromRoute] int value,[FromRoute] DateTime datetime)
    {
        _logger.LogInformation($"Update CPU metric id = {id} with value = {value}, date = {datetime}");
        _repository.Update(new CpuMetric() { Id = id, Value = value, Time = datetime });
        return Ok();
    }

    #endregion

    #region Delete

    [HttpDelete("delete/id/{id}")]
    public IActionResult DeleteMetric([FromRoute] int id)
    {
        _logger.LogInformation($"Delete CPU metric id = {id}");
        _repository.Delete(id);
        return Ok();
    }

    #endregion
}
