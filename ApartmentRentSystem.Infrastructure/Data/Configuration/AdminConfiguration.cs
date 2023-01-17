namespace ApartmentRentSystem.Infrastructure.Data.Configuration
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public static class AdminConfiguration
    {
        public static async void SeedAdmin(IApplicationBuilder builder)
        {
            using (var service = builder.ApplicationServices.CreateScope())
            {
                var roleManager = service.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if(!await roleManager.RoleExistsAsync("Admin"))
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                }

                var userManager = service.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                string adminEmail = "admin@gmail.com";

                var adminUser = await userManager.FindByNameAsync(adminEmail);

                if(adminUser == null)
                {
                    var admin = new ApplicationUser()
                    {
                        UserName = "admin-user",
                        Email = adminEmail,
                        FirstName = "admin1",
                        LastName = "test",
                        EmailConfirmed = true
                    };

                    await userManager.CreateAsync(admin, "admin-pass");
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }
        }
    }
}
