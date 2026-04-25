# Architecture — Incident Platform API

## Purpose

This service provides a team-based Incident Management API for creating, viewing, assigning and updating incidents with clear workflow rules and role-based permissions.

---

## High-level Architecture

The solution follows Clean Architecture to keep business rules independent from frameworks and infrastructure.

### Layers

- **Domain**

- Core business entities and rules (Incident, Team, User, enums/value objects).

- No dependencies on ASP.NET, EF Core or external services.

- **Application**

- Use cases / orchestration (CreateIncident, GetTeamQueue, UpdateIncident, etc.).

- Defines interfaces (ports) for persistence and external concerns (repositories, current user context).

- **Infrastructure**

- Implementations for persistence and external concerns (EF Core repositories, database, auth/JWT, etc.).

- Contains EF Core DbContext and migrations.

- **API**

- ASP.NET Core Web API.

- Controllers/Endpoints map HTTP requests to Application use cases.

- Auth middleware, request validation, and response formatting.

Dependency direction:

Domain ← Application ← Infrastructure ← API

---

## Domain Model (MVP)

### Entities

- **User**

- id, name, email

- role: Reporter | Agent | Admin

- teamId (MVP: a user belongs to one team)

- **Team**

- id, name

- **Incident**

- id, title, description

- priority: Low | Medium | High

- status: Open | InProgress | Resolved | Closed

- teamId

- category (optional)

- reporterId

- assignedToId (nullable)

- createdAt, updatedAt

---

## Workflow Rules (MVP)

### Status transitions

Allowed transitions:

- Open → InProgress

- InProgress → Resolved

- Resolved → Closed

Any other transition must be rejected.

### Status change rules

- Only Agent and Admin can change incident status
- Reporter cannot change incident status
- An Agent can only change the status of incidents assigned to them
- An Admin can change the status of any incident
- Status transitions must follow the workflow rules defined in the Incident aggregate

---

### Assignment rules

- Only Agent and Admin can assign incidents
- Reporter cannot assign incidents
- An Agent can only assign incidents to themselves
- An Agent can only assign incidents that belong to their own team
- An Admin can assign an incident to any user within the incident team
- Incidents in Resolved or Closed cannot be assigned
- An already assigned incident cannot be reassigned, unless the actor is Admin and the incident is not in Resolved or Closed
- Assigning an incident does not change the incident status

---

## Authorization \& Visibility (MVP)

The service uses JWT authentication. The token includes: userId (sub), role, teamId.

### Visibility rules

- **Agent** can view incidents belonging to their team
- **Agent** can view incidents assigned to them
- **Reporter** can view incidents created by them (optional for MVP)
- **Admin** can view all incidents

### Update permissions (field-level)

- **Non-admin users** cannot set `assignedToId` or `teamId`
  - If included in the request, the API must reject with **403 Forbidden**

- **Admin** can update `assignedToId` and `teamId`

---

## API Design

- Team visibility is derived from the JWT
  - `GET /incidents/team` does not accept `teamId` query (prevents cross-team access)

See `docs/api-contract.md` for endpoint details.

---

## Persistence (planned)

- Database: PostgreSQL

- ORM: Entity Framework Core

- Migrations stored in Infrastructure

- Seed data for teams/users for local development

---

## Observability (planned)

- Structured logging (Serilog)

- Health endpoint (`/health`) for readiness checks

---

## Non-goals (MVP)

Out of scope for MVP, planned for later iterations:

- Comments

- Attachments

- Full audit history

- SLAs / escalation rules

- Advanced reassignment workflows
