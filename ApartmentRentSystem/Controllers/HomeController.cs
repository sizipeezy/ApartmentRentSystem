namespace ApartmentRentSystem.Controllers
{
    using ApartmentRentSystem.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;


    public class HomeController : Controller
    {
        public IActionResult Index() => View();
        
        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}