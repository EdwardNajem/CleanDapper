using CleanDapper.Application.Services;
using CleanDapper.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CleanDapper.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentServices _studentServices;

        public StudentController(IStudentServices studentServices)
        {
            _studentServices = studentServices;
        }
        [HttpGet("getAllStudents")]
        public async Task<IActionResult> GetAll()
        {
            var students = await _studentServices.GetAll();
            return Ok(students);
        }

        [HttpGet("coursesOverAge/")]
        public async Task<IActionResult> GetCoursesForStudentsOverAge()
        {
            var courses = await _studentServices.GetCoursesForStudentsOverAge();
            return Ok(courses);
        }

        [HttpPost("AssignCourse")]
        public async Task<IActionResult> AssignCourseToStudent([FromBody] Student student, [FromQuery] int courseId)
        {
            await _studentServices.AssignCourseToStudent(student, courseId);
            return Ok();
        }

        [HttpPut("RemoveCourse")]
        public async Task<IActionResult> UpdateOrRemoveStudentCourse([FromQuery] int studentId)
        {
            await _studentServices.UpdateOrRemoveStudentCourse(studentId);
            return Ok();
        }
    }
}
