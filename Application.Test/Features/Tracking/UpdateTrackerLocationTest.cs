using Application.Common.Repository;
using Moq;
using MediatR;
using System;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;
//using Domain.Aggregates;
using Application.Common.Notifications;
//using Application.Features.Tracking.Commands;

namespace Application.Test.Features.Tracking
{
    /**public class UpdateTrackerLocationTest
    {
        private UpdateTrackerLocationCommandHandler _sytemUnderTest;

        [SetUp]
        public void Setup()
        {
            var eventRepositoryMock = new Mock<IEventRepository>();
            var notificationServiceMock = new Mock<INotificationService>();
            eventRepositoryMock.Setup(er => er.GetByIdAsync<Tracker>(It.IsAny<string>())).ReturnsAsync(Tracker.RegisterTracker("00:00:5e:00:53:af"));
            _sytemUnderTest = new UpdateTrackerLocationCommandHandler(eventRepositoryMock.Object, notificationServiceMock.Object);
        }

        [Test]
        public async Task UpdateTrackerLocationCommandHandler_OnHandle_ReturnsUnitValue()
        {
            var result = await _sytemUnderTest.Handle(new UpdateTrackerLocationCommand(40.1, -32.132, "00:00:5e:00:53:af", DateTime.Now), new CancellationToken());
            Assert.That(result, Is.EqualTo(Unit.Value));
        }
    }**/
}
