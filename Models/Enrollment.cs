using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCourseAPI.Models
{
    public class Enrollment
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
        public string? Grade { get; set; }
    }
}