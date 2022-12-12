namespace ApartmentRentSystem.Infrastructure.Data
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;


    public class Category
    {
        public Category()
        {
            Apartments = new List<Apartment>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Name { get; set; }

        public List<Apartment> Apartments { get; set; }
    }
}
