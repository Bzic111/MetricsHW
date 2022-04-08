using Xunit;
using MetricsManagerHW.Controllers;
using System;
using Microsoft.AspNetCore.Mvc;

namespace ManagerTests;

public class ManagerHDDMetricsUnitTest
{
    private HDDMetricsController _controller;
    private int _agentId;
    private TimeSpan _from;
    private TimeSpan _to;

    public ManagerHDDMetricsUnitTest()
    {
        _controller = new();
        _agentId = 1;
        _from = TimeSpan.FromSeconds(1);
        _to = TimeSpan.FromSeconds(10);
    }

    [Fact]
    public void Test_GetHDDMetricsFromAgent()
    {
        var result = _controller.GetHDDMetricsFromAgent(_agentId, _from, _to);
        _ = Assert.IsAssignableFrom<IActionResult>(result);
    }

    [Fact]
    public void Test_GetHDDMetricsFromAllCluster()
    {
        var result = _controller.GetHDDMetricsFromAllCluster(_from, _to);
        _ = Assert.IsAssignableFrom<IActionResult>(result);
    }
}
