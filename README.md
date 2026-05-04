# 🚀 Incident Platform API

Backend API for a **team-based Incident Management Platform** built with .NET 8 and designed following **Clean Architecture (Hexagonal style)** principles.

The system is designed to be **scalable, maintainable, and testable**, keeping business logic completely independent from frameworks and infrastructure.

---

## Architecture Decisions

### Hexagonal Architecture

The domain is completely isolated from frameworks and infrastructure. Business logic is independently testable without spinning up a web server or a database. Dependencies always point inward — Domain knows nothing about Application, and Application knows nothing about Infrastructure.

### CQRS

Each use case is an explicit Command or Query with its own Handler, making the codebase easy to navigate, test, and extend independently.

### Ports & Adapters

Interfaces (ports) are defined in Domain and Application. Concrete implementations (adapters) live in Infrastructure. Swapping `InMemoryIncidentRepository` for a PostgreSQL + EF Core implementation would only require changes inside Infrastructure, with zero impact on Domain or Application layers.

### Password Hashing

Passwords are hashed with BCrypt (via BCrypt.Net-Next) even in the in-memory repository, reflecting production-ready security practices.

### JWT Authentication

Authentication is handled via JWT tokens. The `ICurrentUser` interface is defined in Application, and its concrete implementation `JwtCurrentUser` lives in API, keeping the domain and application layers free from HTTP concerns.

---

## 🛠️ Tech Stack

- **.NET 8** (ASP.NET Core Web API)
- **JWT Authentication**
- **BCrypt.Net-Next** (password hashing)
- **Swagger / OpenAPI**
- **PostgreSQL** _(planned)_
- **Entity Framework Core** _(planned)_

---

## Features (MVP)

### Included

- User authentication with email and password
- Create incident
- Get all incidents
- Get incidents by team
- Get incident by ID
- Assign incident to agent
- Change incident status
- Role-based authorization (Reporter, Agent, Admin)

### Planned

- Comments system
- File attachments
- Audit history
- SLA management
- PostgreSQL persistence via EF Core

---

## Authorization Rules

- Any authenticated user can create incidents
- **Agents** can:
  - Work only on incidents assigned to them
  - Change status of their assigned incidents
- **Admins** can:
  - Assign incidents to agents
  - Change status of any incident
  - Access all incidents

---

## API Endpoints

### Authentication

| Method | Endpoint             | Description                   |
| ------ | -------------------- | ----------------------------- |
| POST   | `/api/v1/auth/login` | Login with email and password |

### Incidents

| Method | Endpoint                             | Auth            | Description               |
| ------ | ------------------------------------ | --------------- | ------------------------- |
| POST   | `/api/v1/incidents`                  | ✅ All roles    | Create a new incident     |
| GET    | `/api/v1/incidents`                  | ✅ All roles    | Get all incidents         |
| GET    | `/api/v1/incidents/{id}`             | ✅ All roles    | Get incident by ID        |
| GET    | `/api/v1/incidents/team?teamId={id}` | ✅ All roles    | Get incidents by team     |
| GET    | `/api/v1/incidents/my`               | ✅ Agent, Admin | Get my assigned incidents |
| PATCH  | `/api/v1/incidents/{id}/assign`      | ✅ Agent, Admin | Assign incident           |
| PATCH  | `/api/v1/incidents/{id}/status`      | ✅ Agent, Admin | Change incident status    |

### HTTP Status Codes

| Code               | Meaning                  |
| ------------------ | ------------------------ |
| `200 OK`           | Successful request       |
| `201 Created`      | Resource created         |
| `400 Bad Request`  | Validation error         |
| `401 Unauthorized` | Missing or invalid token |
| `403 Forbidden`    | Insufficient permissions |
| `404 Not Found`    | Resource not found       |
| `409 Conflict`     | Business rule violation  |

---

## 🚀 Getting Started

### Prerequisites

- .NET 8 SDK

### Run the project

```bash
dotnet restore
dotnet build
dotnet run --project src/IncidentPlatform.API
```

### Swagger

Once running, open your browser at:
http://localhost:5263/swagger/index.html

---

### Test Credentials

Use any of these accounts to log in via `POST /api/v1/auth/login`:

| Role     | Email                         | Password  |
| -------- | ----------------------------- | --------- |
| Admin    | admin@incidentplatform.dev    | Test1234! |
| Agent    | agent@incidentplatform.dev    | Test1234! |
| Reporter | reporter@incidentplatform.dev | Test1234! |

> 💡 Copy the `accessToken` from the login response and use the **Authorize** button in Swagger to authenticate the rest of the requests.

---

## Testing

Tests are organized mirroring the production code structure:

- **Domain tests** — validate business rules directly on entities (e.g. invalid status transitions, reassignment rules)
- **Application tests** — validate use case logic using mocks for `IIncidentRepository` and `ICurrentUser`

Libraries: **xUnit**, **FluentAssertions**, **Moq**
