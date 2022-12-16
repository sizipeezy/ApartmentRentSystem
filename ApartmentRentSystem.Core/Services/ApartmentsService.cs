namespace ApartmentRentSystem.Core.Services
{
    using ApartmentRentSystem.Core.Contracts;
    using ApartmentRentSystem.Core.Models;
    using ApartmentRentSystem.Core.Models.Agents;
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

        public IEnumerable<ApartmentModel> AllApartmentsByAgent(int agentId)
        {
            var apartments = this.data.Apartments.Where(c => c.IsActive)
                .Where(c => c.AgentId == agentId).ToList();

            return ProjectToApartment(apartments);
        }

        public IEnumerable<ApartmentModel> AllApartmentsByUser(string userId)
        {
            var apartments = this.data.Apartments.Where(x => x.RenterId == userId).ToList();

            return ProjectToApartment(apartments);
        }

        public ApartmentDetailsModel ApartmentDetailsById(int id)
        {
            var apartment = this.data.Apartments.Where(x => x.Id == id)
                .Select(x => new ApartmentDetailsModel
                {
                    Id = x.Id,
                    Address = x.Address,
                    Category = x.Category.Name,
                    Description = x.Description,
                    ImageUrl = x.ImageUrl,
                    Title = x.Title,
                    PricePerMonth = x.PricePerMonth,
                    Agent = new AgentModel
                    {
                        Email = x.Agent.User.Email,
                        PhoneNumber = x.Agent.PhoneNumber
                    }

                }).FirstOrDefault();

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

        public IEnumerable<IndexViewModel> GetLastThree()
            => this.data.Apartments.OrderByDescending(x => x.Id)
            .Select(x => new IndexViewModel
            {
                Id = x.Id,
                ImageUrl = x.ImageUrl,
                Title = x.Title
            }).Take(3).ToList();

        public List<ApartmentModel> ProjectToApartment(List<Apartment> apartments)
        {
            var result = apartments
                .Select(x => new ApartmentModel
                {
                    Id = x.Id,
                    Address = x.Address,
                    ImageUrl = x.ImageUrl,
                    IsRented = x.RenterId != null,
                    PricePerMonth = x.PricePerMonth,
                    Title = x.Title
                })
                .ToList();

            return result;
        }
    }
}
