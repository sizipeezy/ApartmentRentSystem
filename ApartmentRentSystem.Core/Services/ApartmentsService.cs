namespace ApartmentRentSystem.Core.Services
{
    using ApartmentRentSystem.Core.Contracts;
    using ApartmentRentSystem.Core.Models;
    using ApartmentRentSystem.Infrastructure.Data;
    using System.Collections.Generic;

    public class ApartmentsService : IApartmentsService
    {
        private readonly ApplicationDbContext data;

        public ApartmentsService(ApplicationDbContext db)
        {
            this.data = db;
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
                .Select(x => new ApartmentModel()
                {
                    Id = x.Id,
                    Address = x.Address,
                    ImageUrl = x.ImageUrl,
                    IsRented = x.RenterId != null,
                    PricePerMonth = x.PricePerMonth,
                    Title = x.Title
                })
                .ToList();

            result.TotalApartments = apartmentsQuery.Count();

            return result;
        }

        public IEnumerable<IndexViewModel> GetLastThree()
            => this.data.Apartments.OrderByDescending(x => x.Id)
            .Select(x => new IndexViewModel
            {
                Id = x.Id,
                ImageUrl = x.ImageUrl,
                Title = x.Title
            }).Take(3).ToList();
    }
}
