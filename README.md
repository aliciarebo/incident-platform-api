# incident-platform-api

Backend API for a team-based Incident Management Platform built with .NET .



\##  Overview



This service provides:



\- Creation of incidents

\- Team-based visibility

\- Assignment and workflow management

\- Role-based authorization (Reporter, Agent, Admin)

\- JWT authentication 



The system is designed using Clean Architecture to keep business rules independent from infrastructure and frameworks.



---



\## Architecture



The solution follows a layered architecture:



\- \*\*Domain\*\* → Core business entities and rules

\- \*\*Application\*\* → Use cases and orchestration

\- \*\*Infrastructure\*\* → EF Core, persistence, JWT implementation

\- \*\*API\*\* → ASP.NET Core Web API



See detailed documentation:



\- \[API Contract](docs/api-contract.md)

\- \[Architecture Design](docs/architecture.md)



---

\## Tech Stack



\- .NET 8 (ASP.NET Core Web API)

\- Entity Framework Core (planned)

\- PostgreSQL (planned)

\- JWT Authentication

\- Swagger / OpenAPI



---

\## MVP Scope



Included:

\- Create incidents

\- View team queue

\- View assigned incidents

\- Update incident details

\- Role-based permissions



Out of scope (future):

\- Comments

\- Attachments

\- Audit history

\- SLA management

