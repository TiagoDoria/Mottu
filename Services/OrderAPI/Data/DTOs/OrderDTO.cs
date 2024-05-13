using System.ComponentModel.DataAnnotations;

namespace OrderAPI.Data.DTOs
{
    public class OrderDTO
    {
        public Guid Id { get; set; }
        public DateTime? Created { get; set; }
        public decimal RaceValue { get; set; }
        public string? Situation { get; set; }
        public Guid? DeliverymanId { get; set; }
    }
}
