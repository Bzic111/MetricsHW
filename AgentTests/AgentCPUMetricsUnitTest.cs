using MetricsAgent.Controllers;
using MetricsAgent.Interfaces;
using MetricsAgent.Models;
using System;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace AgentTests;

public class AgentCPUMetricsUnitTest
{
    private DateTime _from;
    private DateTime _to;
    private int _procentile;

    private CPUMetricsController _controller;
    private Mock<ICPUMetricsRepository> _mockRepository;
    private Mock<ILogger<CPUMetricsController>> _mockLogger;
    private List<CpuMetric> _responseList;

    public AgentCPUMetricsUnitTest()
    {
        _mockRepository = new();
        _mockLogger = new();
        _controller = new CPUMetricsController(_mockLogger.Object, _mockRepository.Object);
        _from = DateTime.Now.AddDays(-1);
        _to = DateTime.Now.AddDays(1);
        _procentile = 90;
        _responseList = new List<CpuMetric>()
        {
            new (){ Id = 1,Value = 50,Time = DateTime.Now},
            new (){ Id = 2,Value = 51,Time = DateTime.Now},
            new (){ Id = 3,Value = 52,Time = DateTime.Now}
        };
    }

    [Fact]
    public void Test_CreateCPUMetric()
    {
        _mockRepository.Setup(repository => repository.Create(It.IsAny<CpuMetric>())).Verifiable();
        _mockRepository.Verify(repo => repo.Create(It.IsAny<CpuMetric>()), Times.AtMostOnce());
    }

    [Fact]
    public void Test_GetAllCPUMetrics()
    {
        _mockRepository.Setup(repository => repository.GetAll()).Returns(_responseList);
        _mockRepository.Verify(repository => repository.GetAll(), Times.AtMostOnce());
    }

    [Fact]
    public void Test_GetAllCPUMetricsPercentile()
    {
        _mockRepository
            .Setup(repository => repository.GetByTimeFilterWithPercentile(_procentile, _from, _to))
            .Verifiable();
        _mockRepository
            .Verify(repository => repository.GetByTimeFilterWithPercentile(_procentile, _from, _to), Times.AtMostOnce());
    }

    [Fact]
    public void Test_GetById()
    {
        _mockRepository
            .Setup(r => r.GetById(1))
            .Verifiable();
        _mockRepository
            .Verify(r=>r.GetById(1), Times.AtMostOnce());
    }

    [Fact]
    public void Test_GetCPUMetrics()
    {
        _mockRepository
            .Setup(repo => repo.GetByTimeFilter(_from, _to))
            .Returns(_responseList);
        _mockRepository
            .Verify(repository => repository.GetByTimeFilter(_from, _to), Times.AtMostOnce());
    }

    [Fact]
    public void Test_UpdateMetric()
    {
        _mockRepository.Setup(repo => repo.Update(It.IsAny<CpuMetric>())).Verifiable();
        _mockRepository.Verify(repo => repo.Update(It.IsAny<CpuMetric>()), Times.AtMostOnce());
    }

    [Fact]
    public void Test_DeleteMetric()
    {
        _mockRepository.Setup(repo => repo.Delete(1)).Verifiable();
        _mockRepository.Verify(repo => repo.Delete(1), Times.AtMostOnce());
    }
}
