
namespace AuthAPI.Data.DTOs
{
    public class DeliverymanDTO
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Cnpj { get; set; }
        public DateTime BirthDate { get; set; }
        public string DriversLicenseNumber { get; set; }
        public Guid LicenseTypeId { get; set; }
    }
}
