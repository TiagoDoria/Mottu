namespace AuthAPI.Data.DTOs
{
    public class RegistrationRequestDTO
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Cnpj { get; set; }
        public DateTime BirthDate { get; set; }
        public string DriversLicenseNumber { get; set; }
        public Guid LicenseTypeId { get; set; }
        public string Password { get; set; }
        public string? Role { get; set; } = "Deliveryman";
    }
}
