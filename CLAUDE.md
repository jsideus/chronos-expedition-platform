# CLAUDE.md — chronos-expedition-platform

## Project Purpose
This repository is the Application Under Test (AUT) for the `customizable-crucible` test framework. It is a fake microservices constellation modeling a fictional time-travel expedition booking platform. Its primary purpose is to give the test framework realistic backend services to exercise — REST APIs, GraphQL endpoints, database state, message-bus interactions, and eventual consistency scenarios.

This is **not** a portfolio piece on its own. It exists in service of the framework. Code quality is "boring enough to be uninteresting" — clean and idiomatic, but deliberately not over-engineered. The framework is where the interview-grade craft goes.

## Tech Stack
- **Language**: C# 14
- **Runtime**: .NET 10 (LTS, supported until November 10, 2028)
- **Web framework**: ASP.NET Core minimal APIs
- **Data access**: Dapper against PostgreSQL (Npgsql provider)
- **GraphQL**: HotChocolate (BookingsService only)
- **Message bus**: MassTransit on RabbitMQ
- **Schema migrations**: FluentMigrator
- **Local orchestration**: Docker Compose
- **CI/CD**: GitHub Actions

## Repository Structure
chronos-expedition-platform/
├── src/
│   ├── ExpeditionsService/    # REST: list/get expeditions, slot availability
│   ├── BookingsService/       # REST + GraphQL: bookings, publishes BookingPlaced
│   └── PaymentsService/       # Subscribes to BookingPlaced, publishes PaymentCompleted/Failed
├── shared/
│   └── Contracts/             # Shared event/message contracts
├── docker/
│   └── docker-compose.yml     # Local Postgres + RabbitMQ + all services
├── docs/
│   └── adr/                   # Architecture Decision Records
└── .github/workflows/         # CI pipelines

## Domain Model

### Expeditions
Time-travel expeditions to historical or future destinations. Each expedition has:
- A destination (era enum, year as signed int allowing BCE, location, coordinate precision)
- A departure window
- Duration
- Maximum party size
- An assigned guide

Hand-curated seed data spans eras: Library of Alexandria 48 BCE, Cretaceous Photo Safari 66M BCE, Apollo 11 Launch Viewing 1969, Crystal Palace Exhibition 1851, Tokyo 2089, etc.

### Bookings
Traveler bookings against an expedition. Each booking has:
- A primary traveler
- Optional companions (array)
- Medical clearances (required for pre-1900 destinations)
- A cover identity for the destination era (polymorphic by era)

### Payments
Payment processing in "Temporal Credits" with configurable exchange rate. Failure modes:
- Insufficient credits
- Traveler blacklisted by Temporal Authority
- Destination locked due to causality protection

## Conventions and Standards
- Modern C# idioms: file-scoped namespaces, primary constructors, nullable reference types enabled, records for DTOs/contracts, target-typed `new`, `required` members
- One project per service, slim and focused
- Minimal API endpoints in `Program.cs` for small services; extracted into endpoint classes when a service grows past ~5 endpoints
- Dapper queries in dedicated repository classes, not inline in endpoints
- Message contracts live in `shared/Contracts/`, referenced by all services that publish or subscribe
- Async all the way down — no `.Result` or `.Wait()`, ever
- Cancellation tokens on every async public method
- Structured logging with `ILogger<T>`, no `Console.WriteLine` outside `Program.cs`

## What NOT to Do
- **No MediatR.** Direct method calls are fine for services this small. MediatR is over-engineering at this scale.
- **No Clean Architecture / Onion / Hexagonal layering.** These services are intentionally simple. One project per service. Folders by feature, not by layer.
- **No Entity Framework.** This is a Dapper showcase. Dapper is the chosen data access technology and the framework will be tested against it explicitly.
- **No auto-generated XML doc comments.** They produce noise. Comments only when the code's intent is non-obvious.
- **No retries, circuit breakers, or Polly beyond defaults.** These services are fixtures for testing, not production resilience showcases.
- **No authentication beyond a single fake bearer token validator** if needed at all.
- **No premature abstractions.** Rule of three: extract patterns only after they appear three times.
- **No "AI suggested this" code without justification.** Every implementation choice has a reason that can be articulated.

## Current Focus
[Update this section as work progresses.]

**Active slice**: Repository scaffolding complete. Next slice will be the first vertical: ExpeditionsService returning one hardcoded expedition via `GET /expeditions/{id}`, with the framework asserting against it.

## Working with Me (Notes for Claude Code)
- The repo owner is **deliberately rebuilding from-scratch coding skills**. Default to *not* writing code. Default to explaining, asking Socratic questions, and reviewing code the owner has written.
- When the owner asks "review this," do a rigorous code review at the level of a Datadog/Honeycomb staff engineer. Identify issues. Do **not** offer to fix them — the owner fixes them.
- When the owner asks for help understanding a concept, explain it with reference to canonical .NET documentation. Link to `learn.microsoft.com` pages where relevant.
- When generating code is genuinely warranted (boilerplate, config files, repetitive patterns), keep it minimal and explain every non-obvious line.
- The owner is building this as part of a job search targeting senior SDET roles at engineering-first companies. Code that ships here may be discussed in interviews. Optimize for the owner's ability to defend every line.
