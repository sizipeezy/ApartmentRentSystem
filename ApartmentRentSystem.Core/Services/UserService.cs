namespace ApartmentRentSystem.Core.Services
{
    using ApartmentRentSystem.Core.Contracts;
    using ApartmentRentSystem.Core.Models.Users;
    using ApartmentRentSystem.Infrastructure.Data;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;

    public class UserService : IUserService
    {

        private readonly ApplicationDbContext data;

        public UserService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public string GetUserName(string userId)
        {
            var user = this.data.Users.Find(userId);

            if(string.IsNullOrEmpty(user.FirstName) || string.IsNullOrEmpty(user.LastName))
            {
                return null;
            }

            return user.FirstName + " " + user.LastName;
        }

        public IEnumerable<UserModel> GetUsers()
        {
            throw new NotImplementedException();
        }
    }
}
