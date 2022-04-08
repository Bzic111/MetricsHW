using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MetricsManagerHW.ext;

namespace MetricsManagerHW.Controllers
{
    [Route("api/agent")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        public AgentsController()
        {

        }
        #region Create

        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] AgentInfo agentInfo)
        {
            return Ok();
        }

        #endregion

        #region Read
        [HttpGet("show/all")]
        public IActionResult GetAllAgents()
        {
            return Ok();
        }
        #endregion

        #region Update

        [HttpPut("enable/{agentId}")]
        public IActionResult EnableAgentById([FromRoute] int agentId)
        {
            return Ok();
        }

        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgentById([FromRoute] int agentId)
        {
            return Ok();
        }

        #endregion

        #region Delete

        #endregion


    }
}
