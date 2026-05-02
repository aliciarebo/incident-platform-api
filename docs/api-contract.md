# API Contract — Incident Platform

Base URL: `/api/v1`

## Auth

### POST /auth/login

Purpose: Authenticate a user and return a JWT.

Request

- email: string

- password: string

Response

- accessToken: string (JWT)

- user:

&nbsp; - id: string (guid)

&nbsp; - name: string

&nbsp; - role: Reporter | Agent | Admin

&nbsp; - teamId: string (guid)

Notes

\- MVP: simple credentials (seeded users)

\- Token contains: sub(userId), role, teamId

---

## Incidents

### POST /incidents

Purpose: Create a new incident.

Request

- title: string

- description: string

- priority: Low | Medium | High

- teamId: string (guid)

- category?: string

Response (201)

- id: string (guid)

---

### GET /incidents

Purpose: Retrieve all incidents in the system.
Response(200): array

- id: string (guid)

- title: string

- description: string

- teamId: string (guid)

- priority: Low | Medium | High

- status: Open | InProgress | Resolved | Closed

- reporterId: string (guid)

- assignedToId: string (guid, nullable)

- createdAt: string (ISO 8601)

- updatedAt: string (ISO 8601)

Errors

- 403 Forbidden → user does not have permission to view incidents

---

### GET /incidents/team

Purpose: Team queue (incidents for my team, including unassigned).

Notes

- teamId is derived from JWT (not passed as query)

Query

- status?: Open | InProgress | Resolved | Closed

- priority?: Low | Medium | High

- assigned?: true | false (optional)

Response (200): object

- id

- title

- priority

- status

- teamId

- assignedToId (nullable)

- createdAt

- updatedAt

Errors

- 400 Bad Request → invalid query parameters

---

### GET /incidents/my

Purpose: Incidents assigned to the current user.

Response (200): object

- id

- title

- teamId

- priority

- status

- createdAt

- updatedAt

---

### GET /incidents/{id}

Purpose: Incident detail.

Response (200): object

- id

- title

- description

- teamId

- category (nullable)

- priority

- status

- reporterId

- assignedToId (nullable)

- createdAt

- updatedAt

---

### PATCH /incidents/{id}/assign

Purpose: Assign an incident to a user.

Request

- assignedToId: string (guid)

Rules

- Agent and Admin can assign incidents
- Reporter cannot assign incidents
- An Agent can only assign incidents to themselves
- An Agent can only assign incidents that belong to their own team
- An Admin can assign the incident to any user within the incident team
- Incidents in Resolved or Closed cannot be assigned
- An already assigned incident cannot be reassigned, unless the actor is Admin and the incident is not in Resolved or Closed
- Assigning an incident does not change its status

Response (200): object

- id
- title
- description
- teamId
- category
- priority
- status
- reporterId
- assignedToId
- createdAt
- updatedAt

Errors

- 404 Not Found → incident does not exist
- 403 Forbidden → user does not have permission to assign the incident
- 409 Conflict → assignment violates business rules or conflicts with current incident state
- 400 Bad Request → invalid request payload

---

### PATCH /incidents/{id}/status

Purpose: Change the status of an incident.

Request

- status: Open | InProgress | Resolved | Closed

Rules

- Agent and Admin can change incident status
- Reporter cannot change incident status
- An Agent can only change the status of incidents assigned to them
- An Admin can change the status of any incident
- Status transitions must follow the workflow rules defined in the domain

Response (200) : object

- id
- title
- description
- teamId
- category
- priority
- status
- reporterId
- assignedToId
- createdAt
- updatedAt

Errors

- 404 Not Found → incident does not exist
- 403 Forbidden → user does not have permission to change incident status
- 409 Conflict → status transition is invalid
- 400 Bad Request → invalid request payload

## Conventions

- All dates are in ISO 8601 format
- Enums are serialized as strings
- All endpoints return JSON
- All IDs are UUID (GUID)
