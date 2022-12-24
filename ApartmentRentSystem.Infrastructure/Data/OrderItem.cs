namespace ApartmentRentSystem.Infrastructure.Data
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    public class OrderItem
    {
        [Required]
        public int Amount { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }

        public int OrderId { get; set; }

        [ForeignKey(nameof(OrderId))]
        public Order Order { get; set; } = null!;

        public int ApartmentId { get; set; }

        [ForeignKey(nameof(ApartmentId))]
        public Apartment Apartment { get; set; } = null!;
    }
}
