using AppInsight.Models;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AppInsight.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger; 
        }

        public IActionResult Index()
        {
            _logger.LogWarning("An example of a Warning trace..");
            _logger.LogError("An example of an Error level message");
            return View();
        }

        public IActionResult Privacy()
        {
            _logger.Log(LogLevel.Information,"Privacy page");
            try
            {
                throw new NullReferenceException("null value");
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
