using System.ComponentModel.DataAnnotations;

namespace OrderAPI.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public decimal RaceValue { get; set; }
        [Required]
        public string Situation { get; set; } = "disponivel";
        public Guid DeliverymanId { get; set; }

        public class SituacaoValidaAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var situation = value as string;
                if (situation != null && (situation == "aceito" || situation == "entregue" || situation == "disponivel"))
                {
                    return ValidationResult.Success;
                }

                return new ValidationResult(ErrorMessage);
            }
        }
    }
}
