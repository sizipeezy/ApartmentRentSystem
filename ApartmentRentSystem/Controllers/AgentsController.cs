namespace ApartmentRentSystem.Controllers
{
    using ApartmentRentSystem.Core.Constants;
    using ApartmentRentSystem.Core.Contracts;
    using ApartmentRentSystem.Core.Models.Agents;
    using ApartmentRentSystem.Infrastructure;
    using Microsoft.AspNetCore.Mvc;
    using static ApartmentRentSystem.Core.Constants.MessageConstant;

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

            var model = new BecomeAgentFormModel();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Become(BecomeAgentFormModel model)
        {
            var userId = User.Id();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (agentService.ExistsById(userId))
            {
                TempData[MessageConstant.ErrorMessage] = "Вие вече сте Агент";

                return RedirectToAction("Index", "Home");
            }

            if (agentService.UserWithPhoneNumber(model.PhoneNumber))
            {
                TempData[MessageConstant.ErrorMessage] = "Телефона вече съществува";

                return RedirectToAction("Index", "Home");
            }

            if (agentService.UserHasRents(userId))
            {
                TempData[MessageConstant.ErrorMessage] = "Не трябва да имате наеми за да станете агент";

                return RedirectToAction("Index", "Home");
            }

            await agentService.Create(userId, model.PhoneNumber);

            TempData[SuccessMessage] = "You have became Agent successfully!";

            return RedirectToAction("All", "Apartments");
        }
    }
}
