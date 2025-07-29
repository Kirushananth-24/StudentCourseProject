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
    public class InstructorController : ControllerBase
    {
        private readonly IInstructorRepository _instructorRepository;

        public InstructorController(IInstructorRepository instructorRepository)
        {
            _instructorRepository = instructorRepository;
        }

        [HttpGet("getAllInstructors")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllInstructors()
        {
            return Ok(await _instructorRepository.GetAllAsync());
        }

        [HttpGet("getInstructorById/{id}")]
        public async Task<IActionResult> GetInstructorById(int id)
        {
            var instructor = await _instructorRepository.GetByIdAsync(id);
            return instructor == null ? NotFound() : Ok(instructor);
        }

        [HttpPost("createInstructor")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateInstructor([FromBody] Instructor instructor)
        {
            var created = await _instructorRepository.AddAsync(instructor);
            return CreatedAtAction(nameof(GetInstructorById), new { id = created.Id }, created);
        }

        [HttpPut("updateInstructor/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateInstructor(int id, [FromBody] Instructor instructor)
        {
            if (id != instructor.Id)
                return BadRequest();

            return Ok(await _instructorRepository.UpdateAsync(instructor));
        }

        [HttpDelete("deleteInstructor/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteInstructor(int id)
        {
            await _instructorRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}