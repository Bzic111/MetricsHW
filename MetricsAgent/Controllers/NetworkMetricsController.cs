using Microsoft.AspNetCore.Mvc;
using MetricsAgent.Interfaces;
using MetricsAgent.Models;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase
    {
        private readonly ILogger<NetworkMetricsController> _logger;
        private readonly INetworkMetricsRepository _repository;

        public NetworkMetricsController(ILogger<NetworkMetricsController> logger, INetworkMetricsRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        #region Create

        [HttpPost("new/datetime/{date}/value/{value}")]
        public IActionResult CreateMetric([FromRoute] DateTime date, [FromRoute] int value)
        {
            _logger.LogInformation($"Create new Network metric with value = {value}, date = {date}");
            _repository.Create(new() { DateTime = date, Value = value });
            return Ok();
        }

        #endregion

        #region Read

        [HttpGet("all")]
        public IActionResult GetAllNetworkMetrics()
        {
            _logger.LogInformation($"Get all Network metrics");
            var result = _repository.GetAll();
            return Ok(result);
        }

        [HttpGet("id/{id}")]
        public IActionResult GetNetworkMetricById([FromRoute] int id)
        {
            _logger.LogInformation($"Get Network metrics by id = {id}");
            return Ok(_repository.GetById(id));
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetNetworkMetrics([FromRoute] DateTime fromTime, [FromRoute] DateTime toTime)
        {
            _logger.LogInformation($"Get Network metrics by period from {fromTime} to {toTime}");
            var result = _repository.GetByTimeFilter(fromTime, toTime);
            return Ok(result);
        }

        #endregion

        #region Update

        [HttpPut("update/id/{id}/value/{value}/datetime/{datetime}")]
        public IActionResult UpdateMetric([FromRoute] int id, [FromRoute] int value, [FromRoute] DateTime datetime)
        {
            _logger.LogInformation($"Update Network metric id = {id} with value = {value}, date = {datetime}");
            _repository.Update(new NetworkMetrics() { Id = id, Value = value, DateTime = datetime });
            return Ok();
        }

        #endregion

        #region Delete

        [HttpDelete("delete/id/{id}")]
        public IActionResult DeleteMetric([FromRoute] int id)
        {
            _logger.LogInformation($"Delete Network metric id = {id}");
            _repository.Delete(id);
            return Ok();
        }

        #endregion
    }
}
