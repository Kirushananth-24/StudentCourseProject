using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudentCourseAPI.Data.Interfaces;
using StudentCourseAPI.Models;

namespace StudentCourseAPI.Controllers
{
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;

        public StudentsController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Instructor")]
        public async Task<IActionResult> GetAllStudents()
        {
            return Ok(await _studentRepository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            return student == null ? NotFound() : Ok(student);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateStudent(Student student) 
        {
            return Ok(await _studentRepository.AddAsync(student));
        } 

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateStudent(int id, Student student)
        {
            if (id != student.Id) return BadRequest();
            return Ok(await _studentRepository.UpdateAsync(student));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            await _studentRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}