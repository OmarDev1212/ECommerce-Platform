using DomainLayer.Exceptions;
using DomainLayer.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceAbstractions;
using Shared.DTO.IdentityModule;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AuthenticationService(UserManager<ApplicationUser> userManager, IConfiguration configuration) : IAuthenticationService
    {
        public async Task<UserDto> Login(LoginDto loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Email);
            if (user is not null)
            {
                var flag = await userManager.CheckPasswordAsync(user, loginDto.Password);
                if (flag)
                {
                    return new UserDto()
                    {
                        Email = loginDto.Email,
                        DisplayName = user.DisplayName,
                        Token = await CreateTokenAsync(user)
                    };
                }
            }
            throw new BadRequestException("Invalid Email Or Password");

        }


        public async Task<UserDto> Register(RegisterDto registerDto)
        {
            var user = await userManager.FindByEmailAsync(registerDto.Email);
            if (user is not null)
                throw new BadRequestException("Email already Existed");
            var newUser = new ApplicationUser()
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.UserName,
                PhoneNumber = registerDto.PhoneNumber,
            };
            var result = await userManager.CreateAsync(newUser, registerDto.Password);
            if (result.Succeeded)
            {
                return new UserDto()
                {
                    Email = registerDto.Email,
                    DisplayName = registerDto.DisplayName,
                    Token = await CreateTokenAsync(newUser)
                };
            }
            else
            {
                var errors = result.Errors.Select(e => e.Description);
                throw new BadRequestWithErrorsException(errors);
            }
        }

        private async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            //1.claims
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            //securityKey
            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]!));
            //creating token with JwtSecurityToken

            var token = new JwtSecurityToken(
                issuer: configuration["JWT:Issuer"],
                audience: configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(double.Parse(configuration["JWT:Expire"]!)),
                signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
