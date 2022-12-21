namespace ApartmentRentSystem.Core.Services
{
    using ApartmentRentSystem.Core.Contracts;
    using ApartmentRentSystem.Core.Models.Users;
    using ApartmentRentSystem.Infrastructure.Data;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;

    public class UserService : IUserService
    {

        private readonly ApplicationDbContext data;
        private readonly IMapper mapper;

        public UserService(ApplicationDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
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
            var result = new List<UserModel>();

           var allUsers =  
                this.data
                .Agents
                .Include(x => x.User)
                .ProjectTo<UserModel>(this.mapper.ConfigurationProvider)
                .ToList();

            result.AddRange(allUsers);

            var allAgents = 
                this.data
                .Users
                .Where(z => !this.data.Agents.Any(x => x.UserId == z.Id))
                .ProjectTo<UserModel>(this.mapper.ConfigurationProvider)
                .ToList();

            result.AddRange(allAgents);

            return result;
        }
    }
}
