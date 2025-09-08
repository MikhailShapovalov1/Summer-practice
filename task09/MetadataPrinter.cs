using System.Reflection;

namespace task09
{
    public class MetadataPrinter
    {
        public static void Main()
        {
            Console.WriteLine("Enter path to the library");
            string? libraryPath = Console.ReadLine();
            Console.WriteLine();

            if (string.IsNullOrEmpty(libraryPath))
            {
                Console.WriteLine("Empty path");
                return;
            }

            if (!File.Exists(libraryPath))
            {
                Console.WriteLine("Files not found");
                return;
            }

            Assembly assembly = Assembly.LoadFrom(libraryPath);
            var types = assembly.GetExportedTypes().OrderBy(t => t.Namespace + "." + t.Name);

            foreach (var type in types)
            {
                if (type.IsClass)
                {
                    PrintClassInfo(type);
                }
            }
        }

        public static void PrintClassInfo(Type type)
        {
            Console.WriteLine($"Class: {type.FullName}");
            PrintAttributes(type.GetCustomAttributes());
            PrintConstructors(type);
            PrintMethods(type);
            Console.WriteLine();
        }

        public static void PrintAttributes(IEnumerable<Attribute> attributes)
        {
            if (attributes.Any())
            {
                Console.WriteLine("  Attributes:");
                foreach (var attr in attributes)
                {
                    Console.WriteLine($"    - {attr.GetType().Name}");
                }
            }
        }

        public static void PrintConstructors(Type type)
        {
            var constructors = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);

            if (constructors.Length > 0)
            {
                Console.WriteLine("  Constructors:");
                foreach (var ctor in constructors)
                {
                    Console.Write($"    - {type.Name}(");
                    PrintParameters(ctor.GetParameters());
                    Console.WriteLine(")");
                    PrintAttributes(ctor.GetCustomAttributes());
                }
            }
        }

        public static void PrintMethods(Type type)
        {
            var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly).Where(m => !m.IsSpecialName).ToArray();

            if (methods.Length > 0)
            {
                Console.WriteLine("  Methods:");
                foreach (var method in methods)
                {
                    Console.Write($"    - {method.ReturnType.Name} {method.Name}(");
                    PrintParameters(method.GetParameters());
                    Console.WriteLine(")");
                    PrintAttributes(method.GetCustomAttributes());
                }
            }
        }

        public static void PrintParameters(ParameterInfo[] parameters)
        {
            for (int i = 0; i < parameters.Length; i++)
            {
                if (i > 0)
                {
                    Console.Write(", ");
                }

                var param = parameters[i];
                Console.Write($"{param.ParameterType.Name} {param.Name}");
            }
        }
    }
}