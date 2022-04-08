using Xunit;
using MetricsManagerHW.Controllers;
using System;
using Microsoft.AspNetCore.Mvc;

namespace ManagerTests;

public class ManagerDotNetMetricsUnitTest
{
    private DotNetMetricsController _controller;
    private int _agentId;
    private TimeSpan _from;
    private TimeSpan _to;

    public ManagerDotNetMetricsUnitTest()
    {
        _controller = new();
        _agentId = 1;
        _from = TimeSpan.FromSeconds(1);
        _to = TimeSpan.FromSeconds(10);
    }

    [Fact]
    public void Test_GetDotNetMetricsFromAgent()
    {
        var result = _controller.GetDotNetMetricsFromAgent(_agentId, _from, _to);
        _ = Assert.IsAssignableFrom<IActionResult>(result);
    }

    [Fact]
    public void Test_GetDotNetMetricsFromAllCluster()
    {
        var result = _controller.GetDotNetMetricsFromAllCluster(_from, _to);
        _ = Assert.IsAssignableFrom<IActionResult>(result);
    }
}
