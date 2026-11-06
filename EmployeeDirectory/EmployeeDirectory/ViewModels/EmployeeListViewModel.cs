using EmployeeDirectory.Models;

namespace EmployeeDirectory.ViewModels;

public class EmployeeListViewModel
{
    public List<Employee> Employees {get; set; } = new();

    public int CurrentPage {get; set;}
    public int TotalPages {get; set;}

    public int TotalRecords {get; set;}

    public int PageSize {get; set;}

    public string? SearchTerm { get; set; }

    public int minSalary {get; set;}
}