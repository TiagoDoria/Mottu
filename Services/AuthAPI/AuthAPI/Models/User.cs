using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Xml.Schema;

namespace AuthAPI.Models
{
    public class User : IdentityUser
    {
        [Required]
        public string Name { get; set; }
        public string? Cnpj { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? DriversLicenseNumber { get; set; }
        public Guid? LicenseTypeId { get; set; }
    }
}
