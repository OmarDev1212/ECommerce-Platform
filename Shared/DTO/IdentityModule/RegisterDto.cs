using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO.IdentityModule
{
    public class RegisterDto
    {
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
    }
}
