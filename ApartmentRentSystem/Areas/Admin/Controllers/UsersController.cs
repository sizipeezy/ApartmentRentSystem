namespace ApartmentRentSystem.Areas.Admin.Controllers
{
    using ApartmentRentSystem.Core.Contracts;
    using Microsoft.AspNetCore.Mvc;


    public class UsersController : AdminController
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        public IActionResult All()
        {
            var users = this.userService.GetUsers();
            return View(users);
        }
    }
}
