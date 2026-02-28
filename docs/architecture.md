\# Architecture — Incident Platform API



\## Purpose

This service provides a team-based Incident Management API for creating, viewing, assigning and updating incidents with clear workflow rules and role-based permissions.



---



\## High-level Architecture

The solution follows Clean Architecture to keep business rules independent from frameworks and infrastructure.



Layers:



\- \*\*Domain\*\*

&nbsp; - Core business entities and rules (Incident, Team, User, enums/value objects).

&nbsp; - No dependencies on ASP.NET, EF Core or external services.



\- \*\*Application\*\*

&nbsp; - Use cases / orchestration (CreateIncident, GetTeamQueue, UpdateIncident, etc.).

&nbsp; - Defines interfaces (ports) for persistence and external concerns (repositories, current user context).



\- \*\*Infrastructure\*\*

&nbsp; - Implementations for persistence and external concerns (EF Core repositories, database, auth/JWT, etc.).

&nbsp; - Contains EF Core DbContext and migrations.



\- \*\*API\*\*

&nbsp; - ASP.NET Core Web API.

&nbsp; - Controllers/Endpoints map HTTP requests to Application use cases.

&nbsp; - Auth middleware, request validation, and response formatting.



Dependency direction:

Domain ← Application ← Infrastructure ← API



---



\## Domain Model (MVP)

\### Entities

\- \*\*User\*\*

&nbsp; - id, name, email

&nbsp; - role: Reporter | Agent | Admin

&nbsp; - teamId (MVP: a user belongs to one team)



\- \*\*Team\*\*

&nbsp; - id, name



\- \*\*Incident\*\*

&nbsp; - id, title, description

&nbsp; - priority: Low | Medium | High

&nbsp; - status: OPEN | IN\_PROGRESS | RESOLVED | CLOSED

&nbsp; - teamId

&nbsp; - category (optional)

&nbsp; - reporterId

&nbsp; - assignedToId (nullable)

&nbsp; - createdAt, updatedAt



---



\## Workflow Rules (MVP)

\### Status transitions

Allowed transitions:

\- OPEN → IN\_PROGRESS

\- IN\_PROGRESS → RESOLVED

\- RESOLVED → CLOSED



Any other transition must be rejected.



\### Assignment

\- An incident can be unassigned initially.



---



\## Authorization \& Visibility (MVP)

The service uses JWT authentication. The token includes: userId (sub), role, teamId.



\### Visibility rules

\- \*\*Agent\*\* can view incidents belonging to their team.

\- \*\*Agent\*\* can view incidents assigned to them.

\- \*\*Reporter\*\* can view incidents created by them (optional for MVP; can be added later).

\- \*\*Admin\*\* can view all incidents.



\### Update permissions (field-level)

\- \*\*Non-admin users\*\* cannot set `assignedToId` or `teamId`.

&nbsp; - If included in the update request, the API must reject with \*\*403 Forbidden\*\*.

\- \*\*Admin\*\* can update `assignedToId` and `teamId`.



---



\## API Design

The API follows an "intent-based" design:

\- A single update endpoint supports the UI "Edit \& Save" workflow:

&nbsp; - `PATCH /incidents/{id}` (partial update)

\- Team visibility is derived from the JWT:

&nbsp; - `GET /incidents/team` does not accept teamId query (prevents cross-team access)



See `docs/api-contract.md` for endpoint details.



---



\## Persistence (planned)

\- Database: PostgreSQL

\- ORM: Entity Framework Core

\- Migrations stored in Infrastructure

\- Seed data for teams/users for local development



---



\## Observability (planned)

\- Structured logging (Serilog)

\- Health endpoint (`/health`) for readiness checks



---



\## Non-goals (MVP)

Out of scope for MVP, planned for later iterations:

\- Comments

\- Attachments

\- Full audit history

\- SLAs / escalation rules

\- Advanced reassignment workflows

