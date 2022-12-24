namespace ApartmentRentSystem.Infrastructure.Data
{
    using System.ComponentModel.DataAnnotations;
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string ApplicationUserId { get; set; } = null!;

        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
