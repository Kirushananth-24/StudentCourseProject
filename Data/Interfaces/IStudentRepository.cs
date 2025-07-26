using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentCourseAPI.Models;

namespace StudentCourseAPI.Data.Interfaces
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAllAsync();
        Task<Student?> GetByIdAsync(int id);
        Task<Student> AddAsync(Student student);
        Task<Student> UpdateAsync(Student student);
        Task DeleteAsync(int id);
    }
}