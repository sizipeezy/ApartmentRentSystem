namespace ApartmentRentSystem.Infrastructure.Data
{
    using ApartmentRentSystem.Infrastructure.Data.Configuration;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Apartment> Apartments { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Agent> Agents { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Item> Items { get; set; }

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

            builder.Entity<OrderItem>()
                .HasKey(x => new { x.OrderId, x.ApartmentId });


           builder.ApplyConfiguration(new UserConfiguration());
           builder.ApplyConfiguration(new AgentConfiguration());
           builder.ApplyConfiguration(new CategoryConfiguration());
           builder.ApplyConfiguration(new ApartmentConfiguration());

            base.OnModelCreating(builder);
        }
    }
}