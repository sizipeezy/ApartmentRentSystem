namespace ApartmentRentSystem.Controllers
{
    using ApartmentRentSystem.Core.Contracts;
    using ApartmentRentSystem.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;


    public class HomeController : Controller
    {
        private readonly IApartmentsService houseService;

        public HomeController(IApartmentsService houseService)
        {
            this.houseService = houseService;
        }

        public IActionResult Index()
        {
            var apartments = this.houseService.GetLastThree();
            return this.View(apartments);
        }
        
        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}