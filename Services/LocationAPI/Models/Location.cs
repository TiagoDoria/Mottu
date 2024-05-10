using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocationAPI.Models
{
    public class Location
    {
        public Guid Id { get; set; }
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime StartDate { get; set; } = DateTime.Now.AddDays(1);
        [Column(TypeName = "datetime")]
        public DateTime? EndDate { get; set; }
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime ExpectedEndDate { get; set; }
        [Required]
        public int PlanDays { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid MotorcycleId { get; set; }
        public decimal? TotalPrice { get; set; }
    }
}
