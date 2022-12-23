namespace ApartmentRentSystem.Controllers
{
    using ApartmentRentSystem.Core.Contracts;
    using ApartmentRentSystem.Core.Models;
    using ApartmentRentSystem.Extensions;
    using ApartmentRentSystem.Infrastructure;
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using static ApartmentRentSystem.Core.Constants.MessageConstant;

    public class ApartmentsController : Controller
    {
        private readonly IApartmentsService apartmentsService;
        private readonly ICategoryService categoryService;
        private readonly IAgentService agentService;
        private readonly IRentService rentService;
        private readonly IMapper mapper;
        public ApartmentsController(
            IApartmentsService apartmentsService,
            ICategoryService categoryService,
            IAgentService agentService,
            IRentService rentService,
            IMapper mapper)
        {
            this.apartmentsService = apartmentsService;
            this.categoryService = categoryService;
            this.agentService = agentService;
            this.rentService = rentService;
            this.mapper = mapper;
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
            TempData[SuccessMessage] = "You have successfully added a apartment!";

            return this.RedirectToAction(nameof(All));
        }

        [HttpGet]
        public IActionResult Details(int id, string information)
        {
            if (!this.apartmentsService.Exists(id))
            {
                return BadRequest();
            }


            var apartment = this.apartmentsService.ApartmentDetailsById(id);

            if(information != apartment.GetInformation())
            {
                return BadRequest();
            }

            return this.View(apartment);


        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (!this.apartmentsService.Exists(id))
            { 
                return BadRequest();
            }

            if (!this.agentService.HasAgentWithId(id, this.User.Id())
                && !this.User.IsAdmin())
                Unauthorized();

            var apartment = this.apartmentsService.ApartmentDetailsById(id);

            var categoryId = this.categoryService.GetApartmentCategoryId(apartment.Id);

            var apartmentModel = this.mapper.Map<AddApartmentModel>(apartment);

            apartmentModel.CategoryId = categoryId;
            apartmentModel.ApartmentCategories = categoryService.AllCategories();

            return this.View(apartmentModel); 
        }

        [HttpPost]
        public IActionResult Edit(int id, AddApartmentModel model)
        {
            if (!this.apartmentsService.Exists(id))
            {
                return BadRequest();
            }

            if (!this.agentService.HasAgentWithId(id, this.User.Id())
                 && !this.User.IsAdmin())
                Unauthorized();

            if (!this.categoryService.CategoryExists(model.CategoryId))
            {
                this.ModelState.AddModelError(nameof(model.CategoryId), "category doesn't exists.");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            this.apartmentsService.Edit(id, model);

            TempData[WarningMessage] = "You have successfully edited a Apartment.";

            return this.RedirectToAction(nameof(Details), new { id = id , information = model.GetInformation()});
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (!this.apartmentsService.Exists(id))
            {
                return BadRequest();
            }

            if (!this.agentService.HasAgentWithId(id, this.User.Id())
                && !this.User.IsAdmin())
                Unauthorized();

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

            if (!this.agentService.HasAgentWithId(id, this.User.Id())
                  && !this.User.IsAdmin())
                Unauthorized();

            this.apartmentsService.Delete(id);

            TempData[WarningMessage] = "You have successfully removed an Apartment!";

            return this.RedirectToAction(nameof(All));
        }

        [HttpPost]
        public  IActionResult Rent(int id)
        {
            if (this.rentService.IsRented(id))
            {
                return BadRequest();
            }

            if (!this.apartmentsService.Exists(id))
            {
                return BadRequest();
            }

            if(!this.agentService.ExistsById(this.User.Id()) &&
                !this.User.IsAdmin())
            {
                return Unauthorized();
            }

            this.rentService.Rent(id, this.User.Id());

            TempData[SuccessMessage] = "Successfully rented apartment!";

            return this.RedirectToAction(nameof(Mine));
        }

        [HttpPost]
        public IActionResult Leave(int id)
        {
            if (!this.apartmentsService.Exists(id))
            {
                return BadRequest();
            }

            if(!this.rentService.ByUserId(id, this.User.Id()))
            {
                return Unauthorized();
            }

            this.rentService.Leave(id);
            TempData[WarningMessage] = "Successfully leaved apartment";

            return this.RedirectToAction(nameof(Mine));
        }
    }
}
