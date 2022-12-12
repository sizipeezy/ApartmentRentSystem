namespace ApartmentRentSystem.Infrastructure.Data
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Agent
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(Constants.Agent.PhoneNumberMinLength)]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        public string UserId { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; } = null!;
    }
}
