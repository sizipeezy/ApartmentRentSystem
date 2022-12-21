﻿namespace ApartmentRentSystem.Core.Infrastructure
{
    using ApartmentRentSystem.Core.Models;
    using ApartmentRentSystem.Core.Models.Agents;
    using ApartmentRentSystem.Core.Models.Categories;
    using ApartmentRentSystem.Core.Models.Users;
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

            this.CreateMap<Agent, UserModel>()
                .ForMember(x => x.Email, cfg => cfg.MapFrom(a => a.User.Email))
                .ForMember(c => c.FullName, cfg => cfg.MapFrom(a => a.User.FirstName + " " + a.User.LastName));

            this.CreateMap<ApplicationUser, UserModel>()
                .ForMember(x => x.PhoneNumber, cfg => cfg.MapFrom(a => string.Empty))
                .ForMember(x => x.FullName, cfg => cfg.MapFrom(a => a.FirstName + " " + a.LastName));
        }
    }
}
