# 🎓 School Management System — RESTful Web Services

> **A streamlined, production-grade School/Learning Management System backend built with Minimal Clean Architecture & Vertical Slice Architecture.**
> Designed and developed by **Asoh Yannick Anoh** — .NET Backend Developer

---

## 👨‍💻 Author

| Field | Details |
|---|---|
| **Name** | Asoh Yannick Anoh |
| **Role** | .NET Backend Developer |
| **Stack** | .NET 10 · C# · ASP.NET Core 10 · PostgreSQL · Docker · Aspire |
| **Architecture** | Minimal Clean Architecture · Vertical Slice Architecture (VSA) |

---

## 📋 Table of Contents

- [Overview](#-overview)
- [Tech Stack](#-tech-stack)
- [Architecture](#-architecture)
- [Folder Structure](#-folder-structure)
- [Getting Started](#-getting-started)
- [Environment Variables](#-environment-variables)
- [Running with Docker & Aspire](#-running-with-docker--aspire)
- [Database Migrations](#-database-migrations)
- [API Documentation](#-api-documentation)
- [Running Tests](#-running-tests)
- [Adding a New Feature](#-adding-a-new-feature-vertical-slice)
- [Contributing](#-contributing)
- [Roadmap](#-roadmap)
- [License](#-license)

---

## 🌐 Overview

This project exposes a suite of **RESTful Web Services** powering a large-scale Learning Management System. It handles:

- 🧑‍🎓 Student registration & profile management
- 👨‍🏫 Teacher & staff management
- 📚 Course & curriculum management
- 🗓️ Class scheduling & timetabling
- 📝 Assignments, submissions & grading
- 📊 Attendance tracking & reporting
- 💳 Tuition & fee management
- 📧 Notifications & email communication

---

## 🛠️ Tech Stack

| Layer | Technology |
|---|---|
| **Language** | C# 13 |
| **Framework** | ASP.NET Core 10 |
| **Architecture** | Minimal Clean Architecture · Vertical Slice Architecture |
| **API Style** | RESTful (FastEndpoints) |
| **ORM** | Entity Framework Core 10 |
| **Database** | PostgreSQL (containerized via Aspire) |
| **Containerization** | Docker 🐳 |
| **Orchestration** | .NET Aspire 13 |
| **Auth** | ASP.NET Core Identity + JWT 🔐 |
| **Email** | MailKit 4.16+ 📧 |
| **Logging** | Serilog + OpenTelemetry 📈 |
| **Validation** | FluentValidation ✅ |
| **Testing** | xUnit · NSubstitute · Shouldly · Testcontainers 🧪 |

---

## 🏛️ Architecture

This project uses **Minimal Clean Architecture** with **Vertical Slice Architecture (VSA)** — a deliberate, pragmatic simplification of the full Clean Architecture pattern. All code lives in a **single Web project**, organized by **feature** rather than by layer.

```
┌─────────────────────────────────────────────────────────────────────┐
│                        learning_ms.Web                               │
│                                                                     │
│  ┌──────────────────┐   ┌─────────────────┐   ┌──────────────────┐ │
│  │     🧱 Domain     │   │  🌐 Endpoints   │   │ ⚙️ Infrastructure │ │
│  │                  │   │                 │   │                  │ │
│  │  Aggregates      │   │  FastEndpoints  │   │  EF Core / Data  │ │
│  │  ├ Student       │   │  Vertical       │   │  Repositories    │ │
│  │  ├ Course        │   │  Slices         │   │  Email (MailKit) │ │
│  │  ├ Teacher       │   │  ├ Students     │   │  Auth / JWT      │ │
│  │  ├ Enrollment    │   │  ├ Courses      │   │  Caching         │ │
│  │  ├ Attendance    │   │  ├ Grades       │   │  Background Jobs │ │
│  │  └ Grade         │   │  ├ Attendance   │   │  Logging /       │ │
│  │                  │   │  └ Auth         │   │  Telemetry       │ │
│  │  ValueObjects    │   │                 │   │                  │ │
│  │  Domain Events   │   │                 │   │                  │ │
│  │  Enums           │   │                 │   │                  │ │
│  └──────────────────┘   └─────────────────┘   └──────────────────┘ │
│                                                                     │
└─────────────────────────────────────────────────────────────────────┘
                 │                              │
  ┌──────────────▼──────────────┐  ┌────────────▼────────────────────┐
  │  learning_ms.ServiceDefaults │  │     learning_ms.AspireHost       │
  │  📡 Shared Observability     │  │  🎛️ Orchestration & Provisioning  │
  │  (OTel, Serilog, Health)     │  │  (PostgreSQL, Docker, Dashboard) │
  └──────────────────────────────┘  └─────────────────────────────────┘
```

### 🔑 Key Design Principles

- 🏠 **Single Project** — All code in one Web project for simpler dependencies and faster builds
- 🍕 **Vertical Slices** — Organized by feature (Student, Course, Attendance) not by layer
- 🧠 **Domain-Driven Design** — Rich aggregates with proper encapsulation and business logic
- ⚡ **FastEndpoints** — REPR pattern for clean, testable API endpoints
- 🔄 **Optional Mediator** — Used for cross-cutting concerns where needed
- 🎯 **Pragmatic Abstractions** — Interfaces only where they add real value
- 🛡️ **Result Pattern** — `Ardalis.Result` for operation results; no exceptions for business failures

### 🆚 Minimal vs Full Clean Architecture

| Full Template | This Template |
|---|---|
| 4+ projects (Core, UseCases, Infrastructure, Web) | 1 Web project |
| Repository pattern with Specifications | Repository pattern where needed |
| Extensive interfaces and abstractions | Pragmatic abstractions |
| Separate UseCases project with Mediator | Optional Mediator; logic can live in endpoints |
| Complex DDD (Aggregates, Value Objects, Domain Events) | Pragmatic DDD (Aggregates, Value Objects) |

---

## 📁 Folder Structure

```
learning_ms/
│
├── 📂 src/
│   │
│   ├── 📂 learning_ms.Web/                        ← 🌐 Main application project
│   │   │
│   │   ├── 📂 Domain/                             ← 🧱 Domain entities, aggregates & events
│   │   │   ├── 📂 StudentAggregate/
│   │   │   │   ├── Student.cs
│   │   │   │   ├── StudentStatus.cs               ← Enum
│   │   │   │   └── 📂 Events/
│   │   │   │       └── StudentEnrolledEvent.cs    ← Domain event
│   │   │   ├── 📂 CourseAggregate/
│   │   │   │   ├── Course.cs
│   │   │   │   ├── CourseModule.cs
│   │   │   │   └── 📂 Enums/
│   │   │   │       └── CourseLevel.cs
│   │   │   ├── 📂 TeacherAggregate/
│   │   │   │   └── Teacher.cs
│   │   │   ├── 📂 EnrollmentAggregate/
│   │   │   │   └── Enrollment.cs
│   │   │   ├── 📂 AttendanceAggregate/
│   │   │   │   └── AttendanceRecord.cs
│   │   │   └── 📂 GradeAggregate/
│   │   │       └── Grade.cs
│   │   │
│   │   ├── 📂 Infrastructure/                     ← ⚙️ Data access & external services
│   │   │   ├── 📂 Data/
│   │   │   │   ├── AppDbContext.cs
│   │   │   │   ├── 📂 Config/                     ← EF Core entity configurations
│   │   │   │   │   ├── StudentConfiguration.cs
│   │   │   │   │   ├── CourseConfiguration.cs
│   │   │   │   │   └── EnrollmentConfiguration.cs
│   │   │   │   └── 📂 Migrations/
│   │   │   ├── 📂 Repositories/                   ← Concrete repository implementations
│   │   │   ├── 📂 Services/                       ← Third-party service wrappers
│   │   │   ├── 📂 Caching/                        ← Redis / in-memory cache
│   │   │   ├── 📂 Auth/                           ← JWT, Identity configuration
│   │   │   ├── 📂 BackgroundJobs/                 ← Hangfire / Quartz jobs
│   │   │   └── 📂 Email/
│   │   │       └── MailKitEmailSender.cs
│   │   │
│   │   ├── 📂 Endpoints/                          ← 🌐 API endpoints (FastEndpoints / VSA)
│   │   │   ├── 📂 Students/
│   │   │   │   ├── CreateStudentEndpoint.cs
│   │   │   │   ├── GetStudentEndpoint.cs
│   │   │   │   ├── ListStudentsEndpoint.cs
│   │   │   │   └── UpdateStudentEndpoint.cs
│   │   │   ├── 📂 Courses/
│   │   │   │   ├── CreateCourseEndpoint.cs
│   │   │   │   ├── GetCourseEndpoint.cs
│   │   │   │   └── ListCoursesEndpoint.cs
│   │   │   ├── 📂 Enrollments/
│   │   │   ├── 📂 Attendance/
│   │   │   ├── 📂 Grades/
│   │   │   └── 📂 Auth/
│   │   │       ├── LoginEndpoint.cs
│   │   │       └── RegisterEndpoint.cs
│   │   │
│   │   ├── 📄 Program.cs                          ← Application startup & DI registration
│   │   ├── 📄 appsettings.json
│   │   └── 📄 appsettings.Development.json
│   │
│   ├── 📂 learning_ms.ServiceDefaults/            ← 📡 Shared observability defaults
│   │   └── Extensions.cs                         ← OTel, Serilog, Health Checks
│   │
│   └── 📂 learning_ms.AspireHost/                 ← 🎛️ Aspire orchestration entry point
│       ├── Program.cs
│       └── 📂 Properties/
│           └── launchSettings.json
│
├── 📄 Directory.Build.props
├── 📄 Directory.Packages.props                    ← 📦 Central package management (CPM)
├── 📄 global.json
├── 📄 nuget.config
├── 📄 learning_ms.slnx
├── 📄 .editorconfig
├── 📄 .runsettings
└── 📄 README.md
```

---

## 🚀 Getting Started

### ✅ Prerequisites

Make sure you have the following installed:

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) *(required for PostgreSQL container)*
- [Git](https://git-scm.com/)

### 📥 Clone the Repository

```sh
git clone https://github.com/asohyannick/.net_school_management_system.git
cd .net_school_management_system
```

### 🔨 Build the Solution

```sh
dotnet build
```

### ▶️ Run the Application

**Option 1 — Via Aspire (recommended, auto-provisions PostgreSQL):**

```sh
dotnet run --project src/learning_ms.AspireHost
```

The Aspire dashboard will open automatically. PostgreSQL is spun up in Docker, the database is created, and migrations are applied — all automatically. 🎉

**Option 2 — Web API only (requires manual DB setup):**

```sh
dotnet run --project src/learning_ms.Web
```

---

## 🔐 Environment Variables

Create `src/learning_ms.Web/appsettings.Development.json` and fill in your values:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=learning_ms;User Id=sa;Password=YourStrong@Password;TrustServerCertificate=true"
  },
  "Email": {
    "Host": "smtp.yourprovider.com",
    "Port": 587,
    "Username": "your@email.com",
    "Password": "yourpassword",
    "FromName": "Learning MS"
  },
  "Jwt": {
    "Key": "your-super-secret-key-min-32-chars",
    "Issuer": "learning_ms",
    "Audience": "learning_ms_clients",
    "ExpiryMinutes": 60
  }
}
```

> ⚠️ **Never commit secrets to version control.** Use environment variables or a secrets manager in production.

---

## 🐋 Running with Docker & Aspire

When running via Aspire, Docker Desktop must be running. Aspire automatically:

- 🐳 Pulls and starts a **PostgreSQL** container
- 🗃️ Creates the **database** and applies **migrations**
- 📊 Opens the **Aspire Dashboard** for logs, traces, and metrics

```sh
# Start everything via Aspire
dotnet run --project src/learning_ms.AspireHost

# The Aspire dashboard will be available at https://localhost:{port}
```

---

## 🗃️ Database Migrations

```sh
# ➕ Add a new migration
dotnet ef migrations add <MigrationName> \
  -c AppDbContext \
  -p src/learning_ms.Web \
  -s src/learning_ms.Web

# ⬆️ Apply migrations manually
dotnet ef database update \
  -c AppDbContext \
  -p src/learning_ms.Web \
  -s src/learning_ms.Web

# ⏪ Rollback last migration
dotnet ef migrations remove \
  -p src/learning_ms.Web \
  -s src/learning_ms.Web
```

---

## 📖 API Documentation

Once the application is running, API docs are available at:

| UI | URL |
|---|---|
| 🔷 **Scalar UI** | `https://localhost:{port}/scalar` |
| 📘 **Swagger UI** | `https://localhost:{port}/swagger` |

---

## 🧪 Running Tests

```sh
# 🏃 Run all tests
dotnet test

# 📋 Run with detailed output
dotnet test --logger "console;verbosity=detailed"

# 📊 Run with code coverage
dotnet test --collect:"XPlat Code Coverage"
reportgenerator -reports:**/coverage.cobertura.xml -targetdir:coverage-report
```

---

## ➕ Adding a New Feature (Vertical Slice)

Follow these steps to add a new domain feature:

**1. 🧱 Create the Domain Aggregate**
```
Domain/YourFeatureAggregate/YourEntity.cs
Domain/YourFeatureAggregate/Events/YourEntityCreatedEvent.cs
Domain/YourFeatureAggregate/Enums/YourEntityStatus.cs
```

**2. ⚙️ Add EF Core Configuration**
```
Infrastructure/Data/Config/YourEntityConfiguration.cs
```

**3. 🗃️ Create a Migration**
```sh
dotnet ef migrations add AddYourFeature -p src/learning_ms.Web -s src/learning_ms.Web
```

**4. 🌐 Create FastEndpoints**
```
Endpoints/YourFeature/CreateYourFeatureEndpoint.cs
Endpoints/YourFeature/GetYourFeatureEndpoint.cs
Endpoints/YourFeature/ListYourFeatureEndpoint.cs
Endpoints/YourFeature/UpdateYourFeatureEndpoint.cs
```

**5. 🧪 Add Tests**
```
Tests/YourFeature/CreateYourFeatureTests.cs
Tests/YourFeature/GetYourFeatureTests.cs
```

---

## 🤝 Contributing

Contributions are welcome and appreciated! Here's how to get involved:

### 1. 🍴 Fork & Clone

```sh
git clone https://github.com/asohyannick/.net_school_management_system.git
cd .net_school_management_system
```

### 2. 🌿 Create a Feature Branch

Always branch off `develop`. Use a descriptive name:

```sh
git checkout -b feature/add-grade-management
git checkout -b fix/enrollment-duplicate-bug
git checkout -b chore/update-otel-packages
git checkout -b docs/update-api-readme
```

**Branch naming conventions:**

| Prefix | Use for |
|---|---|
| `feature/` | ✨ New features or vertical slices |
| `fix/` | 🐛 Bug fixes |
| `chore/` | ♻️ Dependency updates, refactoring, tooling |
| `docs/` | 📝 Documentation only changes |
| `test/` | 🧪 Adding or updating tests | 

### 3. ✅ Follow the Architecture

- 🧱 **Domain logic** lives in `Domain/` — aggregates should be rich, not anemic
- ⚙️ **Data access** lives in `Infrastructure/Data/` — use EF Core entity configurations
- 🌐 **HTTP concerns** live in `Endpoints/` — keep endpoints thin
- 💼 **Business logic** can live in endpoints or optional Mediator handlers
- 🛡️ Use `Ardalis.Result` for operation results — avoid throwing exceptions for business failures
- ✅ Every new feature should have at least basic **happy-path tests**

### 4. 🧹 Code Standards

- Follow existing naming conventions (`PascalCase` for classes, `camelCase` for locals)
- Use `Ardalis.GuardClauses` for guard clauses and input validation
- Use `Ardalis.Result` for operation results — avoid throwing exceptions for business failures
- Use **FluentValidation** for request model validation in endpoints
- Run `dotnet build` and `dotnet test` before committing — both must pass ✅

### 5. 💬 Commit Messages

Use clear, imperative commit messages with emojis:

```
✨ Add grade management vertical slice
🐛 Fix student enrollment allowing duplicates
♻️ Refactor attendance endpoint to use Result pattern
📦 Update MailKit to 4.16.0
🧪 Add integration tests for course creation
📝 Update contributing guidelines
```

### 6. 📬 Open a Pull Request

- Target the `develop` branch (never push directly to `main`)
- Write a clear PR description: **what** changed and **why**
- Link any related GitHub issues
- Request a review from at least one contributor
- Ensure all CI checks pass ✅

### 7. 🚫 What NOT to Do

- ❌ Don't commit secrets, API keys, or connection strings
- ❌ Don't put business logic in `Program.cs`
- ❌ Don't bypass the Result pattern by throwing exceptions for business rules
- ❌ Don't skip tests for new endpoints
- ❌ Don't push directly to `main` or `develop` without a PR

---

## 🗺️ Roadmap

- [ ] 🔐 JWT Authentication & Role-based Authorization
- [ ] 🧑‍🎓 Student portal endpoints
- [ ] 📚 Course & module management
- [ ] 📝 Assignment submission & grading
- [ ] 📊 Attendance tracking
- [ ] 💳 Fee & payment management
- [ ] 📧 Email notification system
- [ ] 📈 Reporting & analytics endpoints
- [ ] 🌍 Multi-tenancy support (multiple schools)
- [ ] 🔔 Real-time notifications (SignalR)
- [ ] 🧪 Full integration test coverage

---

## 📄 License

This project is licensed under the [MIT License](LICENSE).

---

## 📚 Resources

- [Clean Architecture Template — Ardalis](https://github.com/ardalis/CleanArchitecture)
- [Vertical Slice Architecture — Jimmy Bogard](https://jimmybogard.com/vertical-slice-architecture/)
- [FastEndpoints Documentation](https://fast-endpoints.com/)
- [.NET Aspire Documentation](https://learn.microsoft.com/en-us/dotnet/aspire/)
- [Entity Framework Core Docs](https://learn.microsoft.com/en-us/ef/core/)
- [Ardalis.Result](https://github.com/ardalis/Result)
- [FluentValidation](https://docs.fluentvalidation.net/)

---

<div align="center">

Built with ❤️ by **Asoh Yannick Anoh** · .NET Backend Developer

⭐ If this project helped you, consider giving it a star!

</div>