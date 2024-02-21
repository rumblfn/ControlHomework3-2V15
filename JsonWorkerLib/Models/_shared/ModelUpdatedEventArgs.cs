namespace JsonWorkerLib.Models._shared;

public class ModelUpdatedEventArgs : EventArgs
{
    public DateTime UpdateDateTime { get; } = DateTime.Now;
}