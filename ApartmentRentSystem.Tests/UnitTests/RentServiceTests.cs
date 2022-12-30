namespace ApartmentRentSystem.Tests.UnitTests
{
    using ApartmentRentSystem.Core.Contracts;
    using ApartmentRentSystem.Core.Services;
    using ApartmentRentSystem.Infrastructure.Data;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using NUnit.Framework;

    public class RentServiceTests
    {
        private ApplicationDbContext applicationDbContext;
        private IRentService rentService;
        private IMapper mapper;
        private Mock mock;

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
        public void AllShouldSucceed()
        {
            rentService = new RentService(applicationDbContext, mapper);

            var expected = rentService.All();

            Assert.That(expected is not null);
            Assert.IsTrue(expected.Any());
        }

        [Test]
        public void Rent()
        {
            rentService = new RentService(applicationDbContext, mapper);

            rentService.Rent(1, "dea12856-c198-4129-b3f3-b893d8395082");

            Assert.That(rentService.All() != null);

        }

        [Test]
        public void Leave()
        {
            rentService = new RentService(applicationDbContext, mapper);

            rentService.Leave(1);

            var expected =
                applicationDbContext.Apartments.Find(1)
                .RenterId == null;

            Assert.That(expected, Is.True);
        }

        [Test]
        public void IsRented()
        {
            rentService = new RentService(applicationDbContext, mapper);

            rentService.IsRented(1);

            var expected =
                applicationDbContext.Apartments.Find(1)
                .RenterId != null;

            Assert.That(expected, Is.True);
        }

        [Test]
        public void ByUserId()
        {
            rentService = new RentService(applicationDbContext, mapper);

            var result = rentService.ByUserId(1, "dea12856-c198-4129-b3f3-b893d8395082");

            Assert.IsFalse(result);
        }
    }
}
