using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCourseAPI.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime EnrollmentDate { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}