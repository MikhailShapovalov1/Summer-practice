using System.Text.Json.Serialization;

namespace task13
{
    public class Student
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime BirthDate { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<Subject>? Grades { get; set; } = new List<Subject>();

        public Student() { }

        public Student(string firstName, string lastName, DateTime birthDate, List<Subject>? grades = null)
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            Grades = grades;
        }
    }
}
