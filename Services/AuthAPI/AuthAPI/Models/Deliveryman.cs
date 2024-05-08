using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Xml.Schema;

namespace AuthAPI.Models
{
    public class Deliveryman : IdentityUser
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Cnpj { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public string DriversLicenseNumber { get; set; }
        [Required]
        public Guid LicenseTypeId { get; set; }
    }
}
