using MetricsAgent.Controllers;
using MetricsAgent.Models;
using MetricsAgent.Interfaces;
using System;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace AgentTests;

public class AgentDotNetMetricsUnitTest
{
    private DotNetMetricsController _controller;
    private DateTime _from;
    private DateTime _to;
    private Mock<IDotNetMetricsRepository> _mockRepository;
    private Mock<ILogger<DotNetMetricsController>> _mockLogger;
    private List<DotNetMetrics> _responseList;

    public AgentDotNetMetricsUnitTest()
    {
        _mockRepository = new();
        _mockLogger = new();
        _controller = new(_mockLogger.Object, _mockRepository.Object);
        _from = DateTime.Now.AddDays(-1);
        _to = DateTime.Now.AddDays(1);
        _responseList = new List<DotNetMetrics>()
        {
            new DotNetMetrics(){ Id = 1,Value = 50,Time = DateTime.Now},
            new DotNetMetrics(){ Id = 2,Value = 51,Time = DateTime.Now},
            new DotNetMetrics(){ Id = 3,Value = 52,Time = DateTime.Now}
        };
    }

    // CreateMetric
    [Fact]
    public void Test_CreateMetric()
    {
        _mockRepository.Setup(repository => repository.Create(It.IsAny<DotNetMetrics>())).Verifiable();
        _mockRepository.Verify(repo => repo.Create(It.IsAny<DotNetMetrics>()), Times.AtMostOnce());
    }

    //GetAllDotNetMetrics
    [Fact]
    public void Test_GetAllDotNetMetrics()
    {
        _mockRepository.Setup(repository => repository.GetAll()).Returns(_responseList);
        _mockRepository.Verify(repository => repository.GetAll(), Times.AtMostOnce());
    }

    // GetDotNetMetrics
    [Fact]
    public void Test_GetDotNetMetrics()
    {
        _mockRepository.Setup(repo => repo.GetByTimeFilter(_from, _to)).Returns(_responseList);
        _mockRepository.Verify(repo => repo.GetByTimeFilter(_from, _to), Times.AtMostOnce);
    }

    // GetDotNetMetricById
    [Fact]
    public void Test_GetDotNetMetricById()
    {
        _mockRepository.Setup(repo => repo.GetById(1)).Verifiable();
        _mockRepository.Verify(repo => repo.GetById(1), Times.AtMostOnce);
    }

    // UpdateMetric
    [Fact]
    public void Test_UpdateMetric()
    {
        _mockRepository.Setup(repo => repo.Update(new() { Id = 1, Value = 1, Time = DateTime.Now })).Verifiable();
        _mockRepository.Verify(repo => repo.Update(new() { Id = 1, Value = 1, Time = DateTime.Now }), Times.AtMostOnce);
    }

    // DeleteMetric
    [Fact]
    public void Test_DeleteMetric()
    {
        _mockRepository.Setup(repo => repo.Delete(1)).Verifiable();
        _mockRepository.Verify(repo => repo.Delete(1), Times.AtMostOnce);
    }
}
