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
            }

            if (!File.Exists(libraryPath))
            {
                Console.WriteLine($"Files not found");
                return;
            }

            Assembly assembly = Assembly.LoadFrom(libraryPath);
            IEnumerable<Type> types = assembly.GetExportedTypes().OrderBy(t => t.Namespace + "." + t.Name);

            foreach (Type type in types)
            {
                if (type.IsClass)
                {
                    DisplayClassInfo(type);
                }
            }
        }

        public static void DisplayClassInfo(Type type)
        {
            Console.WriteLine($"Class: {type.FullName}");

            DisplayAttributes(type.GetCustomAttributes());
            DisplayConstructors(type);
            DisplayMethods(type);

            Console.WriteLine();
        }

        public static void DisplayAttributes(IEnumerable<Attribute> attributes)
        {
            if (attributes.Any())
            {
                Console.WriteLine("  Attributes:");
            }

            foreach (Attribute attr in attributes)
            {
                Console.WriteLine($"    - {attr.GetType().Name}");
            }
        }

        public static void DisplayConstructors(Type type)
        {
            ConstructorInfo[] constructors = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);

            if (constructors.Length > 0)
            {
                Console.WriteLine("  Constructors:");
            }

            foreach (ConstructorInfo ctor in constructors)
            {
                Console.Write($"    - {type.Name}(");
                DisplayParameters(ctor.GetParameters());
                Console.WriteLine(")");
                DisplayAttributes(ctor.GetCustomAttributes());
            }
        }

        public static void DisplayMethods(Type type)
        {
            MethodInfo[] methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly).Where(m => !m.IsSpecialName).ToArray();

            if (methods.Length > 0)
            {
                Console.WriteLine("  Methods:");
            }

            foreach (MethodInfo method in methods)
            {
                Console.Write($"    - {method.ReturnType.Name} {method.Name}(");
                DisplayParameters(method.GetParameters());
                Console.WriteLine(")");
                DisplayAttributes(method.GetCustomAttributes());
            }
        }

        public static void DisplayParameters(ParameterInfo[] parameters)
        {
            for (int i = 0; i < parameters.Length; i++)
            {
                if (i > 0)
                {
                    Console.Write(", ");
                }

                ParameterInfo param = parameters[i];
                Console.Write($"{param.ParameterType.Name} {param.Name}");
                var paramAttrs = param.GetCustomAttributes();
            }
        }
    }
}