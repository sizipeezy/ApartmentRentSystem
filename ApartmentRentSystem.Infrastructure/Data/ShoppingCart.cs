namespace ApartmentRentSystem.Infrastructure.Data
{
    using Microsoft.AspNetCore.Http;
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
    }
}
