namespace ApartmentRentSystem.Core.Services
{
    using ApartmentRentSystem.Core.Contracts;
    using ApartmentRentSystem.Core.Models.Categories;
    using ApartmentRentSystem.Infrastructure.Data;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext data;
        private readonly IMapper mapper;

        public CategoryService(ApplicationDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public bool CategoryExists(int categoryId) =>
            this.data.Categories.Any(x => x.Id == categoryId);

        public IEnumerable<CategoriesViewModel> AllCategories() =>
              this.data.Categories
            .ProjectTo<CategoriesViewModel>(this.mapper.ConfigurationProvider)
            .ToList();

        public IEnumerable<string> AllCategoryNames() =>
            data.Categories
            .Select(c => c.Name)
            .Distinct()
            .ToList();

        public int GetApartmentCategoryId(int apartmentId) =>
            this.data.Apartments.Find(apartmentId).CategoryId;
     
    }
}
