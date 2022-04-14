using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using MetricsAgent.Repositoryes;

namespace AgentTests
{
    public class AgentNetworkMetricsUnitTest
    {
        private NetworkMetricsController _controller;
        private DateTime _from;
        private DateTime _to;
        private Mock<ILogger<NetworkMetricsController>> _mockLogger;
        private Mock<NetworkMetricsRepository> _mock;

        public AgentNetworkMetricsUnitTest()
        {
            _mock = new();
            _mockLogger = new();
            _controller = new(_mockLogger.Object, _mock.Object);
            _from = DateTime.Now.AddDays(-1);
            _to = DateTime.Now.AddDays(1);
        }

        [Fact]
        public void Test_GetNetworkMetrics()
        {
            var result = _controller.GetNetworkMetrics(_from, _to);
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}