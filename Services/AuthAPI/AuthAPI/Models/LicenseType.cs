namespace AuthAPI.Models
{
    public class LicenseType
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Description { get; set; }
    }
}
