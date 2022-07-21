using Microsoft.AspNetCore.Mvc;
using Qulix.Entities;
using Qulix.Models;
using Qulix.Repositories;

namespace Qulix.Controllers;
public class EmployeeController : Controller
{
    private readonly ILogger<EmployeeController> _logger;
    private readonly IEmployeeRepository _repo;

    public EmployeeController(ILogger<EmployeeController> logger, IEmployeeRepository repo)
    {
        _logger = logger;
        _repo = repo;
    }

    public async Task<IActionResult> Index()
    {
        var result = await _repo.GetEmployeesAsync();
        if(!result.IsSuccess) return View();
        return View(result.Employees ?? new List<Employee>());
    }

    public IActionResult Create()
        => View();

    [HttpPost]
    public async Task<IActionResult> Create(EmployeeViewModel model)
    {
        if(!ModelState.IsValid) return View(model);
        var employee = new Employee()
        {
            Firstname = model.Firstname,
            Lastname = model.Lastname,
            Middlename = model.Middlename,
            // SignedDate = model.SignedDate,
            Position = model.Position,
            CompanyId = model.CompanyId
        };
        var result = await _repo.AddEmployeeAsync(employee);
        return RedirectToAction("Index");
    }
}