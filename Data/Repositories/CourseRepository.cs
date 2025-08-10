using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentCourseAPI.Data.Interfaces;
using StudentCourseAPI.DTOs;
using StudentCourseAPI.Models;

namespace StudentCourseAPI.Data.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _context;

        public CourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            return await _context.Courses
                .Include(c => c.Department)
                .Include(c => c.Instructor)
                .Include(c => c.Enrollments)
                .ToListAsync();
        }

        public async Task<Course?> GetByIdAsync(int id)
        {
            return await _context.Courses
                .Include(c => c.Department)
                .Include(c => c.Instructor)
                .Include(c => c.Enrollments)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Course> AddAsync(CourseDto courseDto)
        {
            var department = await _context.Departments.FindAsync(courseDto.DepartmentId);
            var instructor = await _context.Instructors.FindAsync(courseDto.InstructorId);

            if (department == null)
                throw new Exception($"Department with ID {courseDto.DepartmentId} not found.");
            if (instructor == null)
                throw new Exception($"Instructor with ID {courseDto.InstructorId} not found.");

            var course = new Course
            {
                Title = courseDto.Title,
                Credits = courseDto.Credits,
                DepartmentId = courseDto.DepartmentId,
                InstructorId = courseDto.InstructorId,
                Department = department,
                Instructor = instructor
            };

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            await _context.Entry(course).Reference(c => c.Department).LoadAsync();
            await _context.Entry(course).Reference(c => c.Instructor).LoadAsync();

            return course;
        }

        public async Task<Course> UpdateAsync(CourseDto courseDto)
        {
            var existingCourse = await _context.Courses
                .Include(c => c.Department)
                .Include(c => c.Instructor)
                .FirstOrDefaultAsync(c => c.Id == courseDto.Id);

            if (existingCourse == null)
                throw new Exception($"Course with ID {courseDto.Id} not found.");

            existingCourse.Title = courseDto.Title;
            existingCourse.Credits = courseDto.Credits;

            if (existingCourse.DepartmentId != courseDto.DepartmentId)
            {
                var department = await _context.Departments.FindAsync(courseDto.DepartmentId);
                if (department == null)
                    throw new Exception($"Department with ID {courseDto.DepartmentId} not found.");
                existingCourse.DepartmentId = courseDto.DepartmentId;
                existingCourse.Department = department;
            }

            if (existingCourse.InstructorId != courseDto.InstructorId)
            {
                var instructor = await _context.Instructors.FindAsync(courseDto.InstructorId);
                if (instructor == null)
                    throw new Exception($"Instructor with ID {courseDto.InstructorId} not found.");
                existingCourse.InstructorId = courseDto.InstructorId;
                existingCourse.Instructor = instructor;
            }

            await _context.SaveChangesAsync();
            return existingCourse;
        }

        public async Task DeleteAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
            }
        }
    }
}