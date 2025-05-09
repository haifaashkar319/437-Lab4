# Library Management System - ASP.NET Core Project

This README explains how the first two parts of the assignment were completed in the `437-Lab1` project. This setup covers the creation of RESTful APIs and integration of FluentValidation for server-side validation.

---

## ✅ Part 1: RESTful API Implementation

We created REST API controllers for each of the main entities:

- `Authors`
- `Books`
- `Borrowers`
- `Loans`

### 🔹 API Folder Structure
All API controllers are in:

```
/Controllers/Api/
```

Each controller:
- Uses `[ApiController]` and `[Route("api/[controller]")]`
- Supports full CRUD operations (GET, POST, PUT, DELETE)
- Uses `LibraryContext` with proper relationships (e.g., `Include()` for navigation properties)

### 🔹 Example Routes
| Entity    | Route                  | Description           |
|-----------|------------------------|-----------------------|
| Authors   | `GET /api/authors`     | Get all authors       |
| Books     | `POST /api/books`      | Add a book            |
| Borrowers | `PUT /api/borrowers/1` | Update a borrower     |
| Loans     | `DELETE /api/loans/1`  | Delete a loan         |

---

## ✅ Part 2: Validation with FluentValidation

### 🔹 Setup

1. Installed FluentValidation:
```bash
dotnet add package FluentValidation.AspNetCore
```

2. In `Program.cs`, registered FluentValidation:
```csharp
builder.Services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
```

3. Created validator classes in a new `/Validators` folder:
   - `AuthorValidator.cs`
   - `BookValidator.cs`
   - `BorrowerValidator.cs`
   - `LoanValidator.cs`

Each validator uses `AbstractValidator<T>` and validates based on business rules (e.g., name length, genre format, date not in future, etc.).

---


## ✅ Next Steps (For Your Friend)

Your friend can continue with:
- Part 3: Pagination, search, and filtering
- Part 4: Documentation (Swagger or other)
- Part 5: Extra features (authentication, frontend improvements, etc.)

Make sure to always run:
```bash
dotnet ef database update
dotnet run
```

Let me know if you need any help finishing this project!
