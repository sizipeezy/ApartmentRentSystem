namespace ApartmentRentSystem.Core.Models
{
    using System.Collections.Generic;


    public class AllApartmentsQueryModel
    {
        public const int ApartmentsPerPage = 3;

        public string Category { get; set; }

        public string SearchTerm { get; set; }

        public ApartmentSorting ApartmentSorting { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int TotalApartmentsCount { get; set; }

        public IEnumerable<string> Categories { get; set; }

        public IEnumerable<ApartmentModel> Apartments { get; set; } = new List<ApartmentModel>();
    }
}
