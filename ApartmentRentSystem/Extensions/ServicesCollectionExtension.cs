namespace ApartmentRentSystem.Extensions
{
    using ApartmentRentSystem.Core.Contracts;
    using ApartmentRentSystem.Core.Services;
    using ApartmentRentSystem.Infrastructure.Data;

    public static class ServicesCollectionExtension
    {
        public static IServiceCollection AddAplicationServices(this IServiceCollection services)
        {

            services.AddScoped<IApartmentsService, ApartmentsService>();
            services.AddScoped<IAgentService, AgentService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IRentService, RentService>();
            services.AddScoped<IStatisticsService, StatisticsService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IOrderService, OrderService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(s => ShoppingCart.GetShoppingCart(s));
            services.AddSession();

            return services;
        }
    }
}
