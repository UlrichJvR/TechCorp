using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TechCorp.Models;

namespace TechCorp.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<AdminUser> _signInManager;
        private readonly ILogger<AuthController> _logger;

        public AuthController(SignInManager<AdminUser> signInManager, ILogger<AuthController> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl ?? Url.Content("~/");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);

            if (result.Succeeded)
            {
                _logger.LogInformation("User logged in.");
                return RedirectToAction("Index", "Employee"); // or Redirect(returnUrl)
            }

            if (result.RequiresTwoFactor)
            {
                return RedirectToAction("LoginWith2fa", new { returnUrl, model.RememberMe });
            }

            if (result.IsLockedOut)
            {
                _logger.LogWarning("User account locked out.");
                return RedirectToAction("Lockout");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }
    }
}