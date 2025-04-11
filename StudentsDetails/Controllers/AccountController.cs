using Microsoft.AspNetCore.Mvc;

namespace StudentsDetails.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
