using System.Data;
using System.Data.Common;
using CleanDapper.Domain.Entities;
using Dapper;
using Npgsql;

namespace CleanDapper.Infrastructure.Repository
{
    public class NpgsqlStudentRepository : IStudentRepository
    {
        private readonly string _connectionString;

        public NpgsqlStudentRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private IDbConnection CreateConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }

        public async Task<IEnumerable<Student>> GetAll()
        {
            string sql = @"SELECT * FROM public.""Students""";
            using (var dbConnection = CreateConnection())
            {
                return await dbConnection.QueryAsync<Student>(sql);
            }
        }

            public async Task<IEnumerable<Course>> GetStudentsOverAge()
            {
                string sql = @"select distinct(c.*) 
                               from public.""Courses"" as ""c"",
                                    public.""Students"" as ""s"",
                                    public.""StudentCourses"" as ""sc"" 
                               where c.""Id"" = sc.""CourseId"" and 
                                     s.""Id"" = sc.""StudentId"" and
                                     s.""Age"" >20";
                using (var dbConnection = CreateConnection())
                {
                    return await dbConnection.QueryAsync<Course>(sql);
                }
            }

            public async Task AssignCourseToStudent(Student student, int courseId)
            {
                string sql = @"INSERT INTO public.""Students"" as ""s"" VALUES (@StudentId, @StudentFirstName, @StudentLastName, @StudentAge)";
                string sql2 = @"INSERT INTO public.""StudentCourses"" (""StudentId"", ""CourseId"") VALUES (@StudentId, @CourseId)";
                using (var dbConnection = CreateConnection())
                {
                    await dbConnection.ExecuteAsync(sql, new { StudentId = student.Id, StudentFirstName = student.FirstName, StudentLastName = student.LastName, StudentAge = student.Age });
                    await dbConnection.ExecuteAsync(sql2, new { StudentId = student.Id, CourseId = courseId });
                }
            }

            public async Task UpdateStudentCourse(int studentId)
            {
                string sql = @"DELETE FROM public.""StudentCourses"" WHERE ""StudentId"" = @StudentId";
                string sql2 = @"DELETE from public.""Students"" as ""s"" where s.""Id"" = @StudentId";
                using (var dbConnection = CreateConnection())
                {
                    await dbConnection.ExecuteAsync(sql, new { StudentId = studentId });
                    await dbConnection.ExecuteAsync(sql2, new { StudentId = studentId });
                }
            }
        }
    }
