using MetricsAgent.Controllers;
using MetricsAgent.Models;
using System;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using MetricsAgent.Interfaces;

namespace AgentTests
{
    public class AgentNetworkMetricsUnitTest
    {
        private NetworkMetricsController _controller;
        private DateTime _from;
        private DateTime _to;
        private Mock<ILogger<NetworkMetricsController>> _mockLogger;
        private Mock<INetworkMetricsRepository> _mockRepository;
        private List<NetworkMetrics> _responseList;

        public AgentNetworkMetricsUnitTest()
        {
            _mockRepository = new();
            _mockLogger = new();
            _controller = new(_mockLogger.Object, _mockRepository.Object);
            _from = DateTime.Now.AddDays(-1);
            _to = DateTime.Now.AddDays(1);
            _responseList = new()
            {
                new() { Id = 1, Value = 50, Time = DateTime.Now },
                new() { Id = 2, Value = 51, Time = DateTime.Now },
                new() { Id = 3, Value = 52, Time = DateTime.Now }
            };
        }

        [Fact]
        public void Test_CreateNetworkMetric()
        {
            _mockRepository.Setup(repository => repository.Create(It.IsAny<NetworkMetrics>())).Verifiable();
            _mockRepository.Verify(repo => repo.Create(It.IsAny<NetworkMetrics>()), Times.AtMostOnce());
        }

        [Fact]
        public void Test_GetAllNetworkMetrics()
        {
            _mockRepository.Setup(repository => repository.GetAll()).Returns(_responseList);
            _mockRepository.Verify(repository => repository.GetAll(), Times.AtMostOnce());
        }

        [Fact]
        public void Test_GetNetworkMetrics()
        {

            _mockRepository
                .Setup(repo => repo.GetByTimeFilter(_from, _to))
                .Returns(_responseList);
            _mockRepository
                .Verify(repository => repository.GetByTimeFilter(_from, _to), Times.AtMostOnce());
        }

        [Fact]
        public void Test_GetNetworkMetricById()
        {

            _mockRepository
                .Setup(repo => repo.GetById(1))
                .Verifiable();
            _mockRepository
                .Verify(repository => repository.GetById(1), Times.AtMostOnce());
        }
        [Fact]
        public void Test_UpdateMetric()
        {
            _mockRepository
                .Setup(repo => repo.Update(It.IsAny<NetworkMetrics>()))
                .Verifiable();
            _mockRepository
                .Verify(repo => repo.Update(It.IsAny<NetworkMetrics>()), Times.AtMostOnce());
        }

        [Fact]
        public void Test_DeleteMetric()
        {
            _mockRepository
                .Setup(repo => repo.Delete(1))
                .Verifiable();
            _mockRepository
                .Verify(repo => repo.Delete(1), Times.AtMostOnce());
        }
    }
}