namespace Utils;

public static class Handlers
{
    public static int GetValue(string message, int min, int max)
    {
        int value;
        string input;
        
        do
        {
            ConsoleMethod.NicePrint(message);
            ConsoleMethod.NicePrint($"> value must be in range: [{min}; {max}]:", Color.Primary, " ");
            input = ConsoleMethod.ReadLine();
        } while (!int.TryParse(input, out value));

        return value;
    }
    
    public static double GetValue(string message, double min, double max)
    {
        double value;
        string input;
        
        do
        {
            ConsoleMethod.NicePrint(message);
            ConsoleMethod.NicePrint($"> value must be in range: [{min}; {max}]:", Color.Primary, " ");
            input = ConsoleMethod.ReadLine();
        } while (!double.TryParse(input, out value));

        return value;
    }
}