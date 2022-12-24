namespace ApartmentRentSystem.Infrastructure.Data
{
    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    public class Item
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public int Quantity { get; set; }

        public Apartment Apartment { get; set; } = null!;

        public string ShoppingCartId { get; set; } = null!;
    }
}
