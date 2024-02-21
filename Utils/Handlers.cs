namespace Utils;

public static class Handlers
{
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