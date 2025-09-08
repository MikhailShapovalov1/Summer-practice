using task13;
using System.Text.Json;

namespace task13tests
{
    public class StudentJsonSerializerTests
    {
        private readonly Student _testStudent = new Student
        {
            FirstName = "John",
            LastName = "Doe",
            BirthDate = new DateTime(2000, 1, 1),
            Grades = new List<Subject>
            {
                new Subject { Name = "Math", Grade = 5 },
                new Subject { Name = "Physics", Grade = 4 }
            }
        };

        private readonly Student _testStudentWithNullGrades = new Student
        {
            FirstName = "Jane",
            LastName = "Smith",
            BirthDate = new DateTime(2001, 5, 15),
            Grades = null
        };

        [Fact]
        public void SerializeToJson_ValidStudent_ReturnsValidJson()
        {
            string json = StudentJsonSerializer.SerializeToJson(_testStudent);

            Assert.Contains("\"FirstName\": \"John\"", json);
            Assert.Contains("\"LastName\": \"Doe\"", json);
            Assert.Contains("\"BirthDate\": \"2000-01-01\"", json);
            Assert.Contains("\"Name\": \"Math\"", json);
            Assert.Contains("\"Grade\": 5", json);
        }

        [Fact]
        public void SerializeToJson_StudentWithNullGrades_IgnoresNullGrades()
        {
            string json = StudentJsonSerializer.SerializeToJson(_testStudentWithNullGrades);

            Assert.DoesNotContain("\"Grades\": null", json);
            Assert.DoesNotContain("\"Grades\": []", json);
        }

        [Fact]
        public void DeserializeFromJson_ValidJson_ReturnsStudent()
        {
            string json = @"{
                ""FirstName"": ""John"",
                ""LastName"": ""Doe"",
                ""BirthDate"": ""2000-01-01"",
                ""Grades"": [
                    { ""Name"": ""Math"", ""Grade"": 5 },
                    { ""Name"": ""Physics"", ""Grade"": 4 }
                ]
            }";

            Student student = StudentJsonSerializer.DeserializeFromJson(json);

            Assert.Equal("John", student.FirstName);
            Assert.Equal("Doe", student.LastName);
            Assert.Equal(new DateTime(2000, 1, 1), student.BirthDate);
            Assert.Equal(2, student.Grades!.Count);
            Assert.Equal("Math", student.Grades[0].Name);
            Assert.Equal(5, student.Grades[0].Grade);
        }

        [Fact]
        public void DeserializeFromJson_InvalidJson_ThrowsException()
        {
            string invalidJson = @"{
                ""FirstName"": """",
                ""LastName"": ""Doe"",
                ""BirthDate"": ""2000-01-01""
            }";

            Assert.Throws<JsonException>(() => StudentJsonSerializer.DeserializeFromJson(invalidJson));
        }

        [Fact]
        public void SaveAndLoadFromFile_ValidStudent_RoundTripSuccess()
        {
            string filePath = Path.Combine(Path.GetTempPath(), "student_test.json");

            StudentJsonSerializer.SaveToFile(_testStudent, filePath);
            Student loadedStudent = StudentJsonSerializer.LoadFromFile(filePath);

            Assert.Equal(_testStudent.FirstName, loadedStudent.FirstName);
            Assert.Equal(_testStudent.LastName, loadedStudent.LastName);
            Assert.Equal(_testStudent.BirthDate, loadedStudent.BirthDate);
            Assert.Equal(_testStudent.Grades!.Count, loadedStudent.Grades!.Count);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
