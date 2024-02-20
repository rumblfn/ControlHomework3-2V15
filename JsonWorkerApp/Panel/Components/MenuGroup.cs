using Utils;

namespace JsonWorkerApp.Panel.Components;

/// <summary>
/// Menu item to select.
/// </summary>
public class MenuGroup
{
    private string Name { get; }
    public MenuItem[] Items { get; }
    public int ExpectedLength { get; set; }

    /// <summary>
    /// Initialization.
    /// </summary>
    /// <param name="name">Group name.</param>
    /// <param name="items">Items in Group.</param>
    public MenuGroup(string name, MenuItem[] items)
    {
        Name = name;
        Items = items;
    }
    
    /// <summary>
    /// Converts group name to writeable view.
    /// </summary>
    /// <returns>Group name in string format.</returns>
    public override string ToString()
    {
        return Name + ":";
    }
    
    /// <summary>
    /// Responsible for the output of the menu group.
    /// </summary>
    public void Write()
    {
        ConsoleMethod.NicePrint(Items.Any(item => item.Selected) ? "?" : " ", 
            Color.Primary, " ");

        string itemString = ToString();
        int needLength = ExpectedLength - itemString.Length;
        string needString = needLength > 0 ? new string(' ', needLength) : string.Empty;
        ConsoleMethod.NicePrint(ToString() + needString, Color.Primary, "");
        
        foreach (MenuItem item in Items)
        {
            item.Write();
        }
        Console.WriteLine();
    }

    public MenuItem this[int index] => Items[index];
}