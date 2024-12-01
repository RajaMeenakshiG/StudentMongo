using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentMongo.Models;
using StudentMongo.Services;

namespace StudentMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly StudentService _studentService;

        public StudentsController(StudentService studentService) =>
            _studentService = studentService;

        [HttpGet]
        public async Task<ActionResult<List<Student>>> Get()
        {
            await _studentService.GetAsync();
            return Ok();
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Student>> Get(string id)
        {
            var student = await _studentService.GetAsync(id);
            if (student is null)
                return NotFound();
            return student;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student newStudent)
        {
            await _studentService.CreateAsync(newStudent);
            return CreatedAtAction(nameof(Get), new { id = newStudent.Id }, newStudent);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Student updatedStudent)
        {
            var student = await _studentService.GetAsync(id);
            if (student is null)
                return NotFound();

            updatedStudent.Id = student.Id;
            await _studentService.UpdateAsync(id, updatedStudent);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var student = await _studentService.GetAsync(id);
            if (student is null)
                return NotFound();

            await _studentService.RemoveAsync(id);
            return NoContent();
        }
    }
}
