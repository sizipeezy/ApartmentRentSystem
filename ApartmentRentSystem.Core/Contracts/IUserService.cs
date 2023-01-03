using ApartmentRentSystem.Core.Models.Users;

namespace ApartmentRentSystem.Core.Contracts
{
    public interface IUserService
    {
        string GetUserName(string userId);

        IEnumerable<UserModel> GetUsers();

        bool UserIdExists(string userId);
    }
}
