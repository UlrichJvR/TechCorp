using System.ComponentModel.DataAnnotations;

namespace TechCorp.Models;

public class Employee
{
    private static readonly string[] ColorPalette = new[]
    {
        "#6f42c1", "#d63384", "#198754", "#0dcaf0", "#ffc107", "#fd7e14", "#dc3545", "#20c997", "#0d6efd"
    };
    public Employee(string fullName, string emailAddress, string jobTitle, string department)
    {
        FullName = fullName;
        EmailAddress = emailAddress;
        JobTitle = jobTitle;
        Department = department;
        ColorHex = GetRandomColor();
    }

    public Employee()
    {
        ColorHex = GetRandomColor();
    }
    
    private string GetRandomColor()
    {
        var rand = new Random(Guid.NewGuid().GetHashCode());
        return ColorPalette[rand.Next(ColorPalette.Length)];
    }

    public Guid EmployeeId { get; set; } = Guid.NewGuid();
    
    [Required(ErrorMessage = "Full Name is required")]
    public string FullName { get; set; }

    [Required(ErrorMessage = "Email Address is required")]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string EmailAddress { get; set; }

    [Required(ErrorMessage = "Job Title is required")]
    public string JobTitle { get; set; } 

    [Required(ErrorMessage = "Department is required")]
    public string Department { get; set; } 
    
    public string ColorHex { get; set; } 
}