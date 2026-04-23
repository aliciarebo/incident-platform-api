using IncidentPlatform.Domain.Incidents;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncidentPlatform.Tests.Domain.Incidents
{
    public class IncidentTests
    {
        [Fact]
        public void AssignTo_Should_Assign_When_Valid()
        {
            var incident = CreateIncident();
            var userId = Guid.NewGuid();

            incident.AssignTo(userId, isAdmin: false);

            incident.AssignedToId.Should().Be(userId);
            incident.UpdatedAt.Should().BeAfter(incident.CreatedAt);
        }

        [Fact]
        public void AssignTo_Should_Throw_When_Status_Is_Resolved()
        {
            var incident = CreateIncident();
            incident.ChangeStatus(IncidentStatus.InProgress);
            incident.ChangeStatus(IncidentStatus.Resolved);

            var userId = Guid.NewGuid();

            var act = () => incident.AssignTo(userId, isAdmin: false);

            act.Should().Throw<InvalidOperationException>()
                .WithMessage("*cannot be assigned*");
        }

        [Fact]
        public void AssignTo_Should_Throw_When_Status_Is_Closed()
        {
            var incident = CreateIncident();
            incident.ChangeStatus(IncidentStatus.InProgress);
            incident.ChangeStatus(IncidentStatus.Resolved);
            incident.ChangeStatus(IncidentStatus.Closed);

            var userId = Guid.NewGuid();

            var act = () => incident.AssignTo(userId, isAdmin: false);

            act.Should().Throw<InvalidOperationException>()
                .WithMessage("*cannot be assigned*");
        }

        [Fact]
        public void AssignTo_Should_Throw_When_Incident_Is_Already_Assigned_And_User_Is_Not_Admin()
        {
            var incident = CreateIncident();
            incident.AssignTo(Guid.NewGuid(), isAdmin: false);

            var act = () => incident.AssignTo(Guid.NewGuid(), isAdmin: false);

            act.Should().Throw<InvalidOperationException>()
                .WithMessage("*cannot be reassigned*");
        }

        [Fact]
        public void AssignTo_Should_Reassign_When_User_Is_Admin()
        {
            var incident = CreateIncident();
            incident.AssignTo(Guid.NewGuid(), isAdmin: false);

            var newUserId = Guid.NewGuid();

            incident.AssignTo(newUserId, isAdmin: true);

            incident.AssignedToId.Should().Be(newUserId);
        }

        [Fact]
        public void ChangeStatus_Should_Change_Status_When_Transition_Is_Valid()
        {
            var incident = CreateIncident();

            incident.ChangeStatus(IncidentStatus.InProgress);

            incident.Status.Should().Be(IncidentStatus.InProgress);
        }

        [Fact]
        public void ChangeStatus_Should_Throw_When_Transition_Is_Invalid()
        {
            var incident = CreateIncident();

            var act = () => incident.ChangeStatus(IncidentStatus.Closed);

            act.Should().Throw<InvalidOperationException>()
                .WithMessage("*Invalid status transition*");
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
