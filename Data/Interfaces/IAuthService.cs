using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using StudentCourseAPI.DTOs;

namespace StudentCourseAPI.Data.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto);
        Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
        Task<IdentityResult> ConfirmEmailAsync(string userId, string token);
    }
}