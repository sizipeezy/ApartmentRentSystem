namespace ApartmentRentSystem.Core.Contracts
{
    using ApartmentRentSystem.Core.Models.Categories;

    public interface ICategoryService
    {
        bool CategoryExists(int categoryId);

        IEnumerable<CategoriesViewModel> AllCategories();

        IEnumerable<string> AllCategoryNames();

        int GetApartmentCategoryId(int apartmentId);
    }
}
