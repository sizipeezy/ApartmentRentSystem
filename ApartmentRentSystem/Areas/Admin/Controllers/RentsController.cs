namespace ApartmentRentSystem.Areas.Admin.Controllers
{
    using ApartmentRentSystem.Areas.Admin.Models;
    using ApartmentRentSystem.Core.Contracts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using static AdminConstants;

    public class RentsController : AdminController
    {
        private readonly IRentService rentService;
        private readonly IMemoryCache memoryCache;

        public RentsController(IRentService rentService, IMemoryCache memoryCache)
        {
            this.rentService = rentService;
            this.memoryCache = memoryCache;
        }

        public IActionResult All()
        {
            var rents = this.memoryCache.Get<IEnumerable<RentModel>>(AdminConstants.RentsCacheKey);

            if(rents == null)
            {
                rents = this.rentService.All();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

                this.memoryCache.Set(AdminConstants.RentsCacheKey, rents, cacheOptions);
            }
           
            return View(rents);
        }
    }
}
