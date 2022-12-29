namespace ApartmentRentSystem.Tests.UnitTests
{
    using ApartmentRentSystem.Core.Contracts;
    using ApartmentRentSystem.Core.Services;
    using ApartmentRentSystem.Infrastructure.Data;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [TestFixture]
    public class CategoryServiceTests
    {
        private ApplicationDbContext applicationDbContext;
        private ICategoryService categoryService;
        private IMapper mapper;

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
        public void CategoryExistsReturnsTrue()
        {
            categoryService = new CategoryService(applicationDbContext, mapper);

            var expected = categoryService.CategoryExists(3);

            Assert.That(expected is true);
            Assert.IsNotNull(expected);
        }

        [Test]
        public void AllCategoriesNotNullAndCountIsMoreThanOne()
        {
            categoryService = new CategoryService(applicationDbContext, mapper);

            var result = categoryService.AllCategories();

            Assert.That(result is not null);
            Assert.That(result.Count() > 1);
        }

        [Test]
        public void AllCategoryNamesIsTypeOfStringAndIsNotNull()
        {
            categoryService = new CategoryService(applicationDbContext, mapper);

            var result = categoryService.AllCategoryNames();

            Assert.IsInstanceOf(typeof(ICollection<string>), result);
            Assert.That(result.Count() != 0);
        }

        [Test]
        public void GetApartmentCategoryId()
        {
            categoryService = new CategoryService(applicationDbContext, mapper);

            var result = categoryService.GetApartmentCategoryId(3);

            Assert.That(result != null);
            Assert.That(result >= 0);
        }
    }
}
