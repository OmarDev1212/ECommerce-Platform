using Shared.DTO.IdentityModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstractions
{
    public interface IAuthenticationService
    {
        Task<UserDto> Login(LoginDto loginDto);
        Task<UserDto> Register(RegisterDto registerDto);
        Task<bool> CheckEmailExist(string email);
        Task<UserDto> GetCurrentUser(string email);
        Task<AddressDto> GetCurrentUserAddress(string email);
        Task<AddressDto>UpdateUserAddress(string email,AddressDto addressDto);
    }
}
