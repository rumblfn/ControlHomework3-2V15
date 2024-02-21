namespace JsonWorkerLib.Models;

public class ModelUpdatedEventArgs : EventArgs
{
    public DateTime UpdateDateTime { get; } = DateTime.Now;
}