using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCourseAPI.Models
{
    public class Instructor
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime HireDate { get; set; }
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}