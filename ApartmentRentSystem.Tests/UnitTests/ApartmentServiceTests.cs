namespace ApartmentRentSystem.Tests.UnitTests
{
    using ApartmentRentSystem.Core.Contracts;
    using ApartmentRentSystem.Core.Models;
    using ApartmentRentSystem.Core.Services;
    using ApartmentRentSystem.Infrastructure.Data;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;


    [TestFixture]
    public class ApartmentServiceTests
    {
        private IApartmentsService apartmentsService;
        private ApplicationDbContext applicationDbContext;
        private IUserService userService;
        protected IMapper mapper;

        [SetUp]
        public void SetUp()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("ApartmentRentSystem")
            .Options;

            applicationDbContext = new ApplicationDbContext(contextOptions);

            applicationDbContext.Database.EnsureDeleted();
            applicationDbContext.Database.EnsureCreated();

            this.mapper = MapperMock.Instance;
        }

        [Test]
        public void AddAsync()
        {
            apartmentsService = new ApartmentsService(applicationDbContext, userService, mapper);

            var testModel = new AddApartmentModel()
            {
                Id = 1,
                Title = "Double Deluxe Room",
                Address = "North London, UK (near the border)",
                Description = "A big house for your whole family. Don't miss to buy a house with three bedrooms.",
                ImageUrl = "https://cf.bstatic.com/xdata/images/hotel/max1280x900/327594640.jpg?k=7a05be91474955db29a28df2cdd1b06cc39581596e6418f4809fa6261c5eade5&o=&hp=1",
                PricePerMonth = 2100.00M,
                CategoryId = 3,
            };

           apartmentsService.AddAsync(testModel, 1);

            Assert.That(applicationDbContext.Apartments.Count() > 1);

        }

        [Test]
        public void AllApartmentsByAgent()
        {
            apartmentsService = new ApartmentsService(applicationDbContext, userService, mapper);

            var apartment = new Apartment()
            {
                Id = 1,
                Title = "Double Deluxe Room",
                Address = "North London, UK (near the border)",
                Description = "A big house for your whole family. Don't miss to buy a house with three bedrooms.",
                ImageUrl = "https://cf.bstatic.com/xdata/images/hotel/max1280x900/327594640.jpg?k=7a05be91474955db29a28df2cdd1b06cc39581596e6418f4809fa6261c5eade5&o=&hp=1",
                PricePerMonth = 2100.00M,
                CategoryId = 3,
                AgentId = 1,
                RenterId = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e"
            };

            this.applicationDbContext.Apartments.Add(apartment);

            var result =  apartmentsService.AllApartmentsByAgent(1);

            Assert.That(result != null);
            Assert.That(result.Count(), Is.EqualTo(3)); 
        }

        [Test]
        public void AllApartmentsByUser()
        {
            apartmentsService = new ApartmentsService(applicationDbContext, userService, mapper);

            var result = apartmentsService.AllApartmentsByUser("dea12856-c198-4129-b3f3-b893d8395082");

            Assert.That(result != null);
            Assert.That(result.Any(), Is.EqualTo(false));
        }

        [Test]
        public void ApartmentDetailsById()
        {
            apartmentsService = new ApartmentsService(applicationDbContext, userService, mapper);

            var result = apartmentsService.ApartmentDetailsById(1);

            Assert.That(result is not null);
            Assert.IsInstanceOf(typeof(ApartmentDetailsModel), result);
        }

        [Test]
        public void Delete()
        {

            apartmentsService = new ApartmentsService(applicationDbContext, userService, mapper);

            apartmentsService.Delete(1);

            Assert.That(this.applicationDbContext.Apartments.Count() != 0);
            Assert.Pass();
        }

        [Test]
        public void Edit()
        {
            apartmentsService = new ApartmentsService(applicationDbContext, userService, mapper);

            var AddApartment = new AddApartmentModel()
            {

                Id = 1,
                Title = "Double Deluxe Room",
                Address = "North London, UK (near the border)",
                Description = "A big house for your whole family. Don't miss to buy a house with three bedrooms.",
                ImageUrl = "https://cf.bstatic.com/xdata/images/hotel/max1280x900/327594640.jpg?k=7a05be91474955db29a28df2cdd1b06cc39581596e6418f4809fa6261c5eade5&o=&hp=1",
                PricePerMonth = 2100.00M,
                CategoryId = 6,
            };

            apartmentsService.Edit(1, AddApartment);

            Assert.Pass();
        }

        [Test]
        public void Exists()
        {
            apartmentsService = new ApartmentsService(applicationDbContext, userService, mapper);

           var result =  apartmentsService.Exists(1);

            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void Get()
        {
            apartmentsService = new ApartmentsService(applicationDbContext, userService, mapper);

            var result = apartmentsService.Get(1);

            Assert.That(result is not null);
            Assert.IsInstanceOf(typeof(Apartment), result);
        }
    }
}
