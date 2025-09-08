namespace task13
{
    public class Subject
    {
        public string Name { get; set; } = string.Empty;
        public int Grade { get; set; }

        public Subject() { }

        public Subject(string name, int grade)
        {
            Name = name;
            Grade = grade;
        }
    }
}
