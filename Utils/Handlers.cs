namespace Utils;

/// <summary>
/// Static handlers.
/// </summary>
public static class Handlers
{
    /// <summary>
    /// Sum of path to directory and file name without extension.
    /// </summary>
    /// <param name="path">Path to handle.</param>
    /// <returns>Full path without extension.</returns>
    public static string GetFilePathWithoutExtension(string path)
    {
        try
        {
            return Path.GetDirectoryName(path) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(path);
        }
        catch (PathTooLongException)
        {
            ConsoleMethod.NicePrint("Path to long. Try again.");
        }
        catch (ArgumentException)
        {
            ConsoleMethod.NicePrint(
                "The path parameter contains invalid characters, is empty, or contains only white spaces.");
        }

        return string.Empty;
    }
    
    /// <summary>
    /// Handles number input in loop with min available value.
    /// </summary>
    /// <param name="min">Minimum available value.</param>
    /// <returns>Read integer number.</returns>
    public static int GetValue(int min)
    {
        int value;
        string input;
        
        do
        {
            ConsoleMethod.NicePrint($"> value must be grater or equal {min}:", Color.Primary, " ");
            input = ConsoleMethod.ReadLine();
        } while (!int.TryParse(input, out value) || value < min);

        return value;
    }
    
    /// <summary>
    /// Same as <see cref="GetValue(int)"/> but with value in double.
    /// </summary>
    /// <param name="min">Minimum available value.</param>
    /// <returns>Read double value.</returns>
    public static double GetValue(double min)
    {
        double value;
        string input;
        
        ConsoleMethod.NicePrint("Use \",\" to enter float value.");
        do
        {
            ConsoleMethod.NicePrint($"> value must be grater or equal {min}:", Color.Primary, " ");
            input = ConsoleMethod.ReadLine();
        } while (!double.TryParse(input, out value) || value < min);

        return value;
    }
}