namespace ApartmentRentSystem.Areas.Admin.Controllers
{
    using ApartmentRentSystem.Core.Contracts;
    using ApartmentRentSystem.Core.Models.Users;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using static AdminConstants;

    public class UsersController : AdminController
    {
        private readonly IUserService userService;
        private readonly IMemoryCache memoryCache;

        public UsersController(IUserService userService, IMemoryCache memoryCache)
        {
            this.userService = userService;
            this.memoryCache = memoryCache;
        }

        public IActionResult All()
        {
            var users = this.memoryCache.Get<IEnumerable<UserModel>>(UsersCacheKey);

            if(users == null)
            {
                users = this.userService.GetUsers();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(10));

                this.memoryCache.Set(UsersCacheKey, users, cacheOptions);
            }
           
            return View(users);
        }
    }
}
