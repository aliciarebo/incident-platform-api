\# API Contract — Incident Platform



Base URL: `/api/v1`



\## Auth



\### POST /auth/login

Purpose: Authenticate a user and return a JWT.



Request

\- email: string

\- password: string



Response

\- accessToken: string (JWT)

\- user:

&nbsp; - id: string (guid)

&nbsp; - name: string

&nbsp; - role: Reporter | Agent | Admin

&nbsp; - teamId: string (guid)



Notes

\- MVP: simple credentials (seeded users)

\- Token contains: sub(userId), role, teamId



---



\## Incidents



\### POST /incidents

Purpose: Create a new incident.



Request

\- title: string

\- description: string

\- priority: Low | Medium | High

\- teamId: string (guid)

\- category?: string





Response (201)

\- id: string (guid)

\- status: OPEN

\- assignedToId: string | null

\- createdAt: string (ISO)

\- updatedAt: string (ISO)



\### GET /incidents/team

Purpose: Team queue (incidents for my team, including unassigned).



Notes

\- teamId is derived from JWT (not passed as query)



Query

\- status?: OPEN | IN\_PROGRESS | RESOLVED | CLOSED

\- priority?: Low | Medium | High

\- assigned?: true | false   (optional)



Response (200): array

\- id

\- title

\- priority

\- status

\- teamId

\- assignedToId (nullable)

\- createdAt

\- updatedAt



---

\### GET /incidents/my

Purpose: Incidents assigned to the current user.



Response (200): array 

\- id

\- title

\- teamId

\- priority

\- status

\- createdAt

\- updatedAt

---



\### GET /incidents/{id}

Purpose: Incident detail.



Response (200)

\- id

\- title

\- description

\- teamId

\- category (nullable)

\- priority

\- status

\- reporterId

\- assignedToId (nullable)

\- createdAt

\- updatedAt



---

\### PATCH /incidents/{id}

Purpose: Edit incident fields in a single "Save" action from the UI.



\- title?

\- description?

\- priority?

\- category?

\- status?

\- teamId? (Admin only)

\- assignedToId?(Admin only)



Response (200)

\- id

\- title

\- description

\- teamId

\- priority

\- status

\- category

\- reporterId

\- assignedToId (nullable)

\- createdAt

\- updatedAt



Notes

\- Non-admin users cannot set assignedToId or teamId (request should be rejected with 403).





