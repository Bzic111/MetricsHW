using MetricsAgent.Controllers;
using MetricsAgent.Models;
using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using MetricsAgent.Interfaces;

namespace AgentTests
{
    public class AgentRAMMetricsUnitTest
    {
        private RAMMetricsController _controller;
        private DateTime _from;
        private DateTime _to;
        private Mock<ILogger<RAMMetricsController>> _mockLogger;
        private Mock<IRamMetricsRepository> _mockRepository;
        private List<RamMetrics> _responseList;

        public AgentRAMMetricsUnitTest()
        {
            _mockRepository = new();
            _mockLogger = new();
            _controller = new(_mockLogger.Object, _mockRepository.Object);
            _from = DateTime.Now.AddDays(-1);
            _to = DateTime.Now.AddDays(1);
            _responseList = new List<RamMetrics>()
            {
                new (){ Id = 1,Value = 50,Time = DateTime.Now},
                new (){ Id = 2,Value = 51,Time = DateTime.Now},
                new (){ Id = 3,Value = 52,Time = DateTime.Now}
            };
        }

        // CreateMetric
        [Fact]
        public void Test_CreateMetric()
        {
            _mockRepository
                .Setup(repository => repository.Create(It.IsAny<RamMetrics>()))
                .Verifiable();
            _mockRepository
                .Verify(repo => repo.Create(It.IsAny<RamMetrics>()), Times.AtMostOnce());
        }

        // GetAll
        [Fact]
        public void Test_GetAllRAMMetrics()
        {
            _mockRepository
                .Setup(repository => repository.GetAll())
                .Returns(_responseList);
            _mockRepository
                .Verify(repository => repository.GetAll(), Times.AtMostOnce());
        }

        // GetById
        [Fact]
        public void Test_GetById()
        {
            _mockRepository
                .Setup(r => r.GetById(1))
                .Verifiable();
            _mockRepository
                .Verify(r => r.GetById(1), Times.AtMostOnce());
        }
        // GetByTimeFilter
        [Fact]
        public void Test_GetRAMMetrics()
        {
            _mockRepository
                .Setup(repo => repo.GetByTimeFilter(_from, _to))
                .Returns(_responseList);
            _mockRepository
                .Verify(repository => repository.GetByTimeFilter(_from, _to), Times.AtMostOnce());
        }
        // Update
        [Fact]
        public void Test_UpdateMetric()
        {
            _mockRepository
                .Setup(repo => repo.Update(It.IsAny<RamMetrics>()))
                .Verifiable();
            _mockRepository
                .Verify(repo => repo.Update(It.IsAny<RamMetrics>()), Times.AtMostOnce());
        }
        // Delete
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