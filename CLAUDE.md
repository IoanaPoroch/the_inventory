# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

**TheInventory** is an ASP.NET Core 8 inventory management system built with a 4-layer architecture. The project is in early development — domain models and EF Core are set up, but RepositoryLayer and ServiceLayer are not yet implemented.

## Build & Run

```powershell
# Build entire solution
dotnet build

# Run the WebAPI (Swagger UI available at http://localhost:5212/swagger)
dotnet run --project WebAPI

# Apply EF Core migrations (once connection string is configured)
dotnet ef database update --project DomainLayer --startup-project WebAPI
```

There are currently no test projects.

## Architecture

```
WebAPI → ServiceLayer → RepositoryLayer → DomainLayer
```

- **DomainLayer** — Entity models, `ApplicationDbContext`, EF Core 8. All entities inherit `BaseEnity` (note: intentional typo in the existing class name).
- **RepositoryLayer** — Intended for repository pattern implementations. Currently empty.
- **ServiceLayer** — Intended for business logic. Currently empty.
- **WebAPI** — ASP.NET Core controllers. Currently only has the scaffold `WeatherForecast` controller; no real endpoints yet.

**Project references are not yet wired.** The layers do not reference each other — this needs to be set up before implementing repositories and services.

## Domain Model

Five entities in `DomainLayer/Models/`:

| Entity | Key Fields |
|---|---|
| `Product` | Name, Color, MadeIn, Price, WarehouseId, SupplierId |
| `Warehouse` | Name, Address |
| `Supplier` | Name, Address |
| `StockLevels` | ProductId, WarehuseId (typo), Quantity |
| `InventoryMovements` | Type (enum: StockIn/StockOut/Transfer), ProductId, ProductQuantity, Date, From, To |

All entities inherit `BaseEnity` (Id: int).

`ApplicationDbContext` lives in `DomainLayer/Data/` and has `DbSet<>` for all five entities. It is not yet registered in WebAPI's DI container and no connection string exists.

## Known Issues in Current Code

- `Supplier.Name` and `Supplier.Address` are typed as `int` instead of `string`
- `StockLevels.WarehuseId` is a typo for `WarehouseId`
- `BaseEnity` is a typo for `BaseEntity` (class name and file name both have it)
- No project references between layers
- `ApplicationDbContext` not registered in `Program.cs`
- No `appsettings` connection string for the database

## EF Core Setup

EF Core 8.0.0 is in `DomainLayer.csproj`. Code-first approach — no migrations exist yet. Before running migrations, wire up the DbContext in `WebAPI/Program.cs`:

```csharp
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
```
