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
