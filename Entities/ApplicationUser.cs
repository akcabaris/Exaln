using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Exaln.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public int TokenVersion { get; set; } = 1;

        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;
        [MaxLength(50)]
        public string LastName { get; set; } =string.Empty;
    }
}
