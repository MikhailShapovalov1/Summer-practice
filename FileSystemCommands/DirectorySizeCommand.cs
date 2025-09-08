using CommandLib;
using System.IO;

public class DirectorySizeCommand : ICommand
{
    string _directory;
    long _size;
    public long Size => _size;

    public DirectorySizeCommand(string directory)
    {
        _directory = directory;
    }

    public void Execute()
    {
        _size = CalculateDirectorySize(_directory);
    }

    private long CalculateDirectorySize(string directory)
    {
        long size = 0;

        var files = Directory.GetFiles(directory);

        foreach (var file in files)
        {
            size += new FileInfo(file).Length;
        }

        var directories = Directory.GetDirectories(directory);

        foreach (var dir in directories)
        {
            size += CalculateDirectorySize(dir);
        }

        return size;
    }
}
