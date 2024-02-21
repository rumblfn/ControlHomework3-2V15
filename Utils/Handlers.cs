namespace Utils;

public static class Handlers
{
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