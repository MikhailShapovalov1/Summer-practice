using System.Text.Json;
using System.Text.Json.Serialization;

namespace task13
{
    public static class StudentJsonSerializer
    {
        private static readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        public static string SerializeToJson(Student student)
        {
            if (student == null)
            {
                throw new ArgumentNullException(nameof(student));
            }

            return JsonSerializer.Serialize(student, _options);
        }

        public static Student DeserializeFromJson(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                throw new ArgumentException("JSON string cannot be null or empty", nameof(json));
            }

            var student = JsonSerializer.Deserialize<Student>(json, _options)!;

            if (string.IsNullOrWhiteSpace(student.FirstName))
            {
                throw new JsonException("FirstName is required");
            }

            if (string.IsNullOrWhiteSpace(student.LastName))
            {
                throw new JsonException("LastName is required");
            }

            if (student.BirthDate > DateTime.Now)
            {
                throw new JsonException("BirthDate cannot be in the future");
            }

            return student;
        }

        public static void SaveToFile(Student student, string filePath)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student));

            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("File path cannot be null or empty", nameof(filePath));

            string json = SerializeToJson(student);
            File.WriteAllText(filePath, json);
        }

        public static Student LoadFromFile(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("File path cannot be null or empty", nameof(filePath));

            if (!File.Exists(filePath))
                throw new FileNotFoundException("File not found", filePath);

            string json = File.ReadAllText(filePath);
            return DeserializeFromJson(json);
        }
    }
}
