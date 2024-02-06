using System.ComponentModel.DataAnnotations;

namespace Rental.DAL.Entities
{
    public class Motorcycle
    {
        [Key]
        public int Id { get; private set; }
        public int ManufactureYear { get; set; }
        public string Model { get; set; }
        public string Plate { get; set;}
        public bool Available { get; set; }
    }
}