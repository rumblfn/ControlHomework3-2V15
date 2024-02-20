using JsonWorkerApp.Panel.Components;
using Utils;

namespace JsonWorkerApp.Panel;

/// <summary>
/// Panel menu (task manager) for working with data.
/// </summary>
internal class DataPanel
{
    private bool _toExit;
    private readonly ConsoleCursor _consoleCursor;
    private readonly MenuTable _menuTable;
    private bool _elementSelected;

    /// <summary>
    /// Initialization.
    /// </summary>
    /// <param name="menuTable">Panel items.</param>
    public DataPanel(MenuTable menuTable)
    {
        _menuTable = menuTable;
        _consoleCursor = new ConsoleCursor();
    }

    /// <summary>
    /// Updates the indexes of the selected group and item.
    /// </summary>
    private void HandleKeys()
    {
        ConsoleKey pressedButtonKey = ConsoleMethod.ReadKey();
        (int rowIndex, int columnIndex) = _menuTable.GetSelectedItemIndexes();
        
        switch (pressedButtonKey)
        {
            case ConsoleKey.DownArrow:
                rowIndex++;
                break;
            case ConsoleKey.UpArrow:
                rowIndex--;
                break;
            case ConsoleKey.LeftArrow:
                columnIndex--;
                break;
            case ConsoleKey.RightArrow:
                columnIndex++;
                break;
            case ConsoleKey.Q:
                _toExit = true;
                break;
            case ConsoleKey.Enter:
                _elementSelected = true;
                _toExit = true;
                return;
            default:
                return;
        }
        
        _menuTable.UpdateSelectedItem(rowIndex, columnIndex);
    }
    
    /// <summary>
    /// Panel runner.
    /// </summary>
    public void Run(string title)
    {
        Console.Clear();
        ConsoleMethod.NicePrint(title, Color.Primary);
        _consoleCursor.UpdateCursorPosition();
        
        while (!_toExit)
        {
            DrawPanel();
            HandleKeys();

            if (!_elementSelected)
            {
                continue;
            }
            
            MenuItem selectedItem = _menuTable.GetSelectedItem();
            
            if (selectedItem.Action is null)
            {
                _toExit = false;
                _elementSelected = false;
            }
            else
            {
                selectedItem.Action();
            }
        }
    }
    
    /// <summary>
    /// Displays the panel on the screen.
    /// </summary>
    private void DrawPanel()
    {
        _consoleCursor.RestorePosition();
        _menuTable.Write();
        ConsoleMethod.NicePrint("Q to exit.");
    }
}