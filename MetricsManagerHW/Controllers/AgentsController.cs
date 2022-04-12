using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MetricsManagerHW.ext;
using NLog;
using NLog.Web;

namespace MetricsManagerHW.Controllers
{
    [Route("api/agent")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly ILogger<AgentsController> _logger;
        public AgentsController(ILogger<AgentsController> logger)
        {
            _logger = logger;
        }

        #region Create

        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] AgentInfo agentInfo)
        {
            _logger.LogInformation($"Register agent {agentInfo.AgentAddress}");
            return Ok();
        }

        #endregion

        #region Read

        [HttpGet("show/all")]
        public IActionResult GetAllAgents()
        {
            _logger.LogInformation($"Get all agents");
            return Ok();
        }

        #endregion

        #region Update

        [HttpPut("enable/{agentId}")]
        public IActionResult EnableAgentById([FromRoute] int agentId)
        {
            _logger.LogInformation($"Enable agent {agentId}");
            return Ok();
        }

        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgentById([FromRoute] int agentId)
        {
            _logger.LogInformation($"Disable agent {agentId}");
            return Ok();
        }

        #endregion

        #region Delete

        #endregion


    }
}
