using DomainLayer.Exceptions;
using DomainLayer.Models.Identity;
using Microsoft.AspNetCore.Identity;
using ServiceAbstractions;
using Shared.DTO.IdentityModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AuthenticationService(UserManager<ApplicationUser> _userManager) : IAuthenticationService
    {
        public async Task<UserDto> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user is not null)
            {
                var flag = await _userManager.CheckPasswordAsync(user, loginDto.Password);
                if (flag)
                {
                    return new UserDto()
                    {
                        Email = loginDto.Email,
                        DisplayName = user.DisplayName,
                        Token = CreateTokenAsync(user)
                    };
                }
            }
            throw new BadRequestException("Invalid Email Or Password");

        }


        public async Task<UserDto> Register(RegisterDto registerDto)
        {
            var user = await _userManager.FindByEmailAsync(registerDto.Email);
            if (user is not null)
                throw new BadRequestException("Email already Existed");
            var newUser = new ApplicationUser()
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.UserName,
                PhoneNumber = registerDto.PhoneNumber,
            };
            var result = await _userManager.CreateAsync(newUser, registerDto.Password);
            if (result.Succeeded)
            {
                return new UserDto()
                {
                    Email = registerDto.Email,
                    DisplayName = registerDto.DisplayName,
                    Token = CreateTokenAsync(newUser)
                };
            }
            else
            {
                var errors = result.Errors.Select(e => e.Description);
                throw new BadRequestWithErrorsException(errors);
            }
        }

        private static string CreateTokenAsync(ApplicationUser user)
        {
            return "ToDo";
        }

    }
}
