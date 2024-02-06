namespace Rental.DAL.DTOs.RentDTOs
{
    public class RentEndDateDTO
    {
        public string DriverEmail { get; set; }
        public string DriverPassword { get; set; }
        public string Plate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
