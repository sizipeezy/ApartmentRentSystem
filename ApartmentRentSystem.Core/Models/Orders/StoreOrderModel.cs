namespace ApartmentRentSystem.Core.Models.Orders
{
    using ApartmentRentSystem.Infrastructure.Data;
    public class StoreOrderModel
    {
        public string UserId { get; set; } = null!;

        public string EmailAddress { get; set; } = null!;
        public List<Item> Items { get; set; } = new List<Item>();
    }
}
