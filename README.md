# AutoLot_EF

A compact, opinionated data-access sample built with EF Core (code-first) following Andrew Troelsen’s AutoLot examples.  
This repository demonstrates clean separation between entity models and the data access layer, automated migrations & seeding, repository pattern usage, custom database exceptions, and integration tests using xUnit against a local MS SQL Server instance.

## Quick overview
- .NET target: `.NET 10`
- Projects:
  - `AutoLot.Models` — entity classes, owned entities, base entity, view models. Entities are configured with both Data Annotations and Fluent API.
  - `AutoLot.Dal` — EF Core DbContext, migrations, migration helpers (automatic seeding), repository implementations, and custom exceptions for database errors.
  - `AutoLot.Tests` (or similar) — xUnit integration tests that exercise the DAL against a local MS SQL Server database.

## Key design points
- Separation of concerns: models are in `AutoLot.Models`; persistence code is in `AutoLot.Dal`.
- Code-first EF Core approach: entities drive the schema; migrations produce and evolve the database.
- Models:
  - Owned entities used where value objects are appropriate.
  - A `BaseEntity` class provides common properties (e.g., `Id`, `Created`, `Modified`) and is inherited by concrete entities.
  - View models exist for DTO/mapping scenarios.
  - Configured both with attributes (Data Annotations) and Fluent API for full control.
- Data Access Layer:
  - `AutoLotDbContext` (or similarly named) configures entities, relationships, and database objects.
  - Repository pattern encapsulates CRUD operations (single-responsibility & testability).
  - Migration helpers provide deterministic seeding of sample data and can recreate the DB for repeatable tests.
  - `CustomExceptions` wrap and expose database-specific errors (useful for logging, retry logic, and clearer errors in upper layers).
- Testing:
  - Integration tests use xUnit and target a local MS SQL Server instance (LocalDB or SQL Express).
  - Tests rely on migration-based database setup + the provided seeding helpers to ensure deterministic state.

## Requirements
- .NET 10 SDK
- Local MS SQL Server instance:
  - LocalDB: `(localdb)\mssqllocaldb`
  - Or SQL Express: `localhost\SQLEXPRESS`
- EF Core CLI tools (if running migrations locally): `dotnet tool install --global dotnet-ef` (if not already installed)

## Setup & run (typical)
1. Clone the repo:
   - `git clone https://github.com/Abhishek5211/AutoLot_EF.git`
2. Update connection string(s):
   - Add or edit your connection string in the consuming project's `appsettings.json` or in the test project's configuration:
     - Example LocalDB:
       - `Server=(localdb)\\mssqllocaldb;Database=AutoLot;Trusted_Connection=True;MultipleActiveResultSets=true`
     - Example SQL Express:
       - `Server=localhost\\SQLEXPRESS;Database=AutoLot;Trusted_Connection=True;MultipleActiveResultSets=true`
3. Apply migrations (from repository root or the `AutoLot.Dal` project folder):
   - Add migration:  
     `dotnet ef migrations add InitialCreate --project AutoLot.Dal --startup-project <startup-project-if-needed>`
   - Update database:  
     `dotnet ef database update --project AutoLot.Dal --startup-project <startup-project-if-needed>`
   - (If migrations are already checked into source control, only `database update` is required.)
4. Run tests:
   - `dotnet test ./tests/AutoLot.Tests`  
   Tests will use migrations + the migration helpers to create & seed the test database, then run integration tests.

## Typical commands
- Restore & build:
  - `dotnet restore`
  - `dotnet build`
- Run migrations:
  - `dotnet ef migrations add <Name> --project AutoLot.Dal`
  - `dotnet ef database update --project AutoLot.Dal`
- Test:
  - `dotnet test`

## Troubleshooting
- Connection errors:
  - Confirm SQL Server / LocalDB instance is running.
  - Confirm the connection string and credentials (if using SQL auth).
  - Check firewall rules if using a remote DB.
- Migration failures:
  - If schema drift occurs while testing, drop database and re-run `dotnet ef database update`, or use the migration helpers that intentionally drop/create for deterministic test runs.
- CustomExceptions:
  - Look for the DAL's exception types in the `AutoLot.Dal` project — these provide more context than raw SQL or provider exceptions.

## References
- Andrew Troelsen — Entity Framework Core chapters (AutoLot examples)
- EF Core docs: https://learn.microsoft.com/ef/core
- xUnit docs: https://xunit.net
