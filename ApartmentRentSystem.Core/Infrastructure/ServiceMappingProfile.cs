namespace ApartmentRentSystem.Core.Infrastructure
{
    using ApartmentRentSystem.Core.Models;
    using ApartmentRentSystem.Core.Models.Agents;
    using ApartmentRentSystem.Core.Models.Categories;
    using ApartmentRentSystem.Infrastructure.Data;
    using AutoMapper;


    public class ServiceMappingProfile : Profile
    {
        public ServiceMappingProfile()
        {
            this.CreateMap<Apartment, ApartmentModel>()
                .ForMember(x => x.IsRented, config => config.MapFrom(x => x.RenterId != null));

            this.CreateMap<Apartment, IndexViewModel>();

            this.CreateMap<Apartment, ApartmentDetailsModel>()
                .ForMember(x => x.IsRented, config => config.MapFrom(x => x.RenterId != null))
                .ForMember(x => x.Category, config => config.MapFrom(x => x.Category.Name));

            this.CreateMap<Agent, AgentModel>()
                .ForMember(x => x.Email, config => config.MapFrom(x => x.User.Email));

            this.CreateMap<Category, CategoriesViewModel>();
        }
    }
}
