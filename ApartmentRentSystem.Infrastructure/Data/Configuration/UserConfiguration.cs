namespace ApartmentRentSystem.Infrastructure.Data.Configuration
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;


    public class UserConfiguration : IEntityTypeConfiguration<IdentityUser>
    {
        public void Configure(EntityTypeBuilder<IdentityUser> builder)
        {
            builder.HasData(SeedUsers());
        }

        private List<IdentityUser> SeedUsers()
        {
            var users = new List<IdentityUser>();
            var hasher = new PasswordHasher<IdentityUser>();

            var user = new IdentityUser()
            {
                Id = "dea12856-c198-4129-b3f3-b893d8395082",
                UserName = "agent@mail.com",
                NormalizedUserName = "agent@mail.com",
                Email = "agent@mail.com",
                NormalizedEmail = "agent@mail.com"
            };

            user.PasswordHash =
                 hasher.HashPassword(user, "agent123");

            users.Add(user);

            var user2 = new IdentityUser()
            {
                Id = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                UserName = "guest@mail.com",
                NormalizedUserName = "guest@mail.com",
                Email = "guest@mail.com",
                NormalizedEmail = "guest@mail.com"
            };

            user2.PasswordHash =
            hasher.HashPassword(user2, "guest123");

            users.Add(user2);

            return users;   
        }
    }
}
