# Library.Management

A **Library Management System Web API** built with **.NET 8**, following **Clean Architecture**, **Entity Framework Core**, **Repository Pattern**, and **Service Layer**.  
Supports full CRUD operations for **Books** and can be extended for other entities like **Authors**.

---

## Prerequisites

- **.NET 8 SDK** or later  
- **SQL Server** (or PostgreSQL, update connection string accordingly)  
- EF Core CLI tools:  

```bash
dotnet tool install --global dotnet-ef
```

---

## Project Structure

Library.Management/
   - Library.Management.Api/ → API Layer (controllers, middleware, Program.cs)
   - Library.Management.Application/ → Application layer (DTOs, services, interfaces, mapping)
   - Library.Management.Domain/ → Domain layer (entities, business logic)
   - Library.Management.Infrastructure/ → Infrastructure layer (DbContext, EF Core, repository implementations)
   - Library.Management.Unit.Testing/ → Unit test cases layer (Unit test cases for services)

---

## Configuration

# Connection String Setup

The application uses Entity Framework Core with SQL Server. Configure the connection string using below code.

Navigate to appsettings.json and update the connection string:
```
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=library_management;Trusted_Connection=true;MultipleActiveResultSets=true"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

Here, put the database connection string for the local database.

---

## Database Migration
# Create Initial Migration

Navigate to the project root and create your first migration:

```dotnet ef migrations add IntialCreate --project Library.Management.Infrastructure --startup-project Library.Management.Api```

## Database Update
# Apply Migrations to Database

```dotnet ef database update --project Library.Management.Infrastructure --startup-project Library.Management.Api```

---

Once the database is set up for this project, you can run the application.

