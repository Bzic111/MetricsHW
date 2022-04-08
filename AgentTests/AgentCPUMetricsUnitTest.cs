using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace AgentTests;

public class AgentCPUMetricsUnitTest
{
    private CPUMetricsController _controller;
    private TimeSpan _from;
    private TimeSpan _to;
    private int _procentile;

    public AgentCPUMetricsUnitTest()
    {
        _controller = new CPUMetricsController();
        _from = TimeSpan.FromSeconds(1);
        _to = TimeSpan.FromSeconds(10);
        _procentile = 90;
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