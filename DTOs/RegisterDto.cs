using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCourseAPI.DTOs
{
    public class RegisterDto
    {
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string ClientURI { get; set; } = null!;
    }
}