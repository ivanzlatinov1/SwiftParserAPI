# SwiftParserAPI

ASP.NET Core Web API for parsing Swift MT103 Messages

## What it does?

- Accepts a `.txt` file containing the Swift MT103 message
- Extracts key fields from the message
- Uploads them into a database
- Offers CRUD operations for Swift Messages and Logs
- Logs events using NLog
- API documentation using Scalar ASP.NET

## Technical Requirements

- [x] Built on **ASP.NET Web API** targeting **.NET 10**
- [x] SWIFT message parsing implemented without third-party libraries
- [x] API documented via integrated OpenAPI/Scalar tooling
- [x] Authentication and authorization intentionally out of scope
- [x] **SQLite** used as the persistent data store
- [x] Data access handled without Entity Framework Core
- [x] Structured logging provided by **NLog**
- [x] **Bonus:** Dedicated `Logs` table introduced to maintain a full audit trail of message lifecycle events

## Project Structure

- `API/Controllers` — Web API controllers handling HTTP request routing and response formatting
- `Application` — Business logic, DTOs and Entity-Model mappers
- `Repositories` — Raw SQLite data access layer with Unit of Work pattern
- `Domain/Entities` — Core domain model definitions
- `Shared` — Utility helpers and shared constants
- `nlog.config` — NLog logging configuration

## Example Swift MT103 file

[Click here to see example MT103 message](./MT103.txt)
