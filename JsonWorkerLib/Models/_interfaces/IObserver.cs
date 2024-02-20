namespace JsonWorkerLib.Models._interfaces;

public interface IObserver<in T>
{
    void Update(object? sender, T updateEvent);
}