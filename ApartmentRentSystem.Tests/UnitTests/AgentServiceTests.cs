namespace ApartmentRentSystem.Tests.UnitTests
{
    using ApartmentRentSystem.Core.Contracts;
    using ApartmentRentSystem.Core.Services;
    using ApartmentRentSystem.Infrastructure.Data;
    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;

    [TestFixture]
    public class AgentServiceTests
    {
        private IAgentService agentService;
        private ApplicationDbContext applicationDbContext;


        [SetUp]
        public void Setup()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("ApartmentRentSystem")
            .Options;

            applicationDbContext = new ApplicationDbContext(contextOptions);

            applicationDbContext.Database.EnsureDeleted();
            applicationDbContext.Database.EnsureCreated();
        }

        [Test]
        public void ExistsById_ShouldSucceed()
        {
            //arange:
            agentService = new AgentService(applicationDbContext);
            //act:

            var resultAgentId = this.agentService.ExistsById("dea12856-c198-4129-b3f3-b893d8395082");

            //assert:
            Assert.That(resultAgentId.Equals(true));
        }

        [Test]
        public void AgentWithPhoneNumberExists()
        {
            agentService = new AgentService(applicationDbContext);

            var result = this.agentService.UserWithPhoneNumber("+359888888888");

            Assert.IsTrue(result);
        }

        [Test]
        public  void CreateShouldSucceed()
        {
            agentService = new AgentService(applicationDbContext);

            var result =  this.agentService.Create("testUser", "+359888888898");

            Assert.That(result.IsCompletedSuccessfully);
        }

        [Test]
        public void UserHasRents()
        {
            agentService = new AgentService(applicationDbContext);

            var result = this.agentService.UserHasRents("testUser");

            Assert.IsFalse(result);
        }

        [Test]
        public void HasAgentWithId()
        {
            agentService = new AgentService(applicationDbContext);

            var result = this.agentService.HasAgentWithId(1, "dea12856-c198-4129-b3f3-b893d8395082");

            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void GetAgentId()
        {
            agentService = new AgentService(applicationDbContext);

            var result = this.agentService.GetAgentId("dea12856-c198-4129-b3f3-b893d8395082");

            Assert.IsNotNull(result);   
        }
    }
}
