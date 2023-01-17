namespace ApartmentRentSystem.Extensions
{
    using ApartmentRentSystem.Core.Models;
    using AutoMapper;

    public class ControllerMappingProfile : Profile
    {
        public ControllerMappingProfile()
        {
            this.CreateMap<ApartmentDetailsModel, AddApartmentModel>();
        }
    }
}
