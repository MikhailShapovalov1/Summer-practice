using System.Reflection;

namespace task07
{
    public static class ReflectionHelper
    {
        public static void PrintTypeInfo(Type type)
        {
            var displayNameAttribute = type.GetCustomAttribute<DisplayNameAttribute>();

            if (displayNameAttribute != null)
            {
                Console.WriteLine($"Class: {displayNameAttribute.DisplayName}");
            }

            var versionAttribute = type.GetCustomAttribute<VersionAttribute>();

            if (versionAttribute != null)
            {
                Console.WriteLine($"Version: {versionAttribute.Version}");
            }

            Console.WriteLine("\nMethods:");

            var methods = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly);

            foreach (var method in methods)
            {
                var methodDisplayNameAttr = method.GetCustomAttribute<DisplayNameAttribute>();

                if (methodDisplayNameAttr != null)
                {
                    Console.WriteLine($"{methodDisplayNameAttr.DisplayName}");
                }
            }

            Console.WriteLine("\nProperties:");

            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly);

            foreach (var property in properties)
            {
                var propertyDisplayNameAttr = property.GetCustomAttribute<DisplayNameAttribute>();
                if (propertyDisplayNameAttr != null)
                {
                    Console.WriteLine($"{propertyDisplayNameAttr.DisplayName}");
                }
            }
        }
    }
}
