using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentMongo.Models;
using StudentMongo.Services;

namespace StudentMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarksController : ControllerBase
    {
        private readonly MarkService _markService;

        public MarksController(MarkService markService)
        {
            _markService = markService;
        }

        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> GetMarksByStudentId(string studentId)
        {
            var marks = await _markService.GetMarksByStudentIdAsync(studentId);
            if (marks == null)
                return NotFound($"No marks found for student with ID {studentId}");

            return Ok(marks);
        }


        [HttpPost]
        public async Task<IActionResult> CreateMark([FromBody] Mark newMark)
        {
            if (newMark == null || newMark.SubjectMarks == null || !newMark.SubjectMarks.Any())
                return BadRequest("Mark data for subjects is required.");

            await _markService.CreateMarkAsync(newMark);
            return CreatedAtAction(nameof(GetMarksByStudentId), new { studentId = newMark.StudentId }, newMark);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMark(string id, [FromBody] Mark updatedMark)
        {
            if (updatedMark == null || updatedMark.SubjectMarks == null || !updatedMark.SubjectMarks.Any())
                return BadRequest("Updated mark data for subjects is required.");

            await _markService.UpdateMarkAsync(id, updatedMark);
            return NoContent();
        }

        [HttpGet("student/{studentId}/average")]
        public async Task<IActionResult> GetAveragePercentage(string studentId)
        {
            var averagePercentage = await _markService.GetAveragePercentageByStudentIdAsync(studentId);
            if (averagePercentage == 0)
                return NotFound($"No marks found for student with ID {studentId}");

            return Ok(new { studentId, averagePercentage });
        }
    }
}
