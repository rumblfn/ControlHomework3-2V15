using JsonWorkerApp.Components;
using Utils;

namespace JsonWorkerApp;

/// <summary>
/// Provides menu panels for working with program,
/// </summary>
public static class Templates
{
    /// <summary>
    /// Panel for initializing data.
    /// It provides console and file actions.
    /// </summary>
    /// <returns>Input panel.</returns>
    public static MenuGroup[] DataSelectActionTypePanel()
    {
        return new [] {
            new MenuGroup("Select what you want to do with data.", new MenuItem[]
            {
                new("Sort", ActionType.Sort),
                new("Filter", ActionType.Filter),
                new("Update", ActionType.Update),
                new("Show data", ActionType.Show),
            })
        };
    }
}