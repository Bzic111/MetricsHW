using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace AgentTests;

public class AgentHDDMetricsUnitTest
{
    private HDDMetricsController _controller;
    private TimeSpan _from;
    private TimeSpan _to;

    public AgentHDDMetricsUnitTest()
    {
        _controller = new HDDMetricsController();
        _from = TimeSpan.FromSeconds(1);
        _to = TimeSpan.FromSeconds(10);
    }

    [Fact]
    public void Test_GetHDDMetrics()
    {
        var result = _controller.GetHDDMetrics(_from, _to);
        _ = Assert.IsAssignableFrom<IActionResult>(result);
    }
}
