using Rental.DAL.Entities.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Rental.DAL.Entities
{
    public class Admin : IUser
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
