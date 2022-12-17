namespace ApartmentRentSystem.Extensions
{
    using ApartmentRentSystem.Core.Models;
    using System.Text;
    using System.Text.RegularExpressions;


    public static class ModelExtensions
    {
        public static string GetInformation(this IApartmentModel apartment)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(apartment.Title.Replace(" ", "-"));
            sb.Append("-");
            sb.Append(GetAddress(apartment.Address));

            return sb.ToString();
        }

        private static string GetAddress(string address)
        {
            string result = string
                .Join("-", address.Split(" ", StringSplitOptions.RemoveEmptyEntries).Take(3));

            return Regex.Replace(address, @"[^a-zA-Z0-9\-]", string.Empty);
        }
    }
}
