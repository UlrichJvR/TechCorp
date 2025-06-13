using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechCorp.Models;
using TechCorp.Services;

namespace TechCorp.Controllers;

[Authorize] // Requires logged-in user
public class EmployeeController : Controller
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    // GET: /Employee
    public IActionResult Index()
    {
        var employees = _employeeService.GetAll();
        return View(employees);
    }

    // GET: /Employee/Details/{id}
    public IActionResult Details(Guid? id)
    {
        if (id.HasValue)
        {
            var employee = _employeeService.GetById(id.Value);
            if (employee == null)
                return NotFound();

            return View(employee); // Existing employee
        }

        return View(new Employee()); // New employee
    }


    // GET: /Employee/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: /Employee/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Employee employee)
    {
        if (!ModelState.IsValid)
            return View(employee);

        _employeeService.Add(employee);
        return RedirectToAction(nameof(Index));
    }

    // GET: /Employee/Edit/{id}
    public IActionResult Edit(Guid id)
    {
        var employee = _employeeService.GetById(id);
        if (employee == null)
            return NotFound();

        return View(employee);
    }

    // POST: /Employee/Edit/{id}
    [HttpPost]
    [ValidateAntiForgeryToken]  
    public IActionResult Edit(Employee employee)
    {
        if (!ModelState.IsValid)
            return View("Details", employee);

        if (employee.EmployeeId == Guid.Empty || _employeeService.GetById(employee.EmployeeId) == null)
        {
            _employeeService.Add(employee); // New
        }
        else
        {
            _employeeService.Update(employee); // Existing
        }

        return RedirectToAction("Index");
    }


    // GET: /Employee/Delete/{id}
    public IActionResult Delete(Guid id)
    {
        var employee = _employeeService.GetById(id);
        if (employee == null)
            return NotFound();

        return View(employee);
    }

    // POST: /Employee/Delete/{id}
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(Guid id)
    {
        _employeeService.Delete(id);
        return RedirectToAction(nameof(Index));
    }
}