namespace ApartmentRentSystem.Core.Contracts
{
    public interface IAgentService
    {
        bool ExistsById(string agentId);    

        bool UserWithPhoneNumber(string phoneNumber);

        bool UserHasRents(string userId);

        Task Create(string userId, string phoneNumber);
    }
}
