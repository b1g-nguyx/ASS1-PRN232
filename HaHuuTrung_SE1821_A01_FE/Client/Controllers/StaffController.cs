using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class StaffController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
