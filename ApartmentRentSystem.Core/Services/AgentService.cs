namespace ApartmentRentSystem.Core.Services
{
    using ApartmentRentSystem.Core.Contracts;
    using ApartmentRentSystem.Infrastructure.Data;
    using System.Threading.Tasks;

    public class AgentService : IAgentService
    {
        private readonly ApplicationDbContext data;

        public AgentService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public async Task Create(string userId, string phoneNumber)
        {
            var agent = new Agent()
            {
                PhoneNumber = phoneNumber,
                UserId = userId,
            };

            await this.data.Agents.AddAsync(agent);
            await this.data.SaveChangesAsync();
        }

        public bool ExistsById(string agentId) =>
        this.data.Agents.Any(x => x.UserId == agentId);
        

        public int GetAgentId(string userId) => 
            data.Agents.FirstOrDefault(x => x.UserId == userId).Id;
        

        public bool UserHasRents(string userId) =>
         this.data.Apartments.Any(x => x.RenterId == userId);
        

        public bool UserWithPhoneNumber(string phoneNumber) =>
         this.data.Agents.Any(x => x.PhoneNumber == phoneNumber);
        
    }
}
