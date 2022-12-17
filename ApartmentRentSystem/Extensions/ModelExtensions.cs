namespace ApartmentRentSystem.Extensions
{
    using ApartmentRentSystem.Core.Models;
    using System.Text.RegularExpressions;


    public static class ModelExtensions
    {
        public static string GetInformation(this IApartmentModel model) =>
            model.Title.Replace(" ", "-") + "-" + GetAddress(model.Address);

        private static string GetAddress(string address)
        {
            address = string.Join("-", address.Split(' ').Take(3));
            return Regex.Replace(address, @"[^a-zA-Z0-9\-]", string.Empty);
        }
    }
}
