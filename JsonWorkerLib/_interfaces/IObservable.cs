namespace JsonWorkerLib._interfaces;

/// <summary>
/// Interface for observable classes.
/// </summary>
/// <typeparam name="T">Type definition for event handler.</typeparam>
public interface IObservable<T>
{
    protected event EventHandler<T> Updated;
    
    /// <summary>
    /// Subscribe method for handling events.
    /// </summary>
    /// <param name="updateEvent">Event to handle.</param>
    public void Subscribe(EventHandler<T> updateEvent)
    {
        Updated += updateEvent;
    }

    /// <summary>
    /// Calls added events in <see cref="Updated"/>.
    /// </summary>
    protected void OnUpdated();
}