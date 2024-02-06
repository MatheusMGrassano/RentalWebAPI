using Rental.DAL.Entities.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Rental.DAL.Entities
{
    public class Driver : IUser
    {
        [Key]
        public int Id { get; private set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Cnpj { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Licence { get; set; }
        public List<string> LicenceType { get; set; }
    }
}

