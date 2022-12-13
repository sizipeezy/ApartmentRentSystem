namespace ApartmentRentSystem.Controllers
{
    using ApartmentRentSystem.Core.Models.Agents;
    using Microsoft.AspNetCore.Mvc;


    public class AgentsController : Controller
    {
        [HttpGet]
        public IActionResult Become() => this.View();

        [HttpPost]
        public IActionResult Become(BecomeAgentFormModel model)
        {
            return this.RedirectToAction(nameof(ApartmentsController.All), "Apartments");
        }
    }
}
