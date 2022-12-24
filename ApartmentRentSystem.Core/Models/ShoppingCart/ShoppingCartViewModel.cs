namespace ApartmentRentSystem.Core.Models.ShoppingCart
{
    using ApartmentRentSystem.Infrastructure.Data;
    public class ShoppingCartViewModel
    {
        public ShoppingCart Cart { get; set; }

        public double Total { get; set; }
    }
}
