namespace ApartmentRentSystem.Core.Models
{
    using System.ComponentModel;


    public class ApartmentModel : IApartmentModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Address { get; set; }

        [DisplayName("Image Url")]
        public string ImageUrl { get; set; }


        [DisplayName("Price Per Month")]
        public decimal PricePerMonth { get; set; }


        [DisplayName("Is Rented")]
        public bool IsRented { get; set; }
    }
}
