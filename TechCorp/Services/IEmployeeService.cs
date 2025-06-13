using TechCorp.Models;

namespace TechCorp.Services;

public interface IEmployeeService
{
    public Employee? GetById(Guid id);
    public List<Employee> GetAll();
    public void Add(Employee employee);
    public void Update(Employee employee);
    public void Delete(Guid id);
}