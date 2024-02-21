using Utils;

namespace JsonWorkerLib;

/// <summary>
/// App logger.
/// </summary>
public static class Logger
{
    private const string FileEnd = "_log.txt";
    
    private static string _filePath = FileEnd;
    public static string FilePath
    {
        get => _filePath;
        set => _filePath = Handlers.GetFilePathWithoutExtension(value) + FileEnd;
    }
    
    /// <summary>
    /// Logs some information in string format with date and time to file.
    /// </summary>
    /// <param name="message">Message that should be logged.</param>
    public static void Info(string message)
    {
        DateTime currentDateTime = DateTime.Now;
        string formattedDateTime = currentDateTime.ToString("dd/MM/yyyy HH:mm:ss");
        WriteToLogFile($"[{formattedDateTime}] {message}{Environment.NewLine}");
    }

    /// <summary>
    /// Append data to existing file or to a new file.
    /// </summary>
    /// <param name="text">Text to write.</param>
    private static void WriteToLogFile(string text)
    {
        try
        {
            File.AppendAllText(FilePath, text);
        }
        catch (Exception ex)
        {
            ConsoleMethod.NicePrint("Something went wrong with adding data to file.");
            ConsoleMethod.NicePrint(ex.Message);
        }
    }
}