using CleanDapper.Domain.Entities;

namespace CleanDapper.Application.Services
{
    public interface IStudentServices
    {
        Task<IEnumerable<Student>> GetAll();
        Task<IEnumerable<Course>> GetCoursesForStudentsOverAge();
        Task AssignCourseToStudent(Student student, int courseId);
        Task UpdateOrRemoveStudentCourse(int studentId);
    }
}
