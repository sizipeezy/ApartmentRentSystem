namespace ApartmentRentSystem.Core.Models
{
    public class IndexViewModel : IApartmentModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }
    }
}
