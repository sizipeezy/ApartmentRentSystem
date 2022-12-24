namespace ApartmentRentSystem.Infrastructure.ViewComponents
{
    using ApartmentRentSystem.Infrastructure.Data;
    using Microsoft.AspNetCore.Mvc;


    public class ShoppingCartSummary : ViewComponent
    {
        private readonly ShoppingCart cart;

        public ShoppingCartSummary(ShoppingCart cart)
        {
            this.cart = cart;
        }

        public IViewComponentResult Invoke()
        {
            var items = this.cart.GetItems();

            return this.View(items.Count);
        }
    }
}
