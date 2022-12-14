namespace ApartmentRentSystem.Controllers
{
    using ApartmentRentSystem.Core.Contracts;
    using ApartmentRentSystem.Core.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class ApartmentsController : Controller
    {
        private readonly IApartmentsService apartmentsService;
        private readonly ICategoryService categoryService;
       
        public ApartmentsController(IApartmentsService apartmentsService, ICategoryService categoryService)
        {
            this.apartmentsService = apartmentsService;
            this.categoryService = categoryService;
        }

        public IActionResult All()
        {
            return this.View();
        }

        public IActionResult Mine()
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult Add()
        {

            return this.View();
        }

        [HttpPost]
        public IActionResult Add(AddApartmentModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            if (this.categoryService.CategoryExists(model.CategoryId))
            {
                this.ModelState.AddModelError(nameof(model.CategoryId),
                    "Category doesn't exists.");
            }

            apartmentsService.AddAsync(model);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            return this.View();
        }


        [HttpPost]
        public IActionResult Details(int id, ApartmentDetailsModel model)
        {
            //Iguard check for the Id

            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            return RedirectToAction(nameof(All));

        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return this.View(new ApartmentEditModel());   
        }

        [HttpPost]
        public IActionResult Edit(int id, ApartmentEditModel model)
        {
            return this.RedirectToAction(nameof(Details), new { id = id });
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return this.View(new ApartmentDetailsModel());
        }

        [HttpPost]
        [Authorize]
        public IActionResult Delete(ApartmentDetailsModel model)
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
