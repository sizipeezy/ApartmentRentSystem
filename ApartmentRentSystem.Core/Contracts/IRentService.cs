namespace ApartmentRentSystem.Core.Contracts
{
    using ApartmentRentSystem.Areas.Admin.Models;

    public interface IRentService
    {
        bool IsRented(int id);

        bool ByUserId(int apartmentId, string userId);

        void Rent(int apartmentId, string userId);

        void Leave(int apartmentId);

        IEnumerable<RentModel> All();
    }
}
