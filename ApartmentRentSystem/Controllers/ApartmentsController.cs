namespace ApartmentRentSystem.Controllers
{
    using ApartmentRentSystem.Core.Contracts;
    using ApartmentRentSystem.Core.Models;
    using ApartmentRentSystem.Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.VisualBasic;

    public class ApartmentsController : Controller
    {
        private readonly IApartmentsService apartmentsService;
        private readonly ICategoryService categoryService;
        private readonly IAgentService agentService;
        public ApartmentsController(
            IApartmentsService apartmentsService,
            ICategoryService categoryService,
            IAgentService agentService)
        {
            this.apartmentsService = apartmentsService;
            this.categoryService = categoryService;
            this.agentService = agentService;
        }

        [HttpGet]
        public IActionResult All([FromQuery]AllApartmentsQueryModel model)
        {
            var result = apartmentsService.All(
                model.Category,
                model.SearchTerm,
                model.ApartmentSorting,
                model.CurrentPage,
                AllApartmentsQueryModel.ApartmentsPerPage
                );

            model.TotalApartmentsCount = result.TotalApartments;
            model.Categories = categoryService.AllCategoryNames();
            model.Apartments = result.AllApartments;

            return this.View(model);
        }

        [Authorize]
        public IActionResult Mine()
        {
            var userId = this.User.Id();
            IEnumerable<ApartmentModel> apartments = null;

            if (this.agentService.ExistsById(userId))
            {
                var currentAgentId = this.agentService.GetAgentId(userId);

                apartments = this.apartmentsService.AllApartmentsByAgent(currentAgentId);
            }
            else
            {
               apartments = this.apartmentsService.AllApartmentsByUser(userId);
            }

            return this.View(apartments);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return this.View(new AddApartmentModel()
            {
                ApartmentCategories = categoryService.AllCategories()
            });
        }

        [HttpPost]
        public IActionResult Add(AddApartmentModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.ApartmentCategories = categoryService.AllCategories();
                return View(model);
            }

            if (!this.categoryService.CategoryExists(model.CategoryId))
            {
                this.ModelState.AddModelError(nameof(model.CategoryId),
                    "Category doesn't exists.");
            }

            var agentId = agentService.GetAgentId(this.User.Id());

            apartmentsService.AddAsync(model, agentId);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            if (!this.apartmentsService.Exists(id))
            {
                return BadRequest();
            }

            var apartment = this.apartmentsService.ApartmentDetailsById(id);

            return this.View(apartment);


        }

        [HttpGet]
        public IActionResult Edit()
        {
            //return this.View(new ApartmentEditModel());   
            return this.View();
        }

        [HttpPost]
        public IActionResult Edit(int id)// ApartmentEditModel model)
        {
            return this.RedirectToAction(nameof(Details), new { id = id });
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Delete()//ApartmentDetailsModel model)
        {
            return this.RedirectToAction(nameof(All));
        }

        [HttpPost]
        public  IActionResult Rent(int id)
        {
            return this.RedirectToAction(nameof(Mine));
        }

        [HttpPost]
        public IActionResult Leave(int id)
        {
            return this.RedirectToAction(nameof(Mine));
        }
    }
}
