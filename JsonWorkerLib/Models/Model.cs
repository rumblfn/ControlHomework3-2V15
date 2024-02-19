namespace JsonWorkerLib.Models;

/// <summary>
/// Base model for json models.
/// </summary>
public abstract class Model
{
    public event EventHandler<ModelUpdatedEventArgs>? Updated;
    
    /// <summary>
    /// Invokes update handler.
    /// </summary>
    protected void OnUpdated()
    {
        Updated?.Invoke(this, new ModelUpdatedEventArgs());
    }
}