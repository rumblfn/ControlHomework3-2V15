namespace JsonWorkerLib.Models;

/// <summary>
/// Base model for json models.
/// </summary>
public abstract class Model : _interfaces.IObservable<ModelUpdatedEventArgs>
{
    public event EventHandler<ModelUpdatedEventArgs>? Updated;
    
    public void Subscribe(EventHandler<ModelUpdatedEventArgs> updateEvent)
    {
        Updated += updateEvent;
    }
    
    public void OnUpdated()
    {
        Updated?.Invoke(this, new ModelUpdatedEventArgs());
    }
}