using Microsoft.AspNetCore.Identity;

namespace DomainLayer.Models.Identity
{
    public class ApplicationUser : IdentityUser<string>
    {
        public string DisplayName { get; set; } = default!;
        public Address Address { get; set; }
    }
}
