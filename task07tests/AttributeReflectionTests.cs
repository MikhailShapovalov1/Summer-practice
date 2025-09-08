using System.Reflection;
using task07;
using DisplayNameAttribute = task07.DisplayNameAttribute;

namespace task07tests
{
    public class AttributeReflectionTests
    {
        [Fact]
        public void Class_HasDisplayNameAttribute()
        {
            var type = typeof(SampleClass);
            var attribute = type.GetCustomAttribute<DisplayNameAttribute>();
            Assert.NotNull(attribute);
            Assert.Equal("Пример класса", attribute.DisplayName);
        }

        [Fact]
        public void Method_HasDisplayNameAttribute()
        {
            var method = typeof(SampleClass).GetMethod("TestMethod");
            var attribute = method!.GetCustomAttribute<DisplayNameAttribute>();
            Assert.NotNull(attribute);
            Assert.Equal("Тестовый метод", attribute.DisplayName);
        }

        [Fact]
        public void Property_HasDisplayNameAttribute()
        {
            var prop = typeof(SampleClass).GetProperty("Number");
            var attribute = prop!.GetCustomAttribute<DisplayNameAttribute>();
            Assert.NotNull(attribute);
            Assert.Equal("Числовое свойство", attribute.DisplayName);
        }

        [Fact]
        public void Class_HasVersionAttribute()
        {
            var type = typeof(SampleClass);
            var attribute = type.GetCustomAttribute<VersionAttribute>();
            Assert.NotNull(attribute);
            Assert.Equal(1, attribute.Major);
            Assert.Equal(0, attribute.Minor);
        }

        [Fact]
        public void PrintTypeInfo_PrintsCorrectInfo()
        {
            StringWriter writer = new StringWriter();
            Console.SetOut(writer);

            ReflectionHelper.PrintTypeInfo(typeof(SampleClass));

            string[] result = writer.ToString().Split("\n\r".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            Assert.Equal("Class: Пример класса", result[0]);
            Assert.Equal("Version: 1.0", result[1]);
            Assert.Equal("Methods:", result[2]);
            Assert.Equal("Тестовый метод", result[3]);
            Assert.Equal("Properties:", result[4]);
            Assert.Equal("Числовое свойство", result[5]);
        }
    }
}
