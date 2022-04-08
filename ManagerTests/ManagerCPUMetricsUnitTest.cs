using Xunit;
using MetricsManagerHW.Controllers;
using System;
using Microsoft.AspNetCore.Mvc;

namespace ManagerTests;

public class ManagerCPUMetricsUnitTest
{
    private CPUMetricsController _controller;
    private int _agentId;
    private TimeSpan _from;
    private TimeSpan _to;

    public ManagerCPUMetricsUnitTest()
    {
        _controller = new();
        _agentId = 1;
        _from = TimeSpan.FromSeconds(1);
        _to = TimeSpan.FromSeconds(10);
    }

    [Fact]
    public void Test_GetCPUMetricsFromAgent()
    {
        var result = _controller.GetCPUMetricsFromAgent(_agentId, _from, _to);
        _ = Assert.IsAssignableFrom<IActionResult>(result);
    }

    [Fact]
    public void Test_GetCPUMetricsPercentileFromAgent()
    {
        int percentile = 90;
        var result = _controller.GetCPUMetricsPercentileFromAgent(_agentId, percentile, _from, _to);
        _ = Assert.IsAssignableFrom<IActionResult>(result);
    }

    [Fact]
    public void Test_GetCPUMetricsFromAllCluster()
    {
        var result = _controller.GetCPUMetricsFromAllCluster(_from, _to);
        _ = Assert.IsAssignableFrom<IActionResult>(result);
    }

    [Fact]
    public void Test_GetCPUMetricsPercentileFromAllCluster()
    {
        int percentile = 90;
        var result = _controller.GetCPUMetricsPercentileFromAllCluster(percentile, _from, _to);
        _ = Assert.IsAssignableFrom<IActionResult>(result);
    }
}