namespace ApartmentRentSystem.Core.Services
{
    using ApartmentRentSystem.Core.Contracts;
    using ApartmentRentSystem.Core.Models.Categories;
    using ApartmentRentSystem.Infrastructure.Data;


    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext data;

        public CategoryService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public bool CategoryExists(int categoryId) =>
            this.data.Categories.Any(x => x.Id == categoryId);

        public IEnumerable<CategoriesViewModel> AllCategories() =>
              this.data.Categories.Select(x => new CategoriesViewModel
              {
                  Id = x.Id,
                  Name = x.Name
              }).ToList();

        public IEnumerable<string> AllCategoryNames() =>
            data.Categories
            .Select(c => c.Name)
            .Distinct()
            .ToList();

    }
}
