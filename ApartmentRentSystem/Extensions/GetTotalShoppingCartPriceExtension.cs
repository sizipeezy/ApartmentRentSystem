namespace ApartmentRentSystem.Extensions
{
    using ApartmentRentSystem.Infrastructure.Data;


    public static class GetTotalShoppingCartPriceExtension
    {
        public static decimal Total(this ShoppingCart cart)
        {
            decimal total = 0;
            foreach (var item in cart.Items)
            {
                total += item.Apartment.PricePerMonth;
            }

            return total;
        }
    }
}
