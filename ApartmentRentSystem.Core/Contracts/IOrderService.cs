namespace ApartmentRentSystem.Core.Contracts
{
    using ApartmentRentSystem.Core.Models.Orders;


    public interface IOrderService
    {
        Task StoreOrder(StoreOrderModel model);
    }
}
