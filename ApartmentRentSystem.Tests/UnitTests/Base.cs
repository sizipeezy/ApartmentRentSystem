using ApartmentRentSystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentRentSystem.Tests.UnitTests
{
    public class Base
    {
        protected ApplicationDbContext data;

        [OneTimeSetUp]

        public void SetUpBase()
        {
            this.data = DatabaseMock.Instance;
            this.SeedDatabase();
        }

        public ApplicationUser Renter { get; set; } = null!;

        public Agent Agent { get; set; }

        public Apartment RentedApartment { get; set; }

        private void SeedDatabase()
        {
            this.Renter = new ApplicationUser()
            {
                Id= "RenterUserId",
                Email = "rent@er.bg",
                FirstName = "Renter",
                LastName = "User"
            };

            this.data.Users.Add(this.Renter);

            var agent = new Agent()
            {
                PhoneNumber = "08985748212",
                User = new ApplicationUser()
                {
                    Id = "TestUserId",
                    Email = "TestUser@test.bg",
                    FirstName = "Test",
                    LastName = "Testov"
                }
            };

            this.data.Agents.Add(this.Agent);
        }


    }
}
