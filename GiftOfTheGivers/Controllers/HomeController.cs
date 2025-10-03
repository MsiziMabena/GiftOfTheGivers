using Microsoft.AspNetCore.Mvc;

namespace GiftOfTheGivers.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
    }
}
