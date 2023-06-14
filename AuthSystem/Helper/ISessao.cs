using Microsoft.AspNetCore.Mvc;

namespace AuthSystem.Helper
{
    public class ISessao : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
