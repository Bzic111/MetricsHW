using Microsoft.AspNetCore.Mvc;
using MetricsManagerHW.ext;
using MetricsManagerHW.DAL.Repository;

namespace MetricsManagerHW.Controllers
{
    [Route("api/agent")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly ILogger<AgentsController> _logger;
        private readonly AgentsRepository _repository;
        public AgentsController(ILogger<AgentsController> logger, AgentsRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        #region Create

        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] AgentInfo agentInfo)
        {
            _logger.LogInformation($"Register agent {agentInfo.AgentAdress}");
            _repository.Create(new DAL.Models.Agent() { Adress = agentInfo.AgentAdress, Enabled = true });
            return Ok();
        }

        #endregion

        #region Read

        [HttpGet("show/all")]
        public IActionResult GetAllAgents()
        {
            _logger.LogInformation($"Get all agents");
            var result = _repository.GetAll();
            return Ok(result);
        }

        #endregion

        #region Update

        [HttpPut("change")]
        public IActionResult ChangeAgentAdress([FromBody] AgentInfo agentInfo)
        {
            _logger.LogInformation($"Changing agent {agentInfo.AgentId} adress to {agentInfo.AgentAdress}");
            _repository.UpdateAdress(new() { Id = agentInfo.AgentId, Adress = agentInfo.AgentAdress });
            return Ok();
        }

        [HttpPut("enable/{agentId}")]
        public IActionResult EnableAgentById([FromRoute] int agentId)
        {
            _logger.LogInformation($"Enable agent {agentId}");
            var agent = _repository.GetById(agentId);
            if (agent is not null)
            {
                agent.Enabled = true;
                _repository.Update(agent);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgentById([FromRoute] int agentId)
        {
            _logger.LogInformation($"Disable agent {agentId}");
            var agent = _repository.GetById(agentId);
            if (agent is not null)
            {
                agent.Enabled = false;
                _repository.Update(agent);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        #endregion

        #region Delete

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteAgent([FromRoute] int id)
        {
            _logger.LogInformation($"Deleting agent {id}");
            _repository.Delete(id);
            return Ok();
        }

        #endregion


    }
}
