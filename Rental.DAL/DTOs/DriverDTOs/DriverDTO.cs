namespace Rental.DAL.DTOs.DriverDTOs
{
    public class DriverDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Cnpj { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Licence { get; set; }
        public List<string> LicenceType { get; set; }
    }
}
