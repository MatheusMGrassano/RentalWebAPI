namespace Rental.DAL.DTOs.MotorcycleDTOs
{
    public class MotorcycleDTO
    {
        public int Id { get; set; }
        public int ManufactureYear { get; set; }
        public string Model { get; set; }
        public string Plate { get; set; }
        public bool Available { get; set; }
    }
}
