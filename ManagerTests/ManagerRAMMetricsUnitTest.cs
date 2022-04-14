using Xunit;
using MetricsManagerHW.Controllers;
using System;
using Microsoft.AspNetCore.Mvc;

namespace ManagerTests
{
    public class ManagerRAMMetricsUnitTest
    {
        private RAMMetricsController _controller;
        private int _agentId;
        private TimeSpan _from;
        private TimeSpan _to;

        public ManagerRAMMetricsUnitTest()
        {
            _controller = new();
            _agentId = 1;
            _from = TimeSpan.FromSeconds(1);
            _to = TimeSpan.FromSeconds(10);
        }
        [Fact]
        public void Test_GetRAMMetricsFromAgent()
        {
            var result = _controller.GetRAMMetricsFromAgent(_agentId, _from, _to);
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void Test_GetRAMMetricsFromAllCluster()
        {
            var result = _controller.GetRAMMetricsFromAllCluster(_from, _to);
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}