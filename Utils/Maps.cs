namespace Utils;

/// <summary>
/// Class for program maps.
/// </summary>
public static class Maps
{
    private static readonly Dictionary<Color, ConsoleColor> ColorMap = new ()
    {
        { Color.Secondary,  ConsoleColor.DarkCyan },
        { Color.Condition,  ConsoleColor.Yellow   },
        { Color.Disabled,   ConsoleColor.DarkBlue },
        { Color.Primary,    ConsoleColor.Cyan     },
        { Color.Error,      ConsoleColor.Red      },
    };

    /// <summary>
    /// Get color specified by enum type <see cref="Color"/>.
    /// </summary>
    /// <param name="color">Color.</param>
    /// <returns>ConsoleColor.</returns>
    public static ConsoleColor GetColor(Color color)
    {
        return ColorMap.GetValueOrDefault(color, ConsoleColor.White);
    }
}