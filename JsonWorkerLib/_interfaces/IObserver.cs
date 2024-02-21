namespace JsonWorkerLib._interfaces;

/// <summary>
/// Interface for observer classes.
/// </summary>
/// <typeparam name="T">Type definition for update event.</typeparam>
public interface IObserver<in T>
{
    /// <summary>
    /// Triggers by observable class and handles logic.
    /// </summary>
    /// <param name="sender">Observable class.</param>
    /// <param name="updateEvent">Event data.</param>
    void Update(object? sender, T updateEvent);
}