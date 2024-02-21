namespace JsonWorkerLib._interfaces;

public interface IObservable<T>
{
    protected event EventHandler<T> Updated;
    
    public void Subscribe(EventHandler<T> updateEvent)
    {
        Updated += updateEvent;
    }

    protected void OnUpdated();
}