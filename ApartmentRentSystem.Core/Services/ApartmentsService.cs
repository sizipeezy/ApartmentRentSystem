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

        public async Task AddAsync(AddApartmentModel model)
        {
            var apartment = new Apartment()
            {
                ImageUrl = model.ImageUrl,
                Description = model.Description,
                PricePerMonth = model.PricePerMonth,
                CategoryId = model.CategoryId,
                Title = model.Title,
                Address = model.Address,
                AgentId = model.AgentId,    
            };

            await this.data.Apartments.AddAsync(apartment);
            await this.data.SaveChangesAsync();
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
