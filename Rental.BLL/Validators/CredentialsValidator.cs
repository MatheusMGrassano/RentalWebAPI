using System.Text.RegularExpressions;

namespace Rental.BLL.Validators
{
    public class CredentialsValidator
    {
        public static void ValidateEmail(string email)
        {
            var regex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");

            if (!regex.IsMatch(email))
                throw new Exception("Please, enter a valid email adress");
        }
        public static void ValidatePassword(string password) 
        {
            var regex = new Regex(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");

            if (!regex.IsMatch(password))
                throw new Exception("The password must cointain at least 8 characters, 1 uppercase, 1 lowercase, 1 number and 1 especial character");
        }
    }
}
