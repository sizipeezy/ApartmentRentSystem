namespace ApartmentRentSystem.Controllers
{
    using ApartmentRentSystem.Core.Contracts;
    using ApartmentRentSystem.Core.Models.Error;
    using ApartmentRentSystem.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;
    using static AdminConstants;

    public class HomeController : Controller
    {
        private readonly IApartmentsService apartmentService;

        public HomeController(IApartmentsService apartmentService)
        {
            this.apartmentService = apartmentService;
        }

        public IActionResult Index()
        {
            if (this.User.IsInRole(AdminRoleName))
            {
                return this.RedirectToAction("Index", "Home", new { area = "Admin" });
            }

            var apartments = this.apartmentService.GetLastThree();

            return this.View(apartments);
        }
        
        public IActionResult Privacy() => View();

        public IActionResult NotFound(int statusCode)
        {
            var viewModel = new HttpErrorViewModel
            {
                StatusCode = statusCode,
            };

            if (statusCode == 404)
            {
                return this.View(viewModel);
            }

            return this.View(
                "Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}