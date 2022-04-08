using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace AgentTests;

public class AgentRAMMetricsUnitTest
{
    private RAMMetricsController _controller;
    private TimeSpan _from;
    private TimeSpan _to;

    public AgentRAMMetricsUnitTest()
    {
        _controller = new RAMMetricsController();
        _from = TimeSpan.FromSeconds(1);
        _to = TimeSpan.FromSeconds(10);
    }

    [Fact]
    public void Test_GetRAMMetrics()
    {
        var result = _controller.GetRAMMetrics(_from, _to);
        _ = Assert.IsAssignableFrom<IActionResult>(result);
    }
}
