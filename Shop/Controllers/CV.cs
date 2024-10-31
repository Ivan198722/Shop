using Microsoft.AspNetCore.Mvc;

namespace Shop.Controllers
{
    public class CV : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
