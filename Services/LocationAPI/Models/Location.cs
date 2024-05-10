using System.ComponentModel.DataAnnotations;

namespace LocationAPI.Models
{
    public class Location
    {
        public Guid Id { get; set; }
        [Required]
        public DateTime StartDate { get; set; } = DateTime.Now.AddDays(1);
        public DateTime EndDate { get; set; }
        [Required]
        public DateTime ExpectedEndDate { get; set; }
        public int PlanDays { get; set; }
        public Guid UserId { get; set; }
        public Guid MotorcycleId { get; set; }
        public decimal? TotalPrice { get; set; }
    }
}
