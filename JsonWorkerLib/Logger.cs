using Utils;

namespace JsonWorkerLib;

public static class Logger
{
    private const string FileEnd = "_log.txt";
    
    private static string _filePath = FileEnd;
    public static string FilePath
    {
        get => _filePath;
        set => _filePath = Handlers.GetFilePathWithoutExtension(value) + FileEnd;
    }
    
    public static void Info(string message)
    {
        DateTime currentDateTime = DateTime.Now;
        string formattedDateTime = currentDateTime.ToString("dd/MM/yyyy HH:mm:ss");
        WriteToLogFile($"[{formattedDateTime}] {message}{Environment.NewLine}");
    }

    private static void WriteToLogFile(string text)
    {
        try
        {
            File.AppendAllText(FilePath, text);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}