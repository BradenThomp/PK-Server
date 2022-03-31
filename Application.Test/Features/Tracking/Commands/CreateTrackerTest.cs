using Application.Common.Repository;
using Application.Features.Tracking.Commands;
using MediatR;
using Moq;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Test.Features.Tracking.Commands
{
    public class CreateTrackerTest
    {
        [Test]
        public async Task ShouldSucceed_ValidCreateTrackerCommand()
        {
            var repo = new Mock<ITrackerRepository>();
            var mediator = new Mock<IMediator>();
            var handler = new CreateTrackerCommandHandler(repo.Object, mediator.Object);
            var command = new CreateTrackerCommand("1", 40, 40);
            var res = await handler.Handle(command, CancellationToken.None);
            Assert.That(res, Is.EqualTo(Unit.Value));
        }
    }
}
