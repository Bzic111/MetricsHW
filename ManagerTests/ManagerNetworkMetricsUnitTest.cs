using Xunit;
using MetricsManagerHW.Controllers;
using System;
using Microsoft.AspNetCore.Mvc;

namespace ManagerTests
{
    public class ManagerNetworkMetricsUnitTest
    {
        private NetworkMetricsController _controller;
        private int _agentId;
        private TimeSpan _from;
        private TimeSpan _to;

        public ManagerNetworkMetricsUnitTest()
        {
            _controller = new();
            _agentId = 1;
            _from = TimeSpan.FromSeconds(1);
            _to = TimeSpan.FromSeconds(10);
        }

        [Fact]
        public void Test_GetNetworkMetricsFromAgent()
        {
            var result = _controller.GetNetworkMetricsFromAgent(_agentId, _from, _to);
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void Test_GetNetworkMetricsFromAllCluster()
        {
            var result = _controller.GetNetworkMetricsFromAllCluster(_from, _to);
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}