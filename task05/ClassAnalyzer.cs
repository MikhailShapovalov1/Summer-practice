using System;
using System.Reflection;

namespace task05
{
    public class ClassAnalyzer
    {
        private Type _type;

        public ClassAnalyzer(Type type)
        {
            _type = type;
        }

        public IEnumerable<string> GetPublicMethods()
        {
            var methods = _type.GetMethods(BindingFlags.Public | BindingFlags.Instance);
            return methods.Select(m => m.Name);
        }

        public IEnumerable<string> GetMethodParams(string methodname)
        {
            var method = _type.GetMethod(methodname)!;
            return method.GetParameters().Select(p => p.Name!);
        }

        public IEnumerable<string> GetAllFields()
        {
            var fields = _type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            return fields.Select(f => f.Name);
        }

        public IEnumerable<string> GetProperties()
        {
            var properties = _type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            return properties.Select(p => p.Name);
        }

        public bool HasAttribute<T>() where T : Attribute
        {
            return _type.GetCustomAttribute<T>() != null;
        }
    }
}
