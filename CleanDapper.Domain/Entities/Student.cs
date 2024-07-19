namespace CleanDapper.Domain.Entities
{
    public class Student
    {
        public required int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required int Age { get; set; }
    }
}
