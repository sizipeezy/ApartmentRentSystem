namespace ApartmentRentSystem.Infrastructure.Data
{
    using ApartmentRentSystem.Infrastructure.Data.Configuration;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Apartment> Apartments { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Agent> Agents { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Apartment>()
                .HasOne(c => c.Category)
                .WithMany(x => x.Apartments)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Apartment>()
                .HasOne(x => x.Agent)
                .WithMany()
                .HasForeignKey(x => x.AgentId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new AgentConfiguration());

            base.OnModelCreating(builder);
        }
    }
}