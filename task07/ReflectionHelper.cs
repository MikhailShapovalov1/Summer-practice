using System.Reflection;
using CommandLib;

namespace CommandRunner
{
    public class Program
    {
        public static void Main()
        {
            var testDir = Path.Combine(Path.GetTempPath(), "TestDir");
            Directory.CreateDirectory(testDir);

            File.WriteAllText(Path.Combine(testDir, "file1.txt"), "Text");
            File.WriteAllText(Path.Combine(testDir, "file2.log"), "Log");

            Assembly assembly = Assembly.LoadFrom("FileSystemCommands.dll");

            var commandTypes = assembly.GetTypes().Where(t => typeof(ICommand).IsAssignableFrom(t));
            string mask = "*.txt";
            object[] args = [testDir, mask];

            foreach (var commandType in commandTypes)
            {
                ICommand command;

                if (commandType.Name == "DirectorySizeCommand")
                {
                    command = (ICommand)Activator.CreateInstance(commandType, args[0])!;
                    command.Execute();
                    Console.WriteLine(((DirectorySizeCommand)command).Size);
                }
                else if (commandType.Name == "FindFilesCommand")
                {
                    command = (ICommand)Activator.CreateInstance(commandType, args)!;
                    command.Execute();

                    string[] foundFiles = ((FindFilesCommand)command).FoundFiles;

                    foreach (var foundFile in foundFiles)
                    {
                        Console.WriteLine(foundFile);
                    }
                }
                else
                {
                    throw new NotImplementedException();
                }
            }

            Directory.Delete(testDir, true);
        }
    }
}