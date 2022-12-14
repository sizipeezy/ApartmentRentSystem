namespace ApartmentRentSystem.Extensions
{
    using ApartmentRentSystem.Core.Contracts;
    using ApartmentRentSystem.Core.Services;

    public static class ServicesCollectionExtension
    {
        public static IServiceCollection AddAplicationServices(this IServiceCollection services)
        {

            services.AddScoped<IApartmentsService, ApartmentsService>();
            services.AddScoped<IAgentService, AgentService>();
            services.AddScoped<ICategoryService, CategoryService>();

            return services;
        }
    }
}
