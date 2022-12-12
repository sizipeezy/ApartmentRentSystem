namespace ApartmentRentSystem.Infrastructure.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;


    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(CreateCategories());

        }
        private List<Category> CreateCategories()
        {
            List<Category> categories = new List<Category>()
            {
                new Category()
                {
                    Id = 1,
                    Name = "Single-Room"
                },

                new Category()
                {
                    Id = 2,
                    Name = "Double"
                },

                new Category()
                {
                    Id = 3,
                    Name = "Superior"
                },

                new Category()
                {
                 Id = 4,
                 Name = "Studio"
                },

                new Category()
                {
                    Id = 5,
                    Name = "Deluxe"
                }


             };

            return categories;
        }
    }
}
