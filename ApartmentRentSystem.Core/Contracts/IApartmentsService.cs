namespace ApartmentRentSystem.Core.Contracts
{
    using ApartmentRentSystem.Core.Models;
    using System.Collections.Generic;


    public interface IApartmentsService
    {
        IEnumerable<IndexViewModel> GetLastThree();

        Task AddAsync(AddApartmentModel model);
    }
}
