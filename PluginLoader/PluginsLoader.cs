using System.Reflection;

namespace PluginLoader
{
    public class PluginsLoader
    {
        public static void Main()
        {
            string? pluginsDirectory = Console.ReadLine();

            if (string.IsNullOrEmpty(pluginsDirectory))
            {
                Console.WriteLine("Empty directory");
            }
            else
            {
                LoadAndExecutePlugins(pluginsDirectory);
            }
        }

        public static void LoadAndExecutePlugins(string pluginsDirectory)
        {
            var dllFiles = Directory.GetFiles(pluginsDirectory, "*.dll", SearchOption.AllDirectories);
            var assemblies = new List<Assembly>();

            foreach (var dll in dllFiles)
            {
                var assembly = Assembly.LoadFrom(dll);
                assemblies.Add(assembly);
            }

            var pluginTypes = new List<Type>();
            foreach (var assembly in assemblies)
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (type.GetCustomAttribute<PluginLoadAttribute>() != null)
                    {
                        pluginTypes.Add(type);
                    }
                }
            }

            var sortedPlugins = Sort(pluginTypes);
            var pluginInstances = new Dictionary<string, IPlugin>();

            foreach (var type in sortedPlugins)
            {
                var instance = (IPlugin)Activator.CreateInstance(type)!;
                pluginInstances[type.FullName!] = instance;
                instance.Execute();
            }
        }

        public static List<Type> Sort(List<Type> pluginTypes)
        {
            var graph = new Dictionary<Type, List<Type>>();
            var inDegree = new Dictionary<Type, int>();

            foreach (var type in pluginTypes)
            {
                graph[type] = new List<Type>();
                inDegree[type] = 0;
            }

            foreach (var type in pluginTypes)
            {
                var attr = type.GetCustomAttribute<PluginLoadAttribute>();

                if (attr != null)
                {
                    foreach (var depName in attr.Dependencies)
                    {
                        var dependency = pluginTypes.FirstOrDefault(t => t.Name == depName);
                        if (dependency != null)
                        {
                            graph[dependency].Add(type);
                            inDegree[type]++;
                        }
                    }
                }
            }

            var queue = new Queue<Type>();
            var result = new List<Type>();

            foreach (var type in pluginTypes)
            {
                if (inDegree[type] == 0)
                {
                    queue.Enqueue(type);
                }
            }

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                result.Add(current);

                foreach (var neighbor in graph[current])
                {
                    inDegree[neighbor]--;
                    if (inDegree[neighbor] == 0)
                    {
                        queue.Enqueue(neighbor);
                    }
                }
            }

            return result;
        }
    }
}