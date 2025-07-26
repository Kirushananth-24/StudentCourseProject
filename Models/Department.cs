using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCourseAPI.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Budget { get; set; }
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}