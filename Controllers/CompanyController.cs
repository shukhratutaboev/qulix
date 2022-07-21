using Microsoft.AspNetCore.Mvc;
using Qulix.Entities;
using Qulix.Models;
using Qulix.Repositories;

namespace Qulix.Controllers;
public class CompanyController : Controller
{
    private readonly ILogger<CompanyController> _logger;
    private readonly ICompanyRepository _repo;

    public CompanyController(ILogger<CompanyController> logger, ICompanyRepository repo)
    {
        _logger = logger;
        _repo = repo;
    }

    public async Task<IActionResult> Index()
    {
        var result = await _repo.GetCompaniesAsync();
        if(!result.IsSuccess) return View();
        return View(result.Companies ?? new List<Company>());
    }

    public IActionResult Create()
        => View();

    [HttpPost]
    public async Task<IActionResult> Create(CompanyViewModel model)
    {
        if(!ModelState.IsValid) return View(model);
        var company = new Company()
        {
            Name = model.Name,
            OrganizationalForm = model.OrganizationalForm
        };
        var result = await _repo.AddCompanyAsync(company);
        return RedirectToAction("Index");
    }
}