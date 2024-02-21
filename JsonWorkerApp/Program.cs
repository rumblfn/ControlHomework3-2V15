using JsonWorkerLib;
using Utils;

namespace JsonWorkerApp;

/// <summary>
/// Starts app and handles it in loop.
/// </summary>
internal static class Program
{
    private const ConsoleKey ExitKey = ConsoleKey.Q;
    
    /// <summary>
    /// Again handler.
    /// </summary>
    /// <returns>Program should restart.</returns>
    private static bool HandleAgain()
    {
        ConsoleMethod.NicePrint($"Press any key to restart or {ExitKey} to finish program.");
        return ConsoleMethod.ReadKey() != ExitKey;
    }
    
    /// <summary>
    /// Runs program body in loop and handles errors.
    /// </summary>
    private static void Main()
    {
        ConsoleMethod.NicePrint("App started.");
        
        do
        {
            try
            {
                ConsoleMethod.NicePrint("> Enter path to json data", Color.Condition);
                string path = ConsoleMethod.ReadLine();

                // Update log path.
                Logger.FilePath = path;
                
                // Start worker.
                var worker = new JsonWorker(path);
                worker.Run();
            }
            catch (Exception e)
            {
                ConsoleMethod.NicePrint("Something went wrong. Try again.");
                ConsoleMethod.NicePrint(e.Message);
            }
        } while (HandleAgain());
        
        ConsoleMethod.NicePrint("App finished.");
    }
}