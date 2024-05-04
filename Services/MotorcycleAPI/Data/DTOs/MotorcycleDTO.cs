using System.ComponentModel.DataAnnotations;

namespace MotorcycleAPI.Data.DTOs
{
    public class MotorcycleDTO
    {
        public Guid Id { get; set; }
        public int Year { get; set; }
        public string Model { get; set; }
        public string Plate { get; set; }
    }
}
