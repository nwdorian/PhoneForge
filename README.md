# PhoneForge

[![build](https://github.com/nwdorian/PhoneForge/actions/workflows/build-validation.yml/badge.svg)](https://github.com/nwdorian/PhoneForge/actions/workflows/build-validation.yml)
[![tests](https://github.com/nwdorian/PhoneForge/actions/workflows/test-validation.yml/badge.svg)](https://github.com/nwdorian/PhoneForge/actions/workflows/test-validation.yml)

This application is designed to demonstrate document processing in ASP.NET

It allows users to work with contact data like in a phone book. They can load data from an Excel file and create PDF reports based on selected filters.

Backend is a .NET Web API and frontend is a simple console application for contact management and PDF report generation.

## Table of contents

- [PhoneForge](#phoneforge)
  - [Table of contents](#table-of-contents)
  - [Technologies](#technologies)
  - [Getting started](#getting-started)
    - [Prerequisites](#prerequisites)
    - [Installation](#installation)
  - [Features](#features)
  - [Solution structure](#solution-structure)
    - [Best practices](#best-practices)
  - [Contributing](#contributing)
  - [License](#license)
  - [Contact](#contact)

## Technologies

- **.NET 10**
- SQL Server
- Entity Framework Core

## Getting started

### Prerequisites

- .NET 10 SDK
- SQL Server (Developer, Express, Docker container)
- IDE like VS2022, VS Code or Rider (optional)
- Database management tool like SSMS or Dbeaver (optional)

### Installation

> [!NOTE]
> EF Core migrations were created.
>
> Migrations will be applied on startup of the API application, create the database, tables and seed that data from excel document.

1. Clone the repository
   - `git clone https://github.com/nwdorian/PhoneForge.git`

2. Configure `appsettings.json`
   - replace the connection string if necessary

3. Run the WebApi from project root
   - `dotnet run --project ./backend/WebApi/WebApi.csproj`

4. Run the Console from project root
   - `dotnet run --project ./frontend/Console/Console.csproj`

## Features

- Contacts management
  - Created, delete and update contacts
  - Pagination, sorting and filtering
- Excel data seeding
  - `./documents/contacts.xlsx` 
  - edit the Excel document to change seed data
- PDF report generation
  - outputs reports to `./documents/report-timestamp.pdf`
  - applies filtering and sorting options

## Solution structure

- Clean Architecture WebAPI project structure
  - Rich domain model
  - Value Objects enforcing invariants
  - CQRS pattern Application layer
  - REPR pattern WebAPI layer
  - Feature folders oranization per layer

### Best practices

- Continuous Integration
  - Github Actions workflows for build and test validation
- Consistent code style enforced by `.editorconfig`
- Central build configuration `Directory.Build.props`
  - .NET 10 target framework for all projects
  - code quality properties
- Central package management `Directory.Packages.Props`
- Static code analysis
  - Treat warnings as errors
  - Analysis level `latest-Recommended`
  - `SonarAnalyzer.CSharp` package
- Api versioning
- Input validation with FluentValidation
- Result pattern
- Structured logging with Serilog
- Soft delete and audit fields
- EF Core configuration classes
- Global exception handling
- ProblemDetails [RFC7808](https://datatracker.ietf.org/doc/html/rfc7807) standard

## Contributing

Contributions are welcome! Please fork the repository and create a pull request with your changes. For major changes, please open an issue first to discuss what you would like to change.

## License

This project is licensed under the MIT License. See the [LICENSE](./LICENSE) file for details.

## Contact

For any questions or feedback, please open an issue.
