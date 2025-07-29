using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentCourseAPI.Models;

namespace StudentCourseAPI.Data.Interfaces
{
    public interface IInstructorRepository
    {
        Task<IEnumerable<Instructor>> GetAllAsync();
        Task<Instructor?> GetByIdAsync(int id);
        Task<Instructor> AddAsync(Instructor instructor);
        Task<Instructor> UpdateAsync(Instructor instructor);
        Task DeleteAsync(int id);
    }
}