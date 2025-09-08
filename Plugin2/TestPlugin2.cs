using PluginLoader;

namespace Plugin2
{
    [PluginLoad("TestPlugin1")]
    public class TestPlugin2 : IPlugin
    {
        public void Execute()
        {
            Console.WriteLine("Plugin 2 execute");
        }
    }
}
