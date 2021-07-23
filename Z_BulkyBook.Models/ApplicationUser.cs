using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Z_BulkyBook.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }

        public int? CompanyId { get; set; } // ? can be null value for companyId

        [ForeignKey("CompanyId")]
        public Company Company { get; set; }

        // not add to database
        [NotMapped]
        public string Role { get; set; }
    }
}
