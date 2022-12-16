namespace ApartmentRentSystem.Core.Services
{
    using ApartmentRentSystem.Core.Contracts;
    using ApartmentRentSystem.Infrastructure.Data;
    using System;


    public class RentService : IRentService
    {
       private readonly ApplicationDbContext data;

        public RentService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public bool ByUserId(int apartmentId, string userId)
        {
            var apartment = this.data.Apartments.Find(apartmentId);

            if(apartment == null)
            {
                return false;
            }

            if(apartment.RenterId != userId)
            {
                return false;
            }

            return true;
        }

        public bool IsRented(int id) => this.data.Apartments.Find(id).RenterId != null;

        public void Rent(int apartmentId, string userId)
        {
            var apartment = this.data.Apartments.Find(apartmentId);

            apartment.RenterId = userId;

            this.data.SaveChanges();
        }
    }
}
