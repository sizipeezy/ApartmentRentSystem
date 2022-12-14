namespace ApartmentRentSystem.Core.Models.Agents
{
    using System.ComponentModel.DataAnnotations;


    public class BecomeAgentFormModel
    {
        [Required]
        [StringLength(100, MinimumLength =15)]
        [Display(Name ="Phone Number")]
        [Phone]
        public string PhoneNumber { get; set; }
    }
}
