using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MetricsAgent.Interfaces;
using MetricsAgent.Models;
using MetricsAgent.Repositoryes;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RAMMetricsController : ControllerBase
    {
        private readonly ILogger<RAMMetricsController> _logger;
        private readonly RAMMetricsRepository _repository;

        public RAMMetricsController(ILogger<RAMMetricsController> logger, RAMMetricsRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        #region Create

        [HttpPost("new/datetime/{date}/value/{value}")]
        public IActionResult CreateMetric([FromRoute] DateTime date, [FromRoute] int value)
        {
            _logger.LogInformation($"Create new RAM metric with value = {value}, date = {date}");
            _repository.Create(new() { Time = date, Value = value });
            return Ok();
        }

        #endregion

        #region Read

        [HttpGet("all")]
        public IActionResult GetAllNetworkMetrics()
        {
            _logger.LogInformation($"Get all RAM metrics");
            var result = _repository.GetAll();
            return Ok(result);
        }

        [HttpGet("id/{id}")]
        public IActionResult GetNetworkMetricById([FromRoute] int id)
        {
            _logger.LogInformation($"Get RAM metrics by id = {id}");
            return Ok(_repository.GetById(id));
        }

        [HttpGet("available/from/{fromTime}/to/{toTime}")]
        public IActionResult GetRAMMetrics([FromRoute] DateTime fromTime, [FromRoute] DateTime toTime)
        {
            _logger.LogInformation($"Get RAM metrics by period from {fromTime} to {toTime}");
            _repository.GetByTimeFilter(fromTime, toTime);
            return Ok();
        }

        #endregion

        #region Update

        [HttpPut("update/id/{id}/value/{value}/datetime/{datetime}")]
        public IActionResult UpdateMetric([FromRoute] int id, [FromRoute] int value, [FromRoute] DateTime datetime)
        {
            _logger.LogInformation($"Update RAM metric id = {id} with value = {value}, date = {datetime}");
            _repository.Update(new RamMetrics() { Id = id, Value = value, Time = datetime });
            return Ok();
        }

        #endregion

        #region Delete

        [HttpDelete("delete/id/{id}")]
        public IActionResult DeleteMetric([FromRoute] int id)
        {
            _logger.LogInformation($"Delete RAM metric id = {id}");
            _repository.Delete(id);
            return Ok();
        }

        #endregion
    }
}