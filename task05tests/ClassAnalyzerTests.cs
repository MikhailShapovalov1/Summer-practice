using Xunit;
using task05;

namespace task05tests
{
    public class TestClass
    {
        public int PublicField;
        private string _privateField = string.Empty;
        public int Property { get; set; }

        public void Method() { }
        public void ParamsMethod(int age, string name) { }
    }

    [Serializable]
    public class AttributedClass { }

    public class ClassAnalyzerTests
    {
        [Fact]
        public void GetPublicMethods_ReturnsCorrectMethods()
        {
            var analyzer = new ClassAnalyzer(typeof(TestClass));
            var methods = analyzer.GetPublicMethods();

            Assert.Contains("Method", methods);
            Assert.Contains("ParamsMethod", methods);
        }

        [Fact]
        public void GetAllFields_IncludesPrivateFields()
        {
            var analyzer = new ClassAnalyzer(typeof(TestClass));
            var fields = analyzer.GetAllFields();

            Assert.Contains("PublicField", fields);
            Assert.Contains("_privateField", fields);
        }

        [Fact]
        public void GetMethodParams_ReturnsCorrectParams()
        {
            var analyzer = new ClassAnalyzer(typeof(TestClass));
            var parameters = analyzer.GetMethodParams("ParamsMethod");

            Assert.Contains("age", parameters);
            Assert.Contains("name", parameters);
        }

        [Fact]
        public void GetProperties_ReturnsCorrectProperties()
        {
            var analyzer = new ClassAnalyzer(typeof(TestClass));
            var properties = analyzer.GetProperties();

            Assert.Contains("Property", properties);
        }

        [Fact]
        public void HasAttribute_ReturnsCorrectValue()
        {
            var analyzer = new ClassAnalyzer(typeof(AttributedClass));
            bool hasAttribute = analyzer.HasAttribute<SerializableAttribute>();

            Assert.True(hasAttribute);
        }
    }
}
