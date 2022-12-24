namespace ApartmentRentSystem.Core.Services
{
    using ApartmentRentSystem.Core.Contracts;
    using ApartmentRentSystem.Core.Models;
    using ApartmentRentSystem.Core.Models.Agents;
    using ApartmentRentSystem.Infrastructure.Data;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;

    public class ApartmentsService : IApartmentsService
    {
        private readonly ApplicationDbContext data;
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public ApartmentsService(ApplicationDbContext db, IUserService userService, IMapper mapper)
        {
            this.data = db;
            this.userService = userService;
            this.mapper = mapper;
        }

        public void AddAsync(AddApartmentModel model, int agentId)
        {
            var apartment = new Apartment()
            {
                Address = model.Address,
                CategoryId = model.CategoryId,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                PricePerMonth = model.PricePerMonth,
                Title = model.Title,
                AgentId = agentId
            };

            this.data.Apartments.Add(apartment);
            data.SaveChanges();
        }

        public ApartmentQueryModel All(string? category = null, string? searchTerm = null, ApartmentSorting sorting = ApartmentSorting.Newest, int currentPage = 1, int apartmentsPerPage = 1)
        {
            var result = new ApartmentQueryModel();
            var apartmentsQuery = this.data.Apartments.Where(x => x.IsActive).AsQueryable();

            if (!string.IsNullOrEmpty(category))
            {
                apartmentsQuery = this.data.Apartments.Where(x => x.Category.Name == category);
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                apartmentsQuery = this.data.Apartments.Where(x => x.Title.StartsWith(searchTerm.ToLower()));
            }

            apartmentsQuery = sorting switch
            {
                ApartmentSorting.Price => apartmentsQuery
                    .OrderBy(x => x.PricePerMonth),
                ApartmentSorting.NotRented => apartmentsQuery
                 .OrderBy(x => x.RenterId != null),
                _ => apartmentsQuery.OrderByDescending(x => x.Id)
            };

            result.AllApartments = apartmentsQuery
                .Skip((currentPage - 1) * apartmentsPerPage)
                .Take(apartmentsPerPage)
                .ProjectTo<ApartmentModel>(this.mapper.ConfigurationProvider)
                .ToList();

            result.TotalApartments = apartmentsQuery.Count();

            return result;
        }

        public IEnumerable<ApartmentModel> AllApartmentsByAgent(int agentId)
        {
            var apartments = this.data.Apartments
                .Where(c => c.IsActive)
                .Where(c => c.AgentId == agentId)
                .ProjectTo<ApartmentModel>(this.mapper.ConfigurationProvider)
                .ToList();

            return apartments;
        }

        public IEnumerable<ApartmentModel> AllApartmentsByUser(string userId)
        {
            var apartments = this.data.Apartments
                .Where(x => x.RenterId == userId)
                .ProjectTo<ApartmentModel>(this.mapper.ConfigurationProvider)
                .ToList();

            return apartments;
        }

        public ApartmentDetailsModel ApartmentDetailsById(int id)
        {
            var apartment = this.data.Apartments.Where(x => x.Id == id)
                .ProjectTo<ApartmentDetailsModel>(this.mapper.ConfigurationProvider)
                .FirstOrDefault();

            return apartment;
        }

        public void Delete(int id)
        {
            var apartment = this.data.Apartments.FirstOrDefault(x => x.Id == id);

            this.data.Apartments.Remove(apartment);

            this.data.SaveChanges();
        }

        public void Edit(int apartmentId, AddApartmentModel model)
        {
            var apartment = this.data.Apartments.Find(apartmentId);

            apartment.Description = model.Description;
            apartment.ImageUrl = model.ImageUrl;
            apartment.PricePerMonth = model.PricePerMonth;
            apartment.Title = model.Title;
            apartment.Address = model.Address;
            apartment.CategoryId = model.CategoryId;

            this.data.SaveChanges();
        }

        public bool Exists(int id) => this.data.Apartments.Any(x => x.Id == id);

        public Apartment Get(int id)
            => this.data.Apartments
            .Where(x => x.IsActive)
            .Include(x => x.Category)
            .Include(x => x.Agent)
            .FirstOrDefault(x => x.Id == id);

        public IEnumerable<IndexViewModel> GetLastThree()
            => this.data.Apartments
            .OrderByDescending(x => x.Id)
            .ProjectTo<IndexViewModel>(this.mapper.ConfigurationProvider)
            .Take(3).ToList();

    }
}
