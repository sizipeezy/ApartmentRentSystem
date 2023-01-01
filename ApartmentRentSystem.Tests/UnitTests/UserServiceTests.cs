namespace ApartmentRentSystem.Tests.UnitTests
{
    using ApartmentRentSystem.Core.Contracts;
    using ApartmentRentSystem.Core.Services;
    using ApartmentRentSystem.Infrastructure.Data;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;


    [TestFixture]
    public class UserServiceTests
    {
        private ApplicationDbContext data;
        private IUserService userService;
        private IMapper mapper;

        [SetUp]
        public void Setup()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
           .UseInMemoryDatabase("ApartmentRentSystem")
            .Options;

            data = new ApplicationDbContext(contextOptions);

            data.Database.EnsureDeleted();
            data.Database.EnsureCreated();

            this.mapper = MapperMock.Instance;
        }

        [Test]
        public void GetUserNameWithNoUserName()
        {
            userService = new UserService(data, mapper);

            var fakeUser = userService.GetUserName("dea12856-c198-4129-b3f3-b893d8395082");

            Assert.IsTrue(fakeUser == null);
        }

        [Test]
        public void GetUsersShouldSucceed()
        {
            userService = new UserService(data, mapper);

            var fakeUser = userService.GetUsers();

            Assert.IsTrue(fakeUser != null);
            Assert.That(fakeUser.Any());
        }
    }
}
