namespace ApartmentRentSystem.Tests.UnitTests
{
    using ApartmentRentSystem.Core.Infrastructure;
    using AutoMapper;


    public static class MapperMock
    {
        public static IMapper Instance
        {
            get
            {
                var mapperConfiguration = new MapperConfiguration(config =>
                {
                    config.AddProfile<ServiceMappingProfile>();
                });

                return new Mapper(mapperConfiguration);
            }
        }
    }
}
