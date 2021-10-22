using Application.Common.Repository;
using Application.Features.Tracking;
using Moq;
using MediatR;
using System;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;
using Domain.Features.Tracking;

namespace Application.Test.Features.Tracking
{
    public class UpdateTrackerLocationTest
    {
        private UpdateTrackerLocationCommandHandler _sytemUnderTest;

        [SetUp]
        public void Setup()
        {
            var eventRepositoryMock = new Mock<IEventRepository>();
            eventRepositoryMock.Setup(er => er.GetByIdAsync<Tracker>(It.IsAny<Guid>())).ReturnsAsync(new Tracker());
            _sytemUnderTest = new UpdateTrackerLocationCommandHandler(eventRepositoryMock.Object);
        }

        [Test]
        public async Task UpdateTrackerLocationCommandHandler_OnHandle_ReturnsUnitValue()
        {
            var result = await _sytemUnderTest.Handle(new UpdateTrackerLocationCommand(40.1, -32.132, Guid.NewGuid(), DateTime.Now), new CancellationToken());
            Assert.That(result, Is.EqualTo(Unit.Value));
        }
    }
}
