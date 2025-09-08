using CommandLib;

public class FindFilesCommand : ICommand
{
    private string _directoryPath;
    private string _mask;
    private string[] _foundFiles = new string[0];
    public string[] FoundFiles => _foundFiles;

    public FindFilesCommand(string directoryPath, string mask)
    {
        _directoryPath = directoryPath;
        _mask = mask;
    }

    public void Execute()
    {
        _foundFiles = Directory.GetFiles(_directoryPath, _mask);
    }
}
