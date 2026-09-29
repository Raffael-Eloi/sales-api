# Sales API

Backend for the Ambev Developer Evaluation technical challenge: a REST API for managing sales, built with .NET 8, following Clean Architecture with CQRS (MediatR).

## Tech stack

- **.NET 8.0 / C#**
- **PostgreSQL** via **EF Core** (Npgsql provider)
- **MediatR** (CQRS), **AutoMapper**, **FluentValidation**
- **Serilog** (structured logging, console + rolling file)
- **JWT** authentication, **BCrypt** password hashing
- **xUnit**, **Bogus**, **NSubstitute**, **FluentAssertions** for testing

## Project structure

```
src/
  Ambev.DeveloperEvaluation.Domain          # Entities, validators, repository interfaces
  Ambev.DeveloperEvaluation.Application     # MediatR commands/handlers (CQRS)
  Ambev.DeveloperEvaluation.ORM             # EF Core DbContext, mappings, repositories, migrations
  Ambev.DeveloperEvaluation.Common          # Cross-cutting: security, validation, logging, health checks
  Ambev.DeveloperEvaluation.IoC             # Dependency injection composition (module initializers)
  Ambev.DeveloperEvaluation.WebApi          # Controllers, request/response DTOs, Program.cs
tests/
  Ambev.DeveloperEvaluation.Unit            # xUnit unit tests (Domain + Application layers)
  Ambev.DeveloperEvaluation.Integration     # Scaffolded, no tests implemented yet
  Ambev.DeveloperEvaluation.Functional      # Scaffolded, no tests implemented yet
```

## Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/) + Docker Compose (for PostgreSQL, and the MongoDB/Redis services also declared in `docker-compose.yml`, currently unused by the app)
- (Optional, only for creating new migrations) the `dotnet-ef` global tool: `dotnet tool install --global dotnet-ef`

## Configuration

Connection string and JWT secret live in `src/Ambev.DeveloperEvaluation.WebApi/appsettings.json` (`appsettings.Development.json` for the Development environment) and can be overridden with environment variables or user secrets.

**Known issue:** the checked-in `ConnectionStrings:DefaultConnection` in `appsettings.json` is written in SQL Server syntax, but the app is wired to PostgreSQL (`UseNpgsql` in `Program.cs`). Replace it with a Postgres-formatted string matching your database, e.g.:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=developer_evaluation;Username=developer;Password=ev@luAt10n"
}
```

(the `developer_evaluation` / `developer` / `ev@luAt10n` values match the `ambev.developerevaluation.database` service in `docker-compose.yml`).

## Running the application

### Option A — full stack via Docker Compose

```bash
docker-compose up --build
```

This starts the API together with PostgreSQL, MongoDB and Redis containers. The API's exposed ports are randomly published by Docker for `ambev.developerevaluation.webapi` (check `docker-compose ps` for the mapped host port), then open `/swagger` on that port.

### Option B — API locally, database in Docker

1. Start just the database: `docker-compose up ambev.developerevaluation.database`
2. Set `ConnectionStrings:DefaultConnection` (see above) to point at that container's published port.
3. Apply migrations:
   ```bash
   dotnet ef database update --project src/Ambev.DeveloperEvaluation.ORM --startup-project src/Ambev.DeveloperEvaluation.WebApi
   ```
4. Run the API:
   ```bash
   dotnet run --project src/Ambev.DeveloperEvaluation.WebApi/Ambev.DeveloperEvaluation.WebApi.csproj
   ```
   Swagger UI opens automatically at `http://localhost:5119/swagger`.

## Running tests

```bash
dotnet test
```

Only the `Ambev.DeveloperEvaluation.Unit` project has tests implemented (Domain entity/validation rules and Application command handlers). `Integration` and `Functional` are empty project scaffolds.

## Available endpoints

| Method | Route | Description |
|---|---|---|
| POST | `/api/auth` | Authenticate a user, returns a JWT |
| POST | `/api/users` | Create a user |
| GET | `/api/users/{id}` | Get a user by ID |
| DELETE | `/api/users/{id}` | Delete a user |
| POST | `/api/sales` | Create a sale |
| GET | `/api/sales/{id}` | Get a sale by ID |
| PUT | `/api/sales/{id}` | Update a sale |
| PATCH | `/api/sales/{id}/cancel` | Cancel a sale |
| DELETE | `/api/sales/{id}` | Delete a sale |

Full request/response schemas are available in Swagger once the app is running.

## What was implemented

- **Users**: authentication (JWT) and CRUD.
- **Sales**: full CRUD plus cancellation, with `Product` line items.
- **Business rules** (quantity-based discount tiers on `Product`): no discount below 4 items, 10% for 4-9, 20% for 10-20, and quantities above 20 are rejected by validation.
- **Sale lifecycle events**: rather than publishing to a message broker (optional per the challenge brief), `SaleCreated`, `SaleModified` and `SaleCancelled` are recorded via Serilog structured logging in the respective handlers (`CreateSaleHandler`, `UpdateSaleHandler`, `CancelSaleHandler`).
- **Validation**: double-layered FluentValidation (WebApi request DTOs and Application commands), surfaced as a 400 response via `ValidationExceptionMiddleware`.

## Not implemented

- A message bus (Rebus) integration — logging is used instead, as noted above.
- Item-level cancellation (`ItemCancelled` event) and a paginated/filterable "list sales" endpoint.
- A Postman/Insomnia collection.
- Integration and Functional test coverage.
