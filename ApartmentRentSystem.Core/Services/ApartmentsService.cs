namespace ApartmentRentSystem.Core.Services
{
    using ApartmentRentSystem.Core.Contracts;
    using ApartmentRentSystem.Core.Models;
    using ApartmentRentSystem.Core.Models.Categories;
    using ApartmentRentSystem.Infrastructure.Data;
    using System.Collections.Generic;
    using System.Threading.Tasks;

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
