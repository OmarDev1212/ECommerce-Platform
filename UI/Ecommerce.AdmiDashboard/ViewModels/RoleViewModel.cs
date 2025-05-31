using System.ComponentModel.DataAnnotations;

namespace Ecommerce.AdminDashboard.ViewModels
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        [Required]
        [StringLength(256, ErrorMessage = "Role name cannot exceed 256 characters.")]
        public string Name { get; set; }
        public RoleViewModel()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
