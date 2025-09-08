using PluginLoader;

namespace Plugin3
{
    [PluginLoad("TestPlugin1", "TestPlugin2")]
    public class TestPlugin3 : IPlugin
    {
        public void Execute()
        {
            Console.WriteLine("Plugin 3 execute");
        }
    }
}
