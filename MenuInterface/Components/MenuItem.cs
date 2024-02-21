using Utils;

namespace MenuInterface.Components;

/// <summary>
/// Menu item to select.
/// </summary>
public class MenuItem
{
    private string Name { get; }
    public readonly Action? Action;
    public bool Selected = false;
    public int ExpectedLength { get; set; }

    /// <summary>
    /// Initialization.
    /// </summary>
    /// <param name="name">Element name.</param>
    /// <param name="action">The method being called.</param>
    public MenuItem(string name, Action? action)
    {
        Name = name;
        Action = action;
    }

    /// <summary>
    /// Converts item to writeable view.
    /// </summary>
    /// <returns>Item in string format.</returns>
    public override string ToString()
    {
        return Selected ? " >" + Name + "<" : " -" + Name + "-";
    }

    /// <summary>
    /// Responsible for the output of the menu item.
    /// </summary>
    public void Write()
    {
        string itemString = ToString();
        int needLength = ExpectedLength - itemString.Length;
        string needString = needLength > 0 ? new string(' ', needLength) : string.Empty;
        
        ConsoleMethod.NicePrint(ToString() + needString,
            Action is null ? Color.Disabled : Selected ? Color.Condition : Color.Secondary, "");
    }
}