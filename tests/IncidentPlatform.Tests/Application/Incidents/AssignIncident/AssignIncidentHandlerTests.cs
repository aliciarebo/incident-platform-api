using FluentAssertions;
using IncidentPlatform.Application.Auth;
using IncidentPlatform.Application.Incidents.AssignIncident;
using IncidentPlatform.Domain.Incidents;
using IncidentPlatform.Domain.Ports;
using IncidentPlatform.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace IncidentPlatform.Tests.Application.Incidents.AssignIncident
{
    public class AssignIncidentHandlerTests
    {
        [Fact]
        public async Task HandleAsync_Should_Assign_Incident_When_Agent_Assigns_To_Self()
        {
            var incident = CreateIncident();
            var currentUserId = Guid.NewGuid();
            var teamId = incident.TeamId;

            var repositoryMock = new Mock<IIncidentRepository>();
            repositoryMock
                .Setup(r => r.GetByIdAsync(incident.Id))
                .ReturnsAsync(incident);

            repositoryMock
                .Setup(r => r.UpdateAsync(incident))
                .Returns(Task.CompletedTask);

            var currentUserMock = new Mock<ICurrentUser>();
            currentUserMock.SetupGet(x => x.UserId).Returns(currentUserId);
            currentUserMock.SetupGet(x => x.Role).Returns(UserRole.Agent);
            currentUserMock.SetupGet(x => x.TeamId).Returns(teamId);

            var handler = new AssignIncidentHandler(repositoryMock.Object, currentUserMock.Object);
            var command = new AssignIncidentCommand(incident.Id, currentUserId);

            var result = await handler.HandleAsync(command);

            result.AssignedToId.Should().Be(currentUserId);
            incident.AssignedToId.Should().Be(currentUserId);
            repositoryMock.Verify(r => r.UpdateAsync(incident), Times.Once);
        }

        [Fact]
        public async Task HandleAsync_Should_Throw_When_Agent_Assigns_To_Other_User()
        {
            var incident = CreateIncident();
            var currentUserId = Guid.NewGuid();
            var otherUserId = Guid.NewGuid();

            var repositoryMock = new Mock<IIncidentRepository>();
            repositoryMock
                .Setup(r => r.GetByIdAsync(incident.Id))
                .ReturnsAsync(incident);

            var currentUserMock = new Mock<ICurrentUser>();
            currentUserMock.SetupGet(x => x.UserId).Returns(currentUserId);
            currentUserMock.SetupGet(x => x.Role).Returns(UserRole.Agent);
            currentUserMock.SetupGet(x => x.TeamId).Returns(incident.TeamId);

            var handler = new AssignIncidentHandler(repositoryMock.Object, currentUserMock.Object);
            var command = new AssignIncidentCommand(incident.Id, otherUserId);

            var act = async () => await handler.HandleAsync(command);

            await act.Should().ThrowAsync<UnauthorizedAccessException>();
        }

        [Fact]
        public async Task HandleAsync_Should_Throw_When_User_Is_Reporter()
        {
            var incident = CreateIncident();

            var repositoryMock = new Mock<IIncidentRepository>();
            repositoryMock
                .Setup(r => r.GetByIdAsync(incident.Id))
                .ReturnsAsync(incident);

            var currentUserMock = new Mock<ICurrentUser>();
            currentUserMock.SetupGet(x => x.UserId).Returns(Guid.NewGuid());
            currentUserMock.SetupGet(x => x.Role).Returns(UserRole.Reporter);
            currentUserMock.SetupGet(x => x.TeamId).Returns(incident.TeamId);

            var handler = new AssignIncidentHandler(repositoryMock.Object, currentUserMock.Object);
            var command = new AssignIncidentCommand(incident.Id, Guid.NewGuid());

            var act = async () => await handler.HandleAsync(command);

            await act.Should().ThrowAsync<UnauthorizedAccessException>();
        }

        private static Incident CreateIncident()
        {
            return new Incident(
                Guid.NewGuid(),
                "Test incident",
                "Test description",
                IncidentPriority.Medium,
                Guid.NewGuid(),
                Guid.NewGuid()
            );
        }
    }
}
