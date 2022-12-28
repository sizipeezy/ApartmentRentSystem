namespace ApartmentRentSystem.Controllers
{
    using ApartmentRentSystem.Core.Constants;
    using ApartmentRentSystem.Core.Contracts;
    using ApartmentRentSystem.Core.Models.Orders;
    using ApartmentRentSystem.Core.Models.ShoppingCart;
    using ApartmentRentSystem.Infrastructure.Data;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;

    public class OrdersController : Controller
    {
        private readonly ShoppingCart shoppingCart;
        private readonly IApartmentsService apartmentService;
        private readonly IOrderService orderService;

        public OrdersController(
            ShoppingCart shoppingCart,
            IApartmentsService apartmentService,
            IOrderService orderService)
        {
            this.shoppingCart = shoppingCart;
            this.apartmentService = apartmentService;
            this.orderService = orderService;
        }


        public IActionResult ShoppingCart()
        {
            var items = shoppingCart.GetItems();
            shoppingCart.Items = items;

            var viewModel = new ShoppingCartViewModel()
            {
                Cart = shoppingCart,
                Total = shoppingCart.Total(),
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Add(int id)
        {
            var item = this.apartmentService.Get(id); 

            if (item != null)
                shoppingCart.Add(item);
            else
                return BadRequest();

            TempData[MessageConstant.SuccessMessage] = "Apartment was added successfully to bag!";

            return this.RedirectToAction(nameof(ShoppingCart));
        }

        [HttpPost]
        public IActionResult Remove(int apartmentId)
        {
            var item = this.apartmentService.Get(apartmentId);

            if (item != null)
                shoppingCart.Remove(item);
            else
                return BadRequest();

            TempData[MessageConstant.WarningMessage] = "Apartment was removed successfully!";

            return this.RedirectToAction(nameof(shoppingCart));
        }

        [HttpPost]
        public async Task<IActionResult> CompleteOrder()
        {
            var items = shoppingCart.GetItems();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userEmailAddress = User.FindFirstValue(ClaimTypes.Email);

            var model = new StoreOrderModel()
            {
                EmailAddress = userEmailAddress,
                Items = items,
                UserId = userId,
            };

             await orderService.StoreOrder(model);
             await shoppingCart.ClearCart();

            TempData[MessageConstant.SuccessMessage] = "Order completed!";

            return this.RedirectToAction(nameof(ShoppingCart));
        }
    }
}
