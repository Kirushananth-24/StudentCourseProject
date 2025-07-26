using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentCourseAPI.Data.Interfaces;
using StudentCourseAPI.Models;

namespace StudentCourseAPI.Data.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context;
        public StudentRepository(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _context.Students.Include(s => s.Enrollments).ToListAsync();
        }

        public async Task<Student?> GetByIdAsync(int id)
        {
            return await _context.Students.Include(s => s.Enrollments).FirstOrDefaultAsync(s => s.Id == id);

        }
        public async Task<Student> AddAsync(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return student;
        }

        public async Task<Student> UpdateAsync(Student student)
        {
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
            return student;
        }

        public async Task DeleteAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }
        }
    }
}