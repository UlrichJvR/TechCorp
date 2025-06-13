using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TechCorp.Models;
using TechCorp.Services;

public class DashboardModel : PageModel
{
    private readonly IEmployeeService _employeeService;

    public DashboardModel(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [BindProperty]
    public Employee NewEmployee { get; set; } = new();

    public List<Employee> Employees { get; set; } = new();

    public void OnGet()
    {
        Employees = _employeeService.GetAll();
    }

    public IActionResult OnPostAdd()
    {
        if (!ModelState.IsValid)
        {
            Employees = _employeeService.GetAll();
            return Page();
        }

        _employeeService.Add(NewEmployee);
        return RedirectToPage();
    }

    public IActionResult OnPostDelete(Guid id)
    {
        _employeeService.Delete(id);
        return RedirectToPage();
    }
}