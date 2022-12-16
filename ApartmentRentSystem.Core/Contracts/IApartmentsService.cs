namespace ApartmentRentSystem.Core.Contracts
{
    using ApartmentRentSystem.Core.Models;
    using System.Collections.Generic;


    public interface IApartmentsService
    {
        bool Exists(int id);
        ApartmentDetailsModel ApartmentDetailsById(int id);

        IEnumerable<IndexViewModel> GetLastThree();

        void AddAsync(AddApartmentModel model, int agentId);

        IEnumerable<ApartmentModel> AllApartmentsByUser(string userId);

        IEnumerable<ApartmentModel> AllApartmentsByAgent(int agentId);

        ApartmentQueryModel All(
            string? category = null,
            string? searchTerm = null,
            ApartmentSorting sorting = ApartmentSorting.Newest,
            int currentPage = 1,
            int housesPerPage = 1);
    }
}
