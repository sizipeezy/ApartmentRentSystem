namespace ApartmentRentSystem.Tests
{
    using ApartmentRentSystem.Infrastructure.Data;
    using Microsoft.EntityFrameworkCore;
    using System;

    public class DatabaseMock
    {
        public static ApplicationDbContext Instance
        {
            get
            {
                var dbcontextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase("ApartmentRentSystem " + DateTime.Now.Ticks.ToString())
                    .Options;

                return new ApplicationDbContext(dbcontextOptions);
            }
        }
    }
}
