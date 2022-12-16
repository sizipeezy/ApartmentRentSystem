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
        public IActionResult Edit(int id)
        {
            if (!this.apartmentsService.Exists(id))
            { 
                return BadRequest();
            }

            var apartment = this.apartmentsService.ApartmentDetailsById(id);

            var categoryId = this.categoryService.GetApartmentCategoryId(apartment.Id);

            var apartmentModel = new AddApartmentModel
            {
                Id = apartment.Id,
                Address = apartment.Address,
                CategoryId = categoryId,
                Description = apartment.Description,
                PricePerMonth = apartment.PricePerMonth,
                ImageUrl = apartment.ImageUrl,
                ApartmentCategories = this.categoryService.AllCategories(),
                Title = apartment.Title
            };

            return this.View(apartmentModel); 
        }

        [HttpPost]
        public IActionResult Edit(int id, AddApartmentModel model)
        {
            if (!this.apartmentsService.Exists(id))
            {
                return BadRequest();
            }

            if(!this.agentService.HasAgentWithId(id, this.User.Id()))
            {
                return Unauthorized();
            }

            if (!this.categoryService.CategoryExists(model.CategoryId))
            {
                this.ModelState.AddModelError(nameof(model.CategoryId), "category doesn't exists.");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            this.apartmentsService.Edit(id, model);

            return this.RedirectToAction(nameof(Details), new { id = id });
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (!this.apartmentsService.Exists(id))
            {
                return BadRequest();
            }

            var viewModel = apartmentsService.ApartmentDetailsById(id);

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Delete(int id, ApartmentDetailsModel model)
        {
            if (!this.apartmentsService.Exists(id))
            {
                return BadRequest();
            }

            if (!this.agentService.HasAgentWithId(model.Id, this.User.Id()))
            {
                return Unauthorized();
            }

            this.apartmentsService.Delete(id);

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
