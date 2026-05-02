# 🚀 Incident Platform API

Backend API for a **team-based Incident Management Platform** built with .NET and designed following **Clean Architecture principles**.

---

## Overview

This API provides the core functionality for managing incidents within teams, including:

- Incident creation
- Team-based visibility
- Assignment and workflow management
- Role-based authorization (Reporter, Agent, Admin)
- JWT authentication

The system is designed to be **scalable, maintainable, and testable**, keeping business logic independent from frameworks and infrastructure.

---

## Architecture

The project follows **Clean Architecture (Hexagonal style)**:

```
src/
  Domain
  Application
  Infrastructure
  API
```

### Key Principles

- Separation of concerns
- Dependency inversion
- Testable business logic
- Framework-agnostic domain

More details:

- [API Contract](docs/api-contract.md)
- [Architecture Design](docs/architecture.md)

---

## 🛠️ Tech Stack

- **.NET 8** (ASP.NET Core Web API)
- **JWT Authentication**
- **Swagger / OpenAPI**
- **Entity Framework Core** _(planned)_
- **PostgreSQL** _(planned)_

---

## Features (MVP)

### Included

- Create incident
- Get all incidents
- Get incidents by team
- Get incident by ID
- Assign incident
- Change incident status
- User authentication (email + password)

### Planned

- Comments system
- File attachments
- Audit history
- SLA management

---

## Authorization Rules

- Any authenticated user can create incidents
- Agents can:
  - Work only on incidents assigned to them
  - Change status of their assigned incidents
- Admins can:
  - Assign incidents
  - Change status of any incident
  - Access all incidents

---

## API Design

- RESTful endpoints
- Consistent error handling
- Proper HTTP status codes:
  - `200 OK`
  - `201 Created`
  - `400 Bad Request`
  - `403 Forbidden`
  - `404 Not Found`
  - `409 Conflict`

---

## API Endpoints

### Authentication

- `POST /auth/login` → User login with email and password

### Incidents

- `POST /incidents` → Create a new incident
- `GET /incidents` → Get all incidents
- `GET /incidents/{id}` → Get incident by ID
- `GET /incidents/team/{teamId}` → Get incidents by team
- `PATCH /incidents/{id}/assign` → Assign incident
- `PATCH /incidents/{id}/status` → Change incident status

---

## Testing

- Unit tests with **xUnit**
- Focus on **business rules validation**
- Independent from infrastructure

---

## Getting Started

### Prerequisites

- .NET 8 SDK

### Run the project

```bash
dotnet restore
dotnet build
dotnet run

```
