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
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IEmailService _emailService;
        public AuthController(IAuthService authService, IEmailService emailService)
        {
            _authService = authService;
            _emailService = emailService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var email = registerDto.UserName;
            var result = await _authService.RegisterAsync(registerDto);

            if (!result.IsAuthSuccessful)
                return BadRequest(result);

            // Generate confirmation link inside the controller
            var confirmationLink = Url.Action("ConfirmEmail", "Auth", new
            {
                userId = result.UserId,
                token = System.Net.WebUtility.UrlEncode(result.Token)
            }, Request.Scheme);

            var emailSubject = "Email Confirmation";
            var emailBody = $"Please confirm your email by clicking this link: {confirmationLink}";

            await _emailService.SendEmailAsync(email, emailSubject, emailBody);

            return Ok("Registration successful. Confirmation email sent.");
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
            token = System.Net.WebUtility.UrlDecode(token);

            var result = await _authService.ConfirmEmailAsync(userId, token);

            if (result.Succeeded)
            {
                return Ok("Email confirmed successfully!");
            }

            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return BadRequest($"Email confirmation failed: {errors}");
        }
    }
}