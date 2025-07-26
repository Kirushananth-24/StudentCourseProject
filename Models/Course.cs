using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCourseAPI.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int Credits { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; } = null!;
        public int InstructorId { get; set; }
        public Instructor Instructor { get; set; } = null!;
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}