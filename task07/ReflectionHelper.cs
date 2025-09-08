using System;
using System.Reflection;
using System.ComponentModel;
using System.Linq;

namespace task07
{
    public static class ReflectionHelper
    {
        public static void PrintTypeInfo(Type type)
        {
            DisplayClassAttributes(type);

            DisplayMethodInfo(type);

            DisplayPropertyInfo(type);
        }

        private static void DisplayClassAttributes(Type type)
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
        }

        private static void DisplayMethodInfo(Type type)
        {
            Console.WriteLine("nMethods:");
            var methods = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly);

            var methodDisplayNames = methods
                .Select(method => method.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName)
                .Where(displayName => !string.IsNullOrEmpty(displayName));

            foreach (var displayName in methodDisplayNames)
            {
                Console.WriteLine(displayName);
            }
        }

        private static void DisplayPropertyInfo(Type type)
        {
            Console.WriteLine("nProperties:");
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly);

            var propertyDisplayNames = properties
                .Select(property => property.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName)
                .Where(displayName => !string.IsNullOrEmpty(displayName));

            foreach (var displayName in propertyDisplayNames)
            {
                Console.WriteLine(displayName);
            }
        }
    }
}