using EmployeeDirectory.Data;
using EmployeeDirectory.Models;
using EmployeeDirectory.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDirectory.Controllers;

public class EmployeesController : Controller
{
    private readonly ApplicationDbContext _context;

    public EmployeesController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(int page = 1, 
    string? searchTerm = null, decimal? minSalary = null)
    {
        const int pageSize = 10;

        IQueryable<Employee> query = _context.Employees;

        if(!string.IsNullOrWhiteSpace(searchTerm))
        {
            searchTerm = searchTerm.Trim();

            query = query.Where(e =>
                e.FirstName.Contains(searchTerm) ||
                e.LastName.Contains(searchTerm));
        }

        if(minSalary.HasValue)
        {
            query = query.Where(e => e.Salary > minSalary.Value);
        }
        int totalRecords = await query.CountAsync();

        var employees = await query
        .OrderBy(e => e.Id)
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();

        var vm = new EmployeeListViewModel
        {
            Employees = employees,
            CurrentPage = page,
            PageSize = pageSize,
            TotalRecords = totalRecords,
            TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize),
            SearchTerm = searchTerm, 
        };


        return View(vm);
    
    }
[HttpDelete("api/employees/{id}")]
public async Task<IActionResult> Delete(int id)
{
    if (id <= 0)
    {
        return BadRequest();
    }

    var employee = await _context.Employees.FindAsync(id);

    if (employee == null)
    {
        return NotFound();
    }

    _context.Employees.Remove(employee);

    await _context.SaveChangesAsync();

    return Ok();
}
}