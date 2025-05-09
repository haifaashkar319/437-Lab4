# EECE 437 Assignment 4 - Clean Architecture Refactor

## Team: Haifa Al Ashkar & Nael Haidar
## Project: LibraryManagement

---

### ✅ STEP 0 – Project Setup (Clean Architecture Restructure)

**Date:** [Insert Date]

**Objective:** Restructure the project to follow Clean Architecture principles by splitting into 4 projects:
- Core.Domain
- Application
- Infrastructure
- Web (existing MVC project)

**Actions Taken:**
- Added three new class library projects:
  - `Core.Domain` for domain entities and interfaces
  - `Application` for use cases, DTOs, validators
  - `Infrastructure` for persistence and external services
- Added references:
  - `Application → Core.Domain`
  - `Infrastructure → Application + Core.Domain`
  - `Web → all 3 layers`
- Verified using `dotnet list <project>.csproj reference`
- Verified successful build: `dotnet build LibraryManagement.sln`

note: 
- Resolved .NET 9 build errors:
  - Added `<GenerateAssemblyInfo>false>` to all `.csproj` files
  - Added `<GenerateTargetFrameworkAttribute>false>` to suppress duplicate framework attributes

**Result:** Project is now structured with clear separation of concerns and ready for Clean Architecture migration.

---

---

### ✅ STEP 1 – Refactor Domain Entities

**Date:** 2025-05-08  
**Objective:** Migrate and clean core business entities into the `Core.Domain` layer to follow Clean Architecture principles.

**Actions Taken:**
- Created `Core.Domain/Entities/` directory
- Moved the following domain models from `Models/`:
  - `Author.cs`
  - `Book.cs`
  - `Borrower.cs`
  - `Loan.cs`
- Removed all framework and persistence annotations:
  - `[Required]`, `[StringLength]`, `[ForeignKey]`, etc.
  - `using System.ComponentModel.DataAnnotations`
- Ensured that domain entities are now:
  - Pure C# POCOs
  - Free from EF Core, MVC, or Identity dependencies

**Result:** Domain entities are persistence-agnostic and framework-independent. The solution builds successfully using the new models.

---

---

### ✅ STEP 2 – Define Domain Interfaces (Repositories)

**Date:** 2025-05-08  
**Objective:** Define repository interfaces for domain entities, enabling loose coupling between the domain and data access layers.

**Actions Taken:**
- Created folder: `Core.Domain/Interfaces/`
- Defined a generic base interface `IRepository<T>` with standard operations:
  - `GetByIdAsync`, `GetAllAsync`, `AddAsync`, `UpdateAsync`, `DeleteAsync`
- Created entity-specific interfaces:
  - `IAuthorRepository`
  - `IBookRepository`
  - `IBorrowerRepository`
  - `ILoanRepository`
- Each interface inherits from `IRepository<T>` to enforce consistency

**Result:** Domain layer now includes all interfaces for core operations, enabling the Infrastructure layer to implement them without tight coupling.

---
---

### ✅ STEP 3 – Implement Infrastructure BookRepository

**Date:** 2025-05-08  
**Objective:** Create the first repository implementation in the Infrastructure layer to handle book-related data access logic.

**Actions Taken:**
- Created folder: `Infrastructure/Repositories/`
- Implemented `BookRepository` to handle:
  - `GetAllAsync()`, `GetByIdAsync()`, `AddAsync()`, `UpdateAsync()`, `DeleteAsync()`
  - Used EF Core with `Include(Author)` for proper navigation handling
- Moved `LibraryContext.cs` to `Infrastructure/Persistence/` and updated its namespace
- Moved `ApplicationUser.cs` to `Infrastructure/Identity/`
- Updated `Program.cs` to register `IBookRepository` with `BookRepository`
- Installed required NuGet packages:
  - `Microsoft.EntityFrameworkCore`
  - `Microsoft.AspNetCore.Identity.EntityFrameworkCore`
- Verified build success with all layers connected

**Result:** The infrastructure is now capable of persisting book-related data using a repository that cleanly implements the domain interface.

---
---

### ✅ STEP 4 – CreateBook Use Case (CQRS with MediatR)

**Date:** 2025-05-08  
**Objective:** Implement the “Create Book” use case using the CQRS pattern with MediatR and FluentValidation.

**Actions Taken:**
- Created folder structure in `Application/Books/`:
  - `Commands/CreateBookCommand.cs`: defines the request object
  - `Handlers/CreateBookHandler.cs`: handles book creation via IBookRepository
  - `Validators/CreateBookValidator.cs`: enforces input rules using FluentValidation
- Used `IRequest<int>` to return the ID of the created book
- Registered MediatR and FluentValidation in `Program.cs`
- Added packages:
  - `MediatR`
  - `FluentValidation`
- Ensured all business logic and validation is handled outside the controller

**Result:** The CreateBook use case is fully functional, cleanly separated from UI logic, and follows Clean Architecture and CQRS best practices.

---
