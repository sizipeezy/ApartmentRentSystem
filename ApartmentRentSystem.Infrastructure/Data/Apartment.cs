namespace ApartmentRentSystem.Infrastructure.Data
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    public class Apartment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(Constants.Apartment.TitleMaxLength, MinimumLength = Constants.Apartment.TitleMinLength)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(Constants.Apartment.AddressMaxLength, MinimumLength = Constants.Apartment.AddressMinLength)]
        public string Address { get; set; } = null!;

        [Required]
        [StringLength(Constants.Apartment.DescriptionMax, MinimumLength = Constants.Apartment.DescriptionLow)]
        public string Description { get; set; } = null!;

        [Required]
        [StringLength(200)]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [Column(TypeName = "money")]
        [Precision(18,2)]
        public decimal PricePerMonth { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;

        [Required]
        public int AgentId { get; set; }

        [ForeignKey(nameof(AgentId))]
        public Agent Agent { get; set; } = null!;

        public string RenterId { get; set; }
    }
}
