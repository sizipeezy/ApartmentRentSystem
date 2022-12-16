namespace ApartmentRentSystem.Core.Models
{
    using ApartmentRentSystem.Core.Models.Agents;


    public class ApartmentDetailsModel : ApartmentModel
    {
        public string Description { get; set; }

        public string  Category { get; set; }

        public AgentModel Agent { get; set; }
    }
}
