using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MetricsAgent.Repositoryes;
using MetricsAgent.Interfaces;
using MetricsAgent.Models;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/dotnet")]
    [ApiController]
    public class DotNetMetricsController : ControllerBase
    {
        private readonly ILogger<DotNetMetricsController> _logger;
        private readonly DotNetMetricsRepository _repository;

        public DotNetMetricsController(ILogger<DotNetMetricsController> logger,DotNetMetricsRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        #region Create

        [HttpPost("new/datetime/{date}/value/{value}")]
        public IActionResult CreateMetric([FromRoute] DateTime date, [FromRoute] int value)
        {
            _logger.LogInformation($"Create new DotNet metric with value = {value}, date = {date}");
            _repository.Create(new() { Time = date, Value = value });
            return Ok();
        }

        #endregion

        #region Read

        [HttpGet("errors-count/from/{fromTime}/to/{toTime}")]
        public IActionResult GetDotNetMetrics([FromRoute] DateTime fromTime, [FromRoute] DateTime toTime)
        {
            _logger.LogInformation($"Get DotNet metrics by period from {fromTime} to {toTime}");
            var result = _repository.GetByTimeFilter(fromTime, toTime);
            return Ok();
        }

        [HttpGet("all")]
        public IActionResult GetAllDotNetMetrics()
        {
            _logger.LogInformation($"Get all DotNet metrics");
            var result = _repository.GetAll();
            return Ok(result);
        }

        [HttpGet("id/{id}")]
        public IActionResult GetDotNetMetricById([FromRoute] int id)
        {
            _logger.LogInformation($"Get DotNet metrics by id = {id}");
            return Ok(_repository.GetById(id));
        }
        
        #endregion

        #region Update

        [HttpPut("update/id/{id}/value/{value}/datetime/{datetime}")]
        public IActionResult UpdateMetric([FromRoute] int id, [FromRoute] int value, [FromRoute] DateTime datetime)
        {
            _logger.LogInformation($"Update DotNet metric id = {id} with value = {value}, date = {datetime}");
            _repository.Update(new DotNetMetrics() { Id = id, Value = value, Time = datetime });
            return Ok();
        }

        #endregion

        #region Delete

        [HttpDelete("delete/id/{id}")]
        public IActionResult DeleteMetric([FromRoute] int id)
        {
            _logger.LogInformation($"Delete DotNet metric id = {id}");
            _repository.Delete(id);
            return Ok();
        }

        #endregion
    }
}
