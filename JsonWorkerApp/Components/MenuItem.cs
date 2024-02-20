using Utils;

namespace JsonWorkerApp.Components;

/// <summary>
/// Menu item to select.
/// </summary>
public class MenuItem
{
    private string Name { get; }
    public readonly Action Action;
    public bool Selected = false;

    /// <summary>
    /// Initialization.
    /// </summary>
    /// <param name="name">Element name.</param>
    /// <param name="action">The method being called.</param>
    public MenuItem(string name, Action action)
    {
        Name = name;
        Action = action;
    }

    /// <summary>
    /// Responsible for the output of the menu item.
    /// </summary>
    public void Write()
    {
        ConsoleMethod.NicePrint(" -" + Name + ";", 
            Selected ? Color.Condition : Color.Secondary, "");
    }
}