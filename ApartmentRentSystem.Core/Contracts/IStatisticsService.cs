using ApartmentRentSystem.Core.Models.Statistics;

namespace ApartmentRentSystem.Core.Contracts
{
    public interface IStatisticsService
    {
        Task<StatisticsServiceModel> Total();
    }
}
