namespace ApartmentRentSystem.Infrastructure
{
    public static class Constants
    {
        public class Agent
        {
            public const int PhoneNumberMinLength = 15;
        }
        public class Apartment
        {
            public const int TitleMaxLength = 50;
            public const int TitleMinLength = 10;

            public const int AddressMaxLength = 150;
            public const int AddressMinLength = 10;

            public const int DescriptionMax = 500;
            public const int DescriptionLow = 30;

            public const int PricePerMonthMax = 2000;
            public const int PricePerMonthLow = 0;
        }
    }
}
