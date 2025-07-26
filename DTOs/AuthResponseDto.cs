using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCourseAPI.DTOs
{
    public class AuthResponseDto
    {
        public bool IsAuthSuccessful { get; set; }
        public string? Token { get; set; }
        public IEnumerable<string>? Errors { get; set; }
    }
}