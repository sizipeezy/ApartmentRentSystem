namespace ApartmentRentSystem.Core.Contracts
{
    using ApartmentRentSystem.Core.Models;
    using System.Collections.Generic;


    public interface IApartmentsService
    {
        IEnumerable<IndexViewModel> GetLastThree();

        void AddAsync(AddApartmentModel model, int agentId);
    }
}
