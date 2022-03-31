using Application.Common.Notifications;
using Application.Common.Repository;
using Application.Common.Services;
using Application.Features.Tracking.Commands;
using Domain.Models;
using MediatR;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Test.Features.Tracking.Commands
{
    public class UpdateTrackerTest
    {
        [Test]
        public async Task ShouldSucceed_ValidUpdateTrackerCommand()
        {
            var trackerRepo = new Mock<ITrackerRepository>();
            var tracker1 = new Tracker()
            {
                HardwareId = "1",
                LastUpdate = DateTime.Now,
                Location = new Location(0, 0)
            };
            trackerRepo.Setup(r => r.GetAsync("1")).Returns(Task.FromResult(tracker1));
            var speakerRepo = new Mock<ISpeakerRepository>();
            var rentalRepo = new Mock<IRentalRepository>();
            var notificationService = new Mock<INotificationService>();
            var emailService = new Mock<IEmailService>();
            var handler = new UpdateTrackerCommandHandler(trackerRepo.Object, speakerRepo.Object, rentalRepo.Object, notificationService.Object, emailService.Object);
            var command = new UpdateTrackerCommand("1", 40, 40);
            var res = await handler.Handle(command, CancellationToken.None);
            Assert.That(res, Is.EqualTo(Unit.Value));
        }

        [Test]
        public async Task ShouldSendNotification_TrackerIsAssignedToSpeaker()
        {
            var trackerRepo = new Mock<ITrackerRepository>();
            var tracker1 = new Tracker()
            {
                HardwareId = "1",
                LastUpdate = DateTime.Now,
                Location = new Location(0, 0),
            };
            var speaker1 = new Speaker("sn-1", "Model");
            speaker1.AttachTracker(tracker1);
            speaker1.ReachedDestination = true;
            trackerRepo.Setup(r => r.GetAsync("1")).Returns(Task.FromResult(tracker1));
            var speakerRepo = new Mock<ISpeakerRepository>();
            speakerRepo.Setup(s => s.GetAsync("sn-1")).Returns(Task.FromResult(speaker1));
            var rentalRepo = new Mock<IRentalRepository>();
            var notificationService = new Mock<INotificationService>();
            var emailService = new Mock<IEmailService>();
            var handler = new UpdateTrackerCommandHandler(trackerRepo.Object, speakerRepo.Object, rentalRepo.Object, notificationService.Object, emailService.Object);
            var command = new UpdateTrackerCommand("1", 40, 40);
            var res = await handler.Handle(command, CancellationToken.None);
            notificationService.Verify(s => s.Notify(It.IsAny<Common.Notifications.INotification>()), Times.Once);
        }

        [Test]
        public async Task ShouldSendEmail_TrackerIsAssignedToSpeakerAndHasReachedTheRentalVenue()
        {
            var trackerRepo = new Mock<ITrackerRepository>();
            var tracker1 = new Tracker()
            {
                HardwareId = "1",
                LastUpdate = DateTime.Now,
                Location = new Location(20, 20),
            };
            var speaker1 = new Speaker("sn-1", "Model");
            speaker1.AttachTracker(tracker1);
            var rental = new Rental(new List<Speaker>() { speaker1 }, null, DateTime.Now, DateTime.Now, DateTime.Now, new Venue(new Location(40, 40), "2500 University Dr NW", "Calgary", "Alberta", "T2N1N4"));
            trackerRepo.Setup(r => r.GetAsync("1")).Returns(Task.FromResult(tracker1));
            var speakerRepo = new Mock<ISpeakerRepository>();
            speakerRepo.Setup(s => s.GetAsync("sn-1")).Returns(Task.FromResult(speaker1));
            var rentalRepo = new Mock<IRentalRepository>();
            rentalRepo.Setup(r => r.GetAsync(It.IsAny<Guid?>())).Returns(Task.FromResult(rental));
            var notificationService = new Mock<INotificationService>();
            var emailService = new Mock<IEmailService>();
            var handler = new UpdateTrackerCommandHandler(trackerRepo.Object, speakerRepo.Object, rentalRepo.Object, notificationService.Object, emailService.Object);
            var command = new UpdateTrackerCommand("1", 40, 40);
            var res = await handler.Handle(command, CancellationToken.None);
            emailService.Verify(s => s.MailAll(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task ShouldNotSendEmail_TrackerIsAssignedToSpeakerAndHasNotReachedTheRentalVenue()
        {
            var trackerRepo = new Mock<ITrackerRepository>();
            var tracker1 = new Tracker()
            {
                HardwareId = "1",
                LastUpdate = DateTime.Now,
                Location = new Location(20, 20),
            };
            var speaker1 = new Speaker("sn-1", "Model");
            speaker1.AttachTracker(tracker1);
            var rental = new Rental(new List<Speaker>() { speaker1 }, null, DateTime.Now, DateTime.Now, DateTime.Now, new Venue(new Location(40, 40), "2500 University Dr NW", "Calgary", "Alberta", "T2N1N4"));
            trackerRepo.Setup(r => r.GetAsync("1")).Returns(Task.FromResult(tracker1));
            var speakerRepo = new Mock<ISpeakerRepository>();
            speakerRepo.Setup(s => s.GetAsync("sn-1")).Returns(Task.FromResult(speaker1));
            var rentalRepo = new Mock<IRentalRepository>();
            rentalRepo.Setup(r => r.GetAsync(It.IsAny<Guid?>())).Returns(Task.FromResult(rental));
            var notificationService = new Mock<INotificationService>();
            var emailService = new Mock<IEmailService>();
            var handler = new UpdateTrackerCommandHandler(trackerRepo.Object, speakerRepo.Object, rentalRepo.Object, notificationService.Object, emailService.Object);
            var command = new UpdateTrackerCommand("1", 20, 20);
            var res = await handler.Handle(command, CancellationToken.None);
            emailService.Verify(s => s.MailAll(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Test]
        public async Task ShouldNotSendEmail_TrackerIsAssignedToSpeakerAndHasAlreadyReachedTheRentalVenue()
        {
            var trackerRepo = new Mock<ITrackerRepository>();
            var tracker1 = new Tracker()
            {
                HardwareId = "1",
                LastUpdate = DateTime.Now,
                Location = new Location(40, 40),
            };
            var speaker1 = new Speaker("sn-1", "Model");
            speaker1.AttachTracker(tracker1);
            speaker1.ReachedDestination = true;
            var rental = new Rental(new List<Speaker>() { speaker1 }, null, DateTime.Now, DateTime.Now, DateTime.Now, new Venue(new Location(40, 40), "2500 University Dr NW", "Calgary", "Alberta", "T2N1N4"));
            trackerRepo.Setup(r => r.GetAsync("1")).Returns(Task.FromResult(tracker1));
            var speakerRepo = new Mock<ISpeakerRepository>();
            speakerRepo.Setup(s => s.GetAsync("sn-1")).Returns(Task.FromResult(speaker1));
            var rentalRepo = new Mock<IRentalRepository>();
            rentalRepo.Setup(r => r.GetAsync(It.IsAny<Guid?>())).Returns(Task.FromResult(rental));
            var notificationService = new Mock<INotificationService>();
            var emailService = new Mock<IEmailService>();
            var handler = new UpdateTrackerCommandHandler(trackerRepo.Object, speakerRepo.Object, rentalRepo.Object, notificationService.Object, emailService.Object);
            var command = new UpdateTrackerCommand("1", 40, 40);
            var res = await handler.Handle(command, CancellationToken.None);
            emailService.Verify(s => s.MailAll(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }
    }
}
