# Online Library Management System (ASP.NET Core MVC)

This is a Library Management System built with ASP.NET Core MVC. It allows users to manage books, authors, borrowers, and loans, providing a structured system for library operations.

---

## Features
- CRUD Operations (Create, Read, Update, Delete) for Books, Authors, Borrowers, and Loans.
- Search & Filtering for easy data retrieval.
- Loan Management with automatic date tracking.
- Bootstrap UI for responsive and modern design.
- Validation & Error Handling for data integrity.

---

## Prerequisites
Make sure you have the following installed:
- .NET SDK (Version 6 or later) - Download from https://dotnet.microsoft.com/en-us/download
- SQL Server / SQLite (for the database)
- Git (for cloning the repository)
- Visual Studio / VS Code (recommended for development)

---

## Installation Instructions

### 1. Clone the Repository
Open Terminal or Command Prompt and run:
https://github.com/haifaashkar319/437-Lab1.git


### 2. Restore Dependencies
Run the following command to install required dependencies:

dotnet restore

### 3. Set Up the Database
If you're using Entity Framework Core, run migrations to set up the database:

dotnet ef database update

If `dotnet ef` is not recognized, install the Entity Framework Core tool:

dotnet tool install --global dotnet-ef

### 4. Check for Bootstrap
If Bootstrap is missing (usually located in the wwwroot/lib/ folder), restore it manually:

For LibMan
Run:

libman restore

### 5. Run the Application
Start the ASP.NET Core MVC application:
dotnet watch run