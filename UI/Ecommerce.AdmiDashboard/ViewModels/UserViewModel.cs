using System.ComponentModel.DataAnnotations;

namespace Ecommerce.AdminDashboard.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        [EmailAddress]
        public string Email { get; set; } = default!;
        public string DisplayName { get; set; } = default!;
        [Phone]
        public string PhoneNumber { get; set; } = default!;
        public string Street { get; set; } = default!;
        public string City { get; set; } = default!;
        public string Country { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public IEnumerable<string> Roles { get; set; } = default!;
    }
}
