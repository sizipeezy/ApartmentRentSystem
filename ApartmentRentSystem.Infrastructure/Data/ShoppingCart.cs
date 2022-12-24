namespace ApartmentRentSystem.Infrastructure.Data
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System.Collections.Generic;


    public class ShoppingCart
    {
        public ApplicationDbContext data { get; set; }

        public string ShoppingCartId { get; set; }

        public List<Item> Items { get; set; } = new List<Item>();

        public ShoppingCart(ApplicationDbContext data)
        {
            this.data = data;
        }

        public static ShoppingCart GetShoppingCart(IServiceProvider service)
        {
            ISession session = service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            var context = service.GetRequiredService<ApplicationDbContext>();

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId};   
        }

        public List<Item> GetItems() => 
                this.data
                .Items
                .Where(x => x.ShoppingCartId == ShoppingCartId)
                .Include(x => x.Apartment)
                .ToList();

        public double Total() =>
            (double)this.data.Items
            .Where(x => x.ShoppingCartId == ShoppingCartId)
            .Select(x => x.Apartment.PricePerMonth * x.Quantity)
            .Sum();

        public void Add(int id)
        {
            var item = this.data.Items.FirstOrDefault(x => x.Id == id && x.ShoppingCartId == ShoppingCartId);

            var apartment = this.data.Apartments.FirstOrDefault(x => x.Id == id);

            if(item == null)
            {
                item = new Item
                {
                    Quantity = 1,
                    Apartment = apartment,
                    ShoppingCartId = ShoppingCartId
                };

                data.Items.Add(item);
            }
            else
            {
                item.Quantity++;
            }

            data.SaveChanges();
        }
    }
}
