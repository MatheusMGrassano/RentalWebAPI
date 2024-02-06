using Rental.DAL.Entities.Records;
using System.ComponentModel.DataAnnotations;

namespace Rental.DAL.Entities
{
    public class Rent
    {
        [Key]
        public int Id { get; set; }
        public Driver RentDriver { get; set; }
        public Motorcycle RentMotorcycle { get; set; }
        public Plan RentPlan { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpectedEndDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? TotalPrice { get; set; }
    }
}
