namespace Rental.DAL.DTOs.RentDTOs
{
    public class RentAddDTO
    {
        public string DriverEmail { get; set; }
        public string DriverPassword { get; set; }
        public int RentDays { get; set; }
    }
}
