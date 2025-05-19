using CocusFlightPlanner.Application.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CocusFlightPlanner.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<AircraftController> _logger;

        public HomeController(ILogger<AircraftController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
   

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
