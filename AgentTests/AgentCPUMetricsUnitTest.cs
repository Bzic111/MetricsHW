using MetricsAgent.Controllers;
using MetricsAgent.Repositoryes;
using MetricsAgent.Interfaces;
using MetricsAgent.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;

namespace AgentTests
{
    public class AgentCPUMetricsUnitTest
    {
        private CPUMetricsController _controller;
        private DateTime _from;
        private DateTime _to;
        private int _procentile;
        private Mock<CPUMetricsRepository> _mockRepository;
        private Mock<ILogger<CPUMetricsController>> _mockLogger;
        public AgentCPUMetricsUnitTest()
        {
            _mockRepository = new();
            _mockLogger = new();
            _controller = new CPUMetricsController(_mockLogger.Object, _mockRepository.Object);
            _from = DateTime.Now.AddDays(-1);
            _to = DateTime.Now.AddDays(1);
            _procentile = 90;
        }

        [Fact]
        public void Test_CreateCPUMetric()
        {
            _mockRepository.Setup(repository => repository.Create(It.IsAny<CpuMetric>())).Verifiable();
            var result = _controller.CreateMetric(
                new MetricsAgent.Request.CpuMetricCreateRequest() { Date = DateTime.Now, Value = 50 });
            _mockRepository.Verify(repo => repo.Create(It.IsAny<CpuMetric>()), Times.AtMostOnce());

        }

        [Fact]
        public void Test_GetCPUMetrics()
        {
            var result = _controller.GetCPUMetrics(_from, _to);
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void Test_GetCPUMetricsPercentile()
        {
            var result = _controller.GetCPUMetricsPercentile(_procentile, _from, _to);
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}