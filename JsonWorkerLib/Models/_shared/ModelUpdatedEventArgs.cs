namespace JsonWorkerLib.Models._shared;

/// <summary>
/// Event data for auto saver notify event.
/// </summary>
public class ModelUpdatedEventArgs : EventArgs
{
    public DateTime UpdateDateTime { get; } = DateTime.Now;
}