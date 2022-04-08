using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace AgentTests;

public class AgentNetworkMetricsUnitTest
{
    private NetworkMetricsController _controller;
    private TimeSpan _from;
    private TimeSpan _to;

    public AgentNetworkMetricsUnitTest()
    {
        _controller = new NetworkMetricsController();
        _from = TimeSpan.FromSeconds(1);
        _to = TimeSpan.FromSeconds(10);
    }

    [Fact]
    public void Test_GetNetworkMetrics()
    {
        var result = _controller.GetNetworkMetrics(_from, _to);
        _ = Assert.IsAssignableFrom<IActionResult>(result);
    }
}