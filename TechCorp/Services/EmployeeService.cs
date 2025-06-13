using TechCorp.Models;

namespace TechCorp.Services;

public class EmployeeService : IEmployeeService
{
    private readonly List<Employee> _employees;
    private readonly ILogger<EmployeeService> _logger;
    
    // dummmy data for testing
    private static readonly string[] FirstNames = { "Ulrich", "Nkosivile", "Liam", "Emma", "Noah", "Olivia", "Elijah", "Ava", "Lucas", "Isabella", "Mason", "Mia" };
    private static readonly string[] LastNames = { "Janse van Rensburg", "Mazima", "Smith", "Johnson", "Brown", "Taylor", "Anderson", "Thomas", "Jackson", "White", "Harris", "Martin" };

    private static readonly string[] JobTitles = { "Software Developer", "Project Manager", "System Analyst", "UX Designer", "QA Engineer" };
    private static readonly string[] Departments = { "IT", "Operations", "HR", "Marketing", "Finance" };


    public EmployeeService(ILogger<EmployeeService> logger)
    {
        _employees = new List<Employee>();
        // dummy data for testing
        // for (int i = 0; i < 5; i++)
        // {
        _employees.Add(new Employee( "Ulrich Janse van Rensburg",  "8sg26y144@vossie.net", "Software Developer",  "IT"));
        _employees.Add(new Employee("Nkosivile Mazima",  "Nkosivile.Mazima@vossie.net",  "Project Manager",  "Operations" ));
        // }
        for (int i = 0; i < 1; i++)
        {
            var name = GenerateRandomName();
            var email = GenerateEmail(name);
            var job = GetRandomJobTitle();
            var dept = GetRandomDepartment();

            _employees.Add(new Employee(name, email, job, dept));
        }
       
        _logger = logger;
        _logger.LogInformation("EmployeeService created.");
    }
    
    public List<Employee> GetAll() => _employees;

    public Employee? GetById(Guid id) => _employees.FirstOrDefault(e => e.EmployeeId == id);

    public void Add(Employee employee)
    {
        employee.EmployeeId = Guid.NewGuid();
        _employees.Add(employee);
    }

    public void Update(Employee employee)
    {
        var existing = GetById(employee.EmployeeId);
        if (existing == null) return;

        existing.FullName = employee.FullName;
        existing.EmailAddress = employee.EmailAddress;
        existing.JobTitle = employee.JobTitle;
        existing.Department = employee.Department;
    }

    public void Delete(Guid id)
    {
        var employee = GetById(id);
        if (employee != null)
        {
            _employees.Remove(employee);
        }
    }
    
    // random name generator helper
    private static Random _rand = new();

    private static string GenerateRandomName()
    {
        var first = FirstNames[_rand.Next(FirstNames.Length)];
        var last = LastNames[_rand.Next(LastNames.Length)];
        return $"{first} {last}";
    }

    private static string GenerateEmail(string fullName)
    {
        var user = fullName.ToLower().Replace(" ", ".");
        return $"{user}@vossie.net";
    }

    private static string GetRandomJobTitle() => JobTitles[_rand.Next(JobTitles.Length)];
    private static string GetRandomDepartment() => Departments[_rand.Next(Departments.Length)];

}