namespace ApartmentRentSystem.Infrastructure.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;


    public class ApartmentConfiguration : IEntityTypeConfiguration<Apartment>
    {
        public void Configure(EntityTypeBuilder<Apartment> builder)
        {
            builder.HasData(SeedApartments());
        }

        private List<Apartment> SeedApartments()
        {
            var apartments = new List<Apartment>()
            {
               new Apartment()
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
                },

                  new Apartment()
                  {
                      Id = 2,
                      Title = "Studio",
                      Address = "Near the Sea Garden in Burgas, Bulgaria",
                      Description = "It has the best comfort you will ever ask for. With two bedrooms, it is great for your family.",
                      ImageUrl = "https://cf.bstatic.com/xdata/images/hotel/max1280x900/313809284.jpg?k=1edac0f04691400c2ef3a873ed0b407196687de81548182c064f8f8b41ba9b5c&o=&hp=1",
                      PricePerMonth = 1200.00M,
                      CategoryId = 1,
                      AgentId = 1
                  },

                      new Apartment()
                  {
                      Id = 3,
                      Title = "Large",
                      Address = "Boyana Neighbourhood, Sofia, Bulgaria",
                      Description = "This luxurious house is everything you will need. It is just excellent.",
                      ImageUrl = "https://cf.bstatic.com/xdata/images/hotel/max1280x900/78568444.jpg?k=31c0bb6d3cde2f60971ea9a3200824a8143a0e5b2358cd484c94aeb772287cbd&o=&hp=1",
                      PricePerMonth = 2000.00M,
                      CategoryId = 5,
                      AgentId = 1
                  },

               };

            return apartments;
        }
    }
}
