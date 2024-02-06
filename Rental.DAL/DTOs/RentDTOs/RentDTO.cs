namespace Rental.DAL.DTOs.RentDTOs
{
    public class RentDTO
    {
        public string DriverName { get; set; }
        public string MotorcyclePlate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? TotalPrice { get; set; }
    }
}
