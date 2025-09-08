using System.Reflection;
using task07;
using task09;

namespace task09tests
{
    public class MetadataPrinterTests
    {
        [DisplayName("Test")]
        [Version(1,0)]
        public class TestClass
        {
            public TestClass() { }

            [DisplayName("Method")]
            public void TestMethod() { }
            public void TestMethodWithParams(int param1, string param2) { }
        }

        [Fact]
        public void EmptyPathTest_PrintsEmptyPath()
        {
            var input = string.Empty;

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            Console.SetIn(new StringReader(input));

            MetadataPrinter.Main();
            var output = stringWriter.ToString();

            Assert.Contains("Empty path", output);
        }

        [Fact]
        public void NotFoundTest_PrintsNotFound()
        {
            var input = "nonexistent.dll";

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            Console.SetIn(new StringReader(input));

            MetadataPrinter.Main();
            var output = stringWriter.ToString();

            Assert.Contains("Files not found", output);
        }

        [Fact]
        public void DisplayClassInfo_DisplaysCorrectInfo()
        {
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            var type = typeof(TestClass);

            MetadataPrinter.DisplayClassInfo(type);
            var output = stringWriter.ToString();

            Assert.Contains($"Class: {type.FullName}", output);
            Assert.Contains("Constructors:", output);
            Assert.Contains("Methods:", output);
        }

        [Fact]
        public void DisplayAttributes_CorrectDisplayClassAttributes()
        {
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            var attributes = typeof(TestClass).GetMethod("TestMethod")!.GetCustomAttributes();

            MetadataPrinter.DisplayAttributes(attributes);
            var output = stringWriter.ToString();

            Assert.Contains("Attributes:", output);
            Assert.Contains("DisplayNameAttribute", output);
        }

        [Fact]
        public void DisplayAttributes_CorrectDisplayMethodAttribute()
        {
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            var attributes = typeof(TestClass).GetCustomAttributes();

            MetadataPrinter.DisplayAttributes(attributes);
            var output = stringWriter.ToString();

            Assert.Contains("Attributes:", output);
            Assert.Contains("DisplayNameAttribute", output);
        }

        [Fact]
        public void DisplayConstructors_CorrectDisplaysConstructors()
        {
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            var type = typeof(TestClass);

            MetadataPrinter.DisplayConstructors(type);
            var output = stringWriter.ToString();

            Assert.Contains("Constructors:", output);
            Assert.Contains($"{type.Name}(", output);
        }

        [Fact]
        public void DisplayMethods_CorrectDisplaysMethods()
        {
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            var type = typeof(TestClass);

            MetadataPrinter.DisplayMethods(type);
            var output = stringWriter.ToString();

            Assert.Contains("Methods:", output);
            Assert.Contains("Void TestMethod()", output);
        }

        [Fact]
        public void DisplayParameters_CorrectDisplaysParameters()
        {
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            var parameters = typeof(TestClass).GetMethod("TestMethodWithParams")!.GetParameters();

            MetadataPrinter.DisplayParameters(parameters);
            var output = stringWriter.ToString();

            Assert.Contains("Int32 param1, String param2", output);
        }
    }
}
