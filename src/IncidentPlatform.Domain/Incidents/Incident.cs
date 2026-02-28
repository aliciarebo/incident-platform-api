
using System;
using static System.Net.Mime.MediaTypeNames;

namespace IncidentPlatform.Domain.Incidents
{
    public class Incident
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public IncidentPriority Priority { get; private set; }
        public Guid TeamId { get; private set; }
        public string? Category { get; private set; }
        public IncidentStatus Status { get; private set; }
        public Guid ReporterId { get; private set; }

        public Guid? AssignedToId { get; private set; }

        public DateTimeOffset CreatedAt { get; private set; }

        public DateTimeOffset UpdatedAt { get; private set; }

        public Incident(Guid id, string title, string description, IncidentPriority priority, Guid teamId, Guid reporterId, string? category = null)
        {
            if (id == Guid.Empty) throw new ArgumentException("Id is required", nameof(id));
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Title is required", nameof(title));
            if (teamId == Guid.Empty) throw new ArgumentException("TeamId is required", nameof(teamId));
            if (reporterId == Guid.Empty) throw new ArgumentException("ReporterId is required", nameof(reporterId));

            Id = id;
            Title = title;
            Description = description;
            Priority = priority;
            TeamId = teamId;
            Category = category;
            Status = IncidentStatus.Open;
            ReporterId = reporterId;
            AssignedToId = null;
            CreatedAt = DateTimeOffset.UtcNow;
            UpdatedAt = CreatedAt;
        }

        public void AssignTo(Guid userId)
        {
            AssignedToId = userId;
            UpdatedAt = DateTimeOffset.UtcNow;

        }

        public void ChangeStatus(IncidentStatus newStatus)
        {
            if (Status == newStatus)
                return;
            if (!IsValidTransition(Status, newStatus))
                throw new InvalidOperationException($"Invalid status transition: {Status} -> {newStatus}");
            Status = newStatus;
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        private static bool IsValidTransition(IncidentStatus currentStatus, IncidentStatus newStatus) =>
        (currentStatus == IncidentStatus.Open && newStatus == IncidentStatus.InProgress) ||
        (currentStatus == IncidentStatus.InProgress && newStatus == IncidentStatus.Resolved) ||
        (currentStatus == IncidentStatus.Resolved && newStatus == IncidentStatus.Closed);
    }


}
