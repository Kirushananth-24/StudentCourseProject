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
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;

        public StudentsController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [HttpGet("getAllStudents")]
        [Authorize(Roles = "Admin,Instructor")]
        public async Task<IActionResult> GetAllStudents()
        {
            return Ok(await _studentRepository.GetAllAsync());
        }

        [HttpGet("GetStudentById/{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            return student == null ? NotFound() : Ok(student);
        }

        [HttpPost("createStudent")]
        [Authorize]
        public async Task<ActionResult> CreateStudent([FromBody] Student student) 
        {
            return Ok(await _studentRepository.AddAsync(student));
        } 

        [HttpPut("updateStudent/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateStudent(int id, Student student)
        {
            if (id != student.Id) return BadRequest();
            return Ok(await _studentRepository.UpdateAsync(student));
        }

        [HttpDelete("deleteStudent/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            await _studentRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}