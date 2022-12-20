namespace ApartmentRentSystem.Core.Infrastructure
{
    using ApartmentRentSystem.Core.Models;
    using ApartmentRentSystem.Infrastructure.Data;
    using AutoMapper;


    public class ServiceMappingProfile : Profile
    {
        public ServiceMappingProfile()
        {
            this.CreateMap<Apartment, ApartmentModel>()
                .ForMember(x => x.IsRented, config => config.MapFrom(x => x.RenterId != null));

            this.CreateMap<Apartment, IndexViewModel>();
        }
    }
}
