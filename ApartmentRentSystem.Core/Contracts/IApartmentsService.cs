namespace ApartmentRentSystem.Core.Contracts
{
    using ApartmentRentSystem.Core.Models;
    using System.Collections.Generic;


    public interface IApartmentsService
    {
        IEnumerable<IndexViewModel> GetLastThree();

        void AddAsync(AddApartmentModel model, int agentId);

        ApartmentQueryModel All(
            string? category = null,
            string? searchTerm = null,
            ApartmentSorting sorting = ApartmentSorting.Newest,
            int currentPage = 1,
            int housesPerPage = 1);
    }
}
