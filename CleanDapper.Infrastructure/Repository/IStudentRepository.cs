using CleanDapper.Domain.Entities;

namespace CleanDapper.Infrastructure.Repository
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAll();
        Task<IEnumerable<Course>> GetStudentsOverAge();
        Task AssignCourseToStudent(Student student, int courseId);
        Task UpdateStudentCourse(int studentId);
    }
}
