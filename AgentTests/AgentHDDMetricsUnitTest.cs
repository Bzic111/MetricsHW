using MetricsAgent.Controllers;
using MetricsAgent.Interfaces;
using System;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using MetricsAgent.DAL.Models;

namespace AgentTests
{
    public class AgentHDDMetricsUnitTest
    {
        private HDDMetricsController _controller;
        private DateTime _from;
        private DateTime _to;
        private Mock<IHddMetricsRepository> _mockRepository;
        private Mock<ILogger<HDDMetricsController>> _mockLogger;
        private List<HddMetrics> _responseList;

        public AgentHDDMetricsUnitTest()
        {
            _mockRepository = new();
            _mockLogger = new();
            _controller = new(_mockLogger.Object, _mockRepository.Object);
            _from = DateTime.Now.AddDays(-1);
            _to = DateTime.Now.AddDays(1);
            _responseList = new List<HddMetrics>()
            {
                new (){ Id = 1,Value = 50,DateTime = DateTime.Now},
                new (){ Id = 2,Value = 51,DateTime = DateTime.Now},
                new (){ Id = 3,Value = 52,DateTime = DateTime.Now}
            };
        }

        // CreateMetric
        [Fact]
        public void Test_CreateMetric()
        {
            _mockRepository.Setup(repository => repository.Create(It.IsAny<HddMetrics>())).Verifiable();
            _mockRepository.Verify(repo => repo.Create(It.IsAny<HddMetrics>()), Times.AtMostOnce());
        }

        // GetHDDMetrics
        [Fact]
        public void Test_GetHDDMetrics()
        {
            _mockRepository.Setup(repo => repo.GetByTimeFilter(_from, _to)).Verifiable();
            _mockRepository.Verify(repo => repo.GetByTimeFilter(_from, _to), Times.AtMostOnce());
        }

        // GetAllHDDMetrics
        [Fact]
        public void Test_GetAllHDDMetrics()
        {
            _mockRepository.Setup(repo => repo.GetAll()).Returns(_responseList);
            _mockRepository.Verify(repo => repo.GetAll(), Times.AtMostOnce());
        }

        // GetHDDMetricById
        [Fact]
        public void Test_GetHDDMetricById()
        {
            _mockRepository.Setup(repo => repo.GetById(1)).Verifiable();
            _mockRepository.Verify(repo => repo.GetById(1), Times.AtMostOnce());
        }

        // UpdateMetric
        [Fact]
        public void Test_UpdateMetric()
        {
            _mockRepository.Setup(repo => repo.Update(It.IsAny<HddMetrics>())).Verifiable();
            _mockRepository.Verify(repo => repo.Update(It.IsAny<HddMetrics>()), Times.AtMostOnce());
        }

        // DeleteMetric
        [Fact]
        public void Test_DeleteMetric()
        {
            _mockRepository.Setup(r => r.Delete(1)).Verifiable();
            _mockRepository.Verify(r => r.Delete(1), Times.AtMostOnce());
        }
    }
}