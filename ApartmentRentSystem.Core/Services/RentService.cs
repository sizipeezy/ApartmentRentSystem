namespace ApartmentRentSystem.Core.Services
{
    using ApartmentRentSystem.Areas.Admin.Models;
    using ApartmentRentSystem.Core.Contracts;
    using ApartmentRentSystem.Infrastructure.Data;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;

    public class RentService : IRentService
    {
        private readonly ApplicationDbContext data;
        private readonly IMapper mapper;
        public RentService(ApplicationDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public IEnumerable<RentModel> All()
        {
            return this.data.Apartments.Include(x => x.Agent.User)
                .Include(x => x.Renter)
                .Where(x => x.RenterId != null)
                .ProjectTo<RentModel>(this.mapper.ConfigurationProvider)
                .ToList();
        }

        public bool ByUserId(int apartmentId, string userId)
        {
            var apartment = this.data.Apartments.Find(apartmentId);

            if (apartment == null)
            {
                return false;
            }

            if (apartment.RenterId != userId)
            {
                return false;
            }

            return true;
        }

        public bool IsRented(int id) => this.data.Apartments.Find(id).RenterId != null;

        public void Leave(int apartmentId)
        {
            var apartment = this.data.Apartments.Find(apartmentId);

            apartment.RenterId = null;

            this.data.SaveChanges();
        }

        public void Rent(int apartmentId, string userId)
        {
            var apartment = this.data.Apartments.Find(apartmentId);

            apartment.RenterId = userId;

            this.data.SaveChanges();
        }
    }
}
