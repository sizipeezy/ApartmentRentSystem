namespace ApartmentRentSystem.Controllers
{
    using ApartmentRentSystem.Core.Contracts;
    using ApartmentRentSystem.Core.Models.Agents;
    using ApartmentRentSystem.Infrastructure;
    using Microsoft.AspNetCore.Mvc;


    public class AgentsController : Controller
    {
        private readonly IAgentService agentService;

        public AgentsController(IAgentService agentService)
        {
            this.agentService = agentService;
        }

        [HttpGet]
        public IActionResult Become()
        {
            if (this.agentService.ExistsById(this.User.Id()))
            {
                return BadRequest();
            }

            return this.View();
        }

        [HttpPost]
        public IActionResult Become(BecomeAgentFormModel model)
        {
            var userId = this.User.Id();

            if (this.agentService.ExistsById(userId))
            {
                return BadRequest();
            }

            if (this.agentService.UserWithPhoneNumber(model.PhoneNumber))
            {
                ModelState.AddModelError(nameof(model.PhoneNumber),
                    "Phone number already exists.");
            }

            if (this.agentService.UserHasRents(userId))
            {
                ModelState.AddModelError(nameof(userId),
                    "User shouldn't have rents if wanna become Agent.");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            agentService.Create(userId, model.PhoneNumber);

            return this.RedirectToAction(nameof(ApartmentsController.All), "Apartments");
        }
    }
}
