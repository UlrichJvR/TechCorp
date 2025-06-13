using Microsoft.AspNetCore.Mvc;

public class RedirectController : Controller
{
    public IActionResult Index()
    {
        if (User.Identity != null && User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Index", "Employee");
        }
        else
        {
            return RedirectToAction("Index", "Home");
        }
    }
}