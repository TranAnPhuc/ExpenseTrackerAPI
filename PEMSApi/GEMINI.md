# PEMSApi - Personal Expense Management System

A .NET 8.0 Web API for managing personal expenses and income, featuring JWT authentication and PostgreSQL integration.

## Project Overview
This project serves as the backend for an Expense Tracker application. It manages transactions and categories, calculating balances and providing paginated transaction history.

### Main Technologies
- **Framework:** .NET 8.0 (ASP.NET Core Web API)
- **Database:** PostgreSQL via [Npgsql.EntityFrameworkCore.PostgreSQL](https://www.npgsql.org/efcore/)
- **ORM:** Entity Framework Core 8.0
- **Authentication:** JWT (JSON Web Token) Bearer Authentication
- **Documentation:** Swagger/OpenAPI (Swashbuckle)

## Architecture
- **Controllers/:** RESTful endpoints for Authentication and Transaction management.
- **Models/:** Data entities representing `Transaction` and `Category`.
  - `Category.Type`: `false` for Income (Thu), `true` for Expense (Chi).
- **Data/:** `AppDbContext` for EF Core and database migrations.

## Building and Running

### Prerequisites
- .NET 8.0 SDK
- PostgreSQL database
- `dotnet-ef` tool (for migrations)

### Configuration
Update the `DefaultConnection` and `Jwt` settings in `appsettings.json` or `appsettings.Development.json` before running.

### Commands
- **Restore Dependencies:** `dotnet restore`
- **Build Project:** `dotnet build`
- **Run API:** `dotnet run`
- **Database Migrations:**
  - Update Database: `dotnet ef database update`
  - Add Migration: `dotnet ef migrations add <MigrationName>`

## Development Conventions
- **Naming:** Follow standard C#/.NET naming conventions (PascalCase for classes and methods).
- **API Versioning:** Currently using default `api/[controller]` routing.
- **Authentication:** Most endpoints (except `Auth/Login`) should typically require a valid JWT token.
- **Data Access:** Use Eager Loading (`.Include(t => t.Category)`) when querying transactions to ensure category details are available.

## Key Files
- `Program.cs`: Application entry point and service configuration.
- `appsettings.json`: Configuration for database connections and JWT keys.
- `Data/AppDbContext.cs`: EF Core database context and seed data.
- `Controllers/TransactionsController.cs`: Main business logic for transaction CRUD and summaries.
