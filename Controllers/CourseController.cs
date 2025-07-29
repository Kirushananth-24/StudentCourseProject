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
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;

        public CourseController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        [HttpGet("getAllCourses")]
        [Authorize(Roles = "Admin,Instructor")]
        public async Task<IActionResult> GetAllCourses()
        {
            return Ok(await _courseRepository.GetAllAsync());
        }

        [HttpGet("getCourseById/{id}")]
        public async Task<IActionResult> GetCourseById(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            return course == null ? NotFound() : Ok(course);
        }

        [HttpPost("createCourse")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCourse([FromBody] Course course)
        {
            var created = await _courseRepository.AddAsync(course);
            return CreatedAtAction(nameof(GetCourseById), new { id = created.Id }, created);
        }

        [HttpPut("updateCourse/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCourse(int id, [FromBody] Course course)
        {
            if (id != course.Id)
                return BadRequest();

            return Ok(await _courseRepository.UpdateAsync(course));
        }

        [HttpDelete("deleteCourse/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            await _courseRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}