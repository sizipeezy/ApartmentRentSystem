namespace ApartmentRentSystem.Areas.Admin.Controllers
{
    using ApartmentRentSystem.Areas.Admin.Models;
    using ApartmentRentSystem.Core.Contracts;
    using ApartmentRentSystem.Infrastructure;
    using Microsoft.AspNetCore.Mvc;
    public class ApartmentsController : AdminController
    {
        private readonly IApartmentsService apartmentsService;
        private readonly IAgentService agentService;
        public ApartmentsController(IApartmentsService apartmentsService, IAgentService agentService)
        {
            this.apartmentsService = apartmentsService;
            this.agentService = agentService;
        }

        public IActionResult Mine()
        {
            var result = new MyApartmentModel();

            var adminUser = this.User.Id();
            result.RentedApartments = this.apartmentsService.AllApartmentsByUser(adminUser);

            var adminAgent = this.agentService.GetAgentId(adminUser);
            result.AddedApartments = this.apartmentsService.AllApartmentsByAgent(adminAgent);

            return View(result);
        }
    }
}
