using System.Text.RegularExpressions;

namespace Rental.BLL.Validators
{
    public class MotorcycleValidator
    {
        public static void ValidateManufactureYear(int manufactureYear)
        {
            if(manufactureYear < 1900 || manufactureYear > DateTime.Today.Year)
            {
                throw new Exception($"Please, enter a valid manufacture year between 1900 and {DateTime.Today.Year}");
            }
        }

        public static void ValidatePlate(string plate)
        {
            var regex = new Regex(@"[A-Z]{3}[0-9][0-9A-Z][0-9]{2}");

            if (!regex.IsMatch(plate))
                throw new Exception("Please, enter a valid plate code");
        }
    }
}
