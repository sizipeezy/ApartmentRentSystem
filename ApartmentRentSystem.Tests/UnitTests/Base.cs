namespace ApartmentRentSystem.Tests.UnitTests
{
    using ApartmentRentSystem.Infrastructure.Data;
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

            this.RentedApartment = new Apartment()
            {
                Title = "Test Title",
                Address = "Bukovo, test Gotse Delchev",
                Description = "Test description for hte apartment system v1.0, modofied to be the best web app",
                ImageUrl = "https://cf.bstatic.com/xdata/images/hotel/max1280x900/383031658.jpg?k=ce7cad10cc9051d1f5359339857801ebe1031a8f7b8118954311ad335ef47acd&o=&hp=1",
                Renter = this.Renter,
                Agent = this.Agent,
                Category = new Category { Name = "Old School" },

            };

            this.data.Apartments.Add(RentedApartment);

            var nonRented = new Apartment()
            {
                Title = "Test Title2",
                Address = "Bukovo, test Gotse Delchev23",
                Description = "Test description for hte apartment system v1.0, modofied to be the best web app",
                ImageUrl = "https://cf.bstatic.com/xdata/images/hotel/max1280x900/383031658.jpg?k=ce7cad10cc9051d1f5359339857801ebe1031a8f7b8118954311ad335ef47acd&o=&hp=1",
                Renter = this.Renter,
                Agent = this.Agent,
                Category = new Category { Name = "Single" },
            };


            this.data.Apartments.Add(nonRented);
            this.data.SaveChanges();
        }

        [OneTimeTearDown]
        public void TearDownBase()
            => this.data.Dispose();

    }
}
