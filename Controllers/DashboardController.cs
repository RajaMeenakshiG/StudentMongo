using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentMongo.Services;

namespace StudentMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly StudentDashboardService _dashboardService;

        public DashboardController(StudentDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("total-students")]
        public IActionResult GetTotalStudents()
        {
            var totalStudents = _dashboardService.GetTotalStudents();
            return Ok(new { TotalStudents = totalStudents });
        }

        [HttpGet("students-by-grade")]
        public IActionResult GetStudentsByGrade()
        {
            var studentsByGrade = _dashboardService.GetStudentsByGrade();
            return Ok(studentsByGrade);
        }

        [HttpGet("students-older-than/{age}")]
        public IActionResult GetStudentsOlderThan(int age)
        {
            var students = _dashboardService.GetStudentsOlderThan(age);
            return Ok(students);
        }
    }
}
