using EmployeeDirectory.Models;

namespace EmployeeDirectory.Data;

public static class SeedData
{
    public static void Initialize(ApplicationDbContext context)
    {
        if(context.Employees.Any())
            return;
        var departments = new[]
        {
            "IT",
            "HR",
            "Finance",
            "Marketing",
            "Sales"
        };

        var jobTitles = new[]
        {
            "Software Engineer",
            "HR Specialist",
            "Accountant",
            "Marketing Executive",
            "Sales Executive"
        };

        var employees = new List<Employee>();

        for (int i = 1; i <= 100; i++)
        {
            employees.Add(new Employee
            {
                FirstName = $"Employee{i}",
                LastName = $"Test{i}",
                Department = departments[i % departments.Length],
                JobTitle = jobTitles[i % jobTitles.Length],
                Email = $"employee{i}@company.com",
                Salary = 5000000 + (i * 100000),
                HireDate = DateTime.Now.AddDays(-i * 30)
            });
        }    
        
        context.Employees.AddRange(employees);
        context.SaveChanges();
    }
}