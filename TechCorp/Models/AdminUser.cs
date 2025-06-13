using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace TechCorp.Models
{
    public class AdminUser : IdentityUser
    {
        public string FullName { get; set; }
        public string Department { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Role { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}