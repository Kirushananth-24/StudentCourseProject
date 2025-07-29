using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using StudentCourseAPI.Data.Interfaces;
using StudentCourseAPI.DTOs;
using StudentCourseAPI.Models;

namespace StudentCourseAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;

        public AuthService(UserManager<ApplicationUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            var user = new ApplicationUser
            {
                UserName = registerDto.UserName,
                Email = registerDto.UserName,
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                return new AuthResponseDto
                {
                    IsAuthSuccessful = false,
                    Errors = result.Errors.Select(e => e.Description)
                };
            }

            // Optional: Add role
            if (!string.IsNullOrEmpty(registerDto.Role))
            {
                await _userManager.AddToRoleAsync(user, registerDto.Role);
            }

            // Generate email confirmation token
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            return new AuthResponseDto
            {
                IsAuthSuccessful = true,
                Token = token,
                UserId = user.Id,
                Email = user.Email
            };
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.UserName);

            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                return new AuthResponseDto { IsAuthSuccessful = false, Errors = new[] { "Invalid credentials." } };
            }


            if (!user.EmailConfirmed)
            {
                return new AuthResponseDto { IsAuthSuccessful = false, Errors = new[] { "Email not confirmed yet" } };
            }

            var token = await _tokenService.CreateToken(user);

            return new AuthResponseDto { IsAuthSuccessful = true, Token = token };
        }

        public async Task<IdentityResult> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            
            if (user == null) return IdentityResult.Failed(new IdentityError { Description = "User not found." });

            return await _userManager.ConfirmEmailAsync(user, token);
        }
    }
}
