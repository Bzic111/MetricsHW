using Xunit;
using MetricsManagerHW.Controllers;
using System;
using Microsoft.AspNetCore.Mvc;
using MetricsManagerHW.ext;

namespace ManagerTests
{
    public class ManagerAgentsUnitTest
    {
        private AgentsController _controller;
        private AgentInfo _agentInfo;
        public ManagerAgentsUnitTest()
        {
            _controller = new();
            _agentInfo = new AgentInfo() { AgentId = 1, AgentAdress = @"http://agent.info" };
        }

        [Fact]
        public void Test_RegisterAgent()
        {
            var result = _controller.RegisterAgent(_agentInfo);
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void Test_EnableAgentById()
        {
            var result = _controller.EnableAgentById(1);
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void Test_DisableAgentById()
        {
            var result = _controller.DisableAgentById(1);
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void Test_GetAllAgents()
        {
            var result = _controller.GetAllAgents();
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}