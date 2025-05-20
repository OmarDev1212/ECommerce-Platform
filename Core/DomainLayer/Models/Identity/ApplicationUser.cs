using Microsoft.AspNetCore.Identity;

namespace DomainLayer.Models.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string DisplayName { get; set; } = default!;
        public Address Address { get; set; }
    }
}
