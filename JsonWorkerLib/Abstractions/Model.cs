using JsonWorkerLib.Models._shared;

namespace JsonWorkerLib.Abstractions;

/// <summary>
/// Base model for json models.
/// </summary>
public abstract class Model : JsonWorkerLib._interfaces.IObservable<ModelUpdatedEventArgs>
{
    public event EventHandler<ModelUpdatedEventArgs>? Updated;
    
    public void Subscribe(EventHandler<ModelUpdatedEventArgs> updateEvent)
    {
        Updated += updateEvent;
    }
    
    public void OnUpdated()
    {
        Logger.Info($"Model listeners count: {Updated?.GetInvocationList().Length}");
        Updated?.Invoke(this, new ModelUpdatedEventArgs());
    }
}