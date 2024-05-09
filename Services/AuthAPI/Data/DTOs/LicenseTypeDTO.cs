namespace AuthAPI.Data.DTOs
{
    public class LicenseTypeDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Description { get; set; }
    }
}
