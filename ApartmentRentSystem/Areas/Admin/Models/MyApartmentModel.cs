namespace ApartmentRentSystem.Areas.Admin.Models
{
    using ApartmentRentSystem.Core.Models;

    public class MyApartmentModel
    {
        public IEnumerable<ApartmentModel> AddedApartments { get; set; } =
            new List<ApartmentModel>();

        public IEnumerable<ApartmentModel> RentedApartments { get; set; } =
            new List<ApartmentModel>();
    }
}
