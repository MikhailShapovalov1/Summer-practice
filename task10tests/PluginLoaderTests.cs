using PluginLoader;
using Plugin1;
using Plugin2;
using Plugin3;

namespace task10tests
{
    public class PluginLoaderTests
    {
        [Fact]
        public void RunWithEmptyInput_WritesEmptyDirectory()
        {
            var input = new StringReader("");
            Console.SetIn(input);
            var output = new StringWriter();
            Console.SetOut(output);

            PluginsLoader.Main();

            Assert.Contains("Empty directory", output.ToString());
        }

        [Fact]
        public void TopologicalSort_ReturnsCorrectOrder()
        {
            var pluginTypes = new List<Type> {
                typeof(TestPlugin3),
                typeof(TestPlugin2),
                typeof(TestPlugin1)
            };

            var result = PluginsLoader.TopologicalSort(pluginTypes);

            Assert.Equal(3, result.Count);
            Assert.Equal(typeof(TestPlugin1), result[0]);
            Assert.Equal(typeof(TestPlugin2), result[1]);
            Assert.Equal(typeof(TestPlugin3), result[2]);
        }


        [Fact]
        public void LoadAndExecutePlugins_LoadsAndExecutesPlugins()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            var directory = Path.Combine(Environment.CurrentDirectory.Substring(0, Environment.CurrentDirectory.IndexOf("bin")), "Test DLL files");
            PluginsLoader.LoadAndExecutePlugins(directory);

            Assert.Contains("Plugin 1 execute", output.ToString());
            Assert.Contains("Plugin 2 execute", output.ToString());
            Assert.Contains("Plugin 3 execute", output.ToString());
        }
    }
}
