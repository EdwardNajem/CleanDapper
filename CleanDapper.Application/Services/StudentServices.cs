using CleanDapper.Domain.Entities;
using CleanDapper.Infrastructure.Repository;

namespace CleanDapper.Application.Services
{
    public class StudentServices : IStudentServices
    {
        private readonly IStudentRepository _studentRepository;

        public StudentServices(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<IEnumerable<Student>> GetAll()
        {
            var students = await _studentRepository.GetAll();
            return students;
        }

        public async Task<IEnumerable<Course>> GetCoursesForStudentsOverAge()
        {
            var courses = await _studentRepository.GetStudentsOverAge();

            return courses;
        }


        public async Task AssignCourseToStudent(Student student, int courseId)
        {
            await _studentRepository.AssignCourseToStudent(student, courseId);
        }

        public async Task UpdateOrRemoveStudentCourse(int studentId)
        {
            await _studentRepository.UpdateStudentCourse(studentId);
        }
    }
}
