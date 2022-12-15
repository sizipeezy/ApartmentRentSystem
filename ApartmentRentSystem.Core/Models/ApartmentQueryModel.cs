namespace ApartmentRentSystem.Core.Models
{
    public class ApartmentQueryModel
    {
        public int TotalApartments { get; set; }

        public IEnumerable<ApartmentModel> AllApartments { get; set; } = new List<ApartmentModel>();
    }
}
