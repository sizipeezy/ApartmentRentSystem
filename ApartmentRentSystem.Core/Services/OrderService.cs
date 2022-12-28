namespace ApartmentRentSystem.Core.Services
{
    using ApartmentRentSystem.Core.Contracts;
    using ApartmentRentSystem.Core.Models.Orders;
    using ApartmentRentSystem.Infrastructure.Data;
    using System.Threading.Tasks;


    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext data;

        public OrderService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public async Task StoreOrder(StoreOrderModel model)
        {
            var order = new Order()
            {
                Email = model.EmailAddress,
                ApplicationUserId = model.UserId
            };

            await data.Orders.AddAsync(order);
            await data.SaveChangesAsync();

            foreach (var item in model.Items)
            {
                var orderItem = new OrderItem()
                {
                    Amount = item.Quantity,
                    Apartment = item.Apartment,
                    ApartmentId = item.Apartment.Id,
                    OrderId = order.Id,
                    Price = item.Apartment.PricePerMonth,
                };

                await data.OrderItems.AddRangeAsync(orderItem);
            }

            await data.SaveChangesAsync();
        }
    }
}
