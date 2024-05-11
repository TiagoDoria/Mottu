using System.ComponentModel.DataAnnotations;

namespace MottuWeb.Models
{
    public class LocationDTO
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime ExpectedEndDate { get; set; }
        public int PlanDays { get; set; }
        public Guid UserId { get; set; }
        public Guid MotorcycleId { get; set; }
        public decimal? TotalPrice { get; set; }
    }
}
