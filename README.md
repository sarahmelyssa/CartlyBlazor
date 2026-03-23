# Cartly Blazor

Cartly Blazor is a price comparison platform built with ASP.NET Core Blazor Server. It helps users search products, compare store prices, manage profiles, and recover access through Supabase authentication.

## Highlights

- Product search and product detail pages
- Customer registration, login, password recovery, and profile editing
- Admin-oriented pages for products, stores, users, and price records
- PostgreSQL persistence through Entity Framework Core and Npgsql
- Supabase authentication integration
- Responsive UI with custom CSS and product imagery

## Tech Stack

- C# / .NET 8
- ASP.NET Core Blazor Server
- Entity Framework Core
- PostgreSQL
- Supabase

## Configuration

This repository does not include production credentials. Configure secrets locally with environment variables or .NET user secrets:

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=...;Port=5432;Database=...;Username=...;Password=...;SSL Mode=Require"
dotnet user-secrets set "Supabase:Url" "https://your-project.supabase.co"
dotnet user-secrets set "Supabase:Key" "your-supabase-anon-key"
```

## Run Locally

```bash
dotnet restore
dotnet build
dotnet run --project "Cartly Blazor/Cartly Blazor.csproj"
```

## Status

Academic portfolio project focused on full-stack web development, authentication flows, database integration, and clean user-facing screens.
