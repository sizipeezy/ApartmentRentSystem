namespace ApartmentRentSystem.Areas.Admin.Controllers
{
    using ApartmentRentSystem.Core.Contracts;
    using Microsoft.AspNetCore.Mvc;

    public class RentsController : AdminController
    {
        private readonly IRentService rentService;

        public RentsController(IRentService rentService)
        {
            this.rentService = rentService;
        }

        public IActionResult All()
        {
            var rents = this.rentService.All();
            return View(rents);
        }
    }
}
