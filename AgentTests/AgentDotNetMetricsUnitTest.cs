using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace AgentTests;

public class AgentDotNetMetricsUnitTest
{
    private DotNetMetricsController _controller;
    private TimeSpan _from;
    private TimeSpan _to;

    public AgentDotNetMetricsUnitTest()
    {
        _controller = new DotNetMetricsController();
        _from = TimeSpan.FromSeconds(1);
        _to = TimeSpan.FromSeconds(10);
    }

    [Fact]
    public void Test_GetDotNetMetrics()
    {
        var result = _controller.GetDotNetMetrics(_from, _to);
        _ = Assert.IsAssignableFrom<IActionResult>(result);
    }
}
