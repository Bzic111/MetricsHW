using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using MetricsAgent.Repositoryes;

namespace AgentTests
{
    public class AgentDotNetMetricsUnitTest
    {
        private DotNetMetricsController _controller;
        private DateTime _from;
        private DateTime _to; 
        private Mock<DotNetMetricsRepository> _mock;
        private Mock<ILogger<DotNetMetricsController>> _mockLogger;

        public AgentDotNetMetricsUnitTest()
        {
            _mock = new();
            _mockLogger = new();
            _controller = new (_mockLogger.Object,_mock.Object);
            _from = DateTime.Now.AddDays(-1);
            _to = DateTime.Now.AddDays(1);
        }

        [Fact]
        public void Test_GetDotNetMetrics()
        {
            var result = _controller.GetDotNetMetrics(_from, _to);
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}