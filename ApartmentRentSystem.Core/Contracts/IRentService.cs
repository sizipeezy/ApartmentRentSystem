namespace ApartmentRentSystem.Core.Contracts
{
    public interface IRentService
    {
        bool IsRented(int id);

        bool ByUserId(int apartmentId, string userId);

        void Rent(int apartmentId, string userId);
    }
}
