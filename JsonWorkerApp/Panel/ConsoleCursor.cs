namespace JsonWorkerApp.Panel;

public class ConsoleCursor
{
    private int _currentCursorRowIndex;
    private int _currentCursorColumnIndex;

    public ConsoleCursor()
    {
        UpdateCursorPosition();
    }
    
    /// <summary>
    /// Updates cursor position of the console.
    /// </summary>
    public void UpdateCursorPosition()
    {
        _currentCursorRowIndex = Console.CursorTop;
        _currentCursorColumnIndex = Console.CursorLeft;
    }

    public void RestorePosition()
    {
        Console.SetCursorPosition(_currentCursorColumnIndex, _currentCursorRowIndex);
    }
}