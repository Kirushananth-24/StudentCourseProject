using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudentCourseAPI.Data.Interfaces;
using StudentCourseAPI.DTOs;

namespace StudentCourseAPI.Controllers
{
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var result = await _authService.RegisterAsync(registerDto);
            if (!result.IsAuthSuccessful) return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var result = await _authService.LoginAsync(loginDto);
            if (!result.IsAuthSuccessful) return Unauthorized(result);
            return Ok(result);
        }

        [HttpGet("confirmemail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var result = await _authService.ConfirmEmailAsync(userId, token);

            if (result.Succeeded)
            {
                return Ok("Email confirmed successfully!");
            }

            return BadRequest("Email confirmation failed");
        }
    }
}