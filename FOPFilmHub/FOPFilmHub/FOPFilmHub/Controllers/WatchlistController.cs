using Microsoft.AspNetCore.Mvc;

namespace FOPFilmHub.Controllers
{
    public class WatchlistController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
