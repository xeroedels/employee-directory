# Employee Directory

A small employee directory built with **ASP.NET Core MVC** and **Entity Framework Core** on **SQL Server**. It lists employees in a table with name search, a salary filter, pagination, and delete, and it fills itself with sample data on first run.


## Features

- **Employee table** with first name, last name, department, job title, email, salary, and hire date
- **Search** by first name or last name
- **Salary filter**: a button that shows only employees earning more than 6,000,000
- **Pagination**, 10 employees per page, with the total record count and the current page
- **Delete** with a confirmation dialog. The row is removed from the table and the total count updates without reloading the page
- **Sample data**: 100 test employees are created automatically when the database is empty

## Tech stack

| Area | Technology |
| --- | --- |
| Framework | ASP.NET Core MVC (.NET 8) |
| Language | C# |
| Data access | Entity Framework Core 8 (code-first, migrations) |
| Database | SQL Server |
| Frontend | Razor views, Bootstrap, vanilla JavaScript (`fetch`) |

## Project structure

```
EmployeeDirectory/
├── Controllers/
│   ├── EmployeesController.cs    # list, search, filter, paginate, delete
│   └── HomeController.cs
├── Data/
│   ├── ApplicationDbContext.cs
│   └── SeedData.cs               # creates 100 sample employees on first run
├── Migrations/                   # EF Core migrations (InitialCreate)
├── Models/
│   └── Employee.cs
├── ViewModels/
│   └── EmployeeListViewModel.cs  # data passed to the list view
├── Views/
│   └── Employees/Index.cshtml    # the directory page
├── wwwroot/
│   └── js/employee.js            # delete button logic
├── appsettings.json              # connection string
└── Program.cs
```

## Getting started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server (Express, Developer, or LocalDB all work)
- The EF Core command-line tool:
  ```bash
  dotnet tool install --global dotnet-ef
  ```

### Run it locally

1. **Clone the repository**
   ```bash
   git clone https://github.com/<your-username>/<repo-name>.git
   cd <repo-name>
   ```

2. **Set your connection string** in `appsettings.json` so it points to your own SQL Server instance:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=.\\SQLEXPRESS;Database=EmployeeDirectoryDb;Trusted_Connection=True;TrustServerCertificate=True;"
   }
   ```
   Replace `.\\SQLEXPRESS` with your server name. For LocalDB, use `(localdb)\\MSSQLLocalDB`.

3. **Create the database** from the included migration:
   ```bash
   dotnet ef database update
   ```

4. **Start the app**
   ```bash
   dotnet run
   ```

5. **Open the directory** at the address printed in the console, followed by `/Employees`, for example `https://localhost:7262/Employees`. On first run the 100 sample employees are inserted automatically.

## API

Deleting an employee is done through a small HTTP endpoint that the page calls with `fetch`:

| Method | Route | Responses |
| --- | --- | --- |
| `DELETE` | `/api/employees/{id}` | `200 OK` when deleted, `404 Not Found` if the employee does not exist, `400 Bad Request` for an invalid id |

## Data model

| Field | Type | Rules |
| --- | --- | --- |
| `Id` | `int` | Primary key |
| `FirstName` | `string` | Required, max 50 characters |
| `LastName` | `string` | Required, max 50 characters |
| `Department` | `string` | Required, max 100 characters |
| `JobTitle` | `string` | Required, max 100 characters |
| `Email` | `string?` | Optional, must be a valid email address |
| `Salary` | `decimal(18,2)` | |
| `HireDate` | `DateTime` | |

## Author

Built by **Sagita Sasmita Wijaya** as a practice project for ASP.NET Core MVC and Entity Framework Core. See more at [xeroedels.github.io/portfolio](https://xeroedels.github.io/portfolio/).
