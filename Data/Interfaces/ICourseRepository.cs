using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentCourseAPI.Models;

namespace StudentCourseAPI.Data.Interfaces
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetAllAsync();
        Task<Course?> GetByIdAsync(int id);
        Task<Course> AddAsync(Course course);
        Task<Course> UpdateAsync(Course course);
        Task DeleteAsync(int id);
    }
}