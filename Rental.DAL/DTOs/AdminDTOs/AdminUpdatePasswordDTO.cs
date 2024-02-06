using System.Runtime.CompilerServices;

namespace Rental.DAL.DTOs.AdminDTOs
{
    public class AdminUpdatePasswordDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
    }
}
