namespace ApartmentRentSystem.Core.Models.Agents
{
    using System.ComponentModel.DataAnnotations;


    public class AgentModel : BecomeAgentFormModel
    {
        [Required]
        public string Email { get; set; } = null!;
    }
}
