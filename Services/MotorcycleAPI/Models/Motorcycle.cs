using System.ComponentModel.DataAnnotations;
namespace MotorcycleAPI.Models
{
    public class Motorcycle
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public string Model {  get; set; }
        [Required]
        public string Plate { get; set; }
    }
}
