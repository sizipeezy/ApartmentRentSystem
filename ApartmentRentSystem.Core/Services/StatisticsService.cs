namespace ApartmentRentSystem.Core.Services
{
    using ApartmentRentSystem.Core.Contracts;
    using ApartmentRentSystem.Core.Models.Statistics;
    using ApartmentRentSystem.Infrastructure.Data;
    using Microsoft.EntityFrameworkCore;

    public class StatisticsService : IStatisticsService
    {
        private readonly ApplicationDbContext data;

        public StatisticsService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public async Task<StatisticsServiceModel> Total()
        {
            int totalHouses = await data.Apartments
                .CountAsync(h => h.IsActive);
            int rentedHouses = await data.Apartments
                .CountAsync(h => h.IsActive && h.RenterId != null);

            return new StatisticsServiceModel()
            {
                TotalApartments = totalHouses,
                TotalRents = rentedHouses
            };
        }
    }
}
