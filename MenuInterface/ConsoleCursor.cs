namespace MenuInterface;

/// <summary>
/// Cursor for interactive menu.
/// </summary>
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

    /// <summary>
    /// Restores cursor position with saved.
    /// </summary>
    public void RestorePosition()
    {
        Console.SetCursorPosition(_currentCursorColumnIndex, _currentCursorRowIndex);
    }
}