using System.Text.Json;
using System.Collections;
using JsonWorkerLib._interfaces;

namespace JsonWorkerLib.Abstractions;

/// <summary>
/// Abstraction for specified type.
/// Provides methods to work with collection of data.
/// </summary>
/// <typeparam name="TCollection">Type of data in collection.</typeparam>
public abstract class Repository<TCollection> : ISerializable, IEnumerable<TCollection>
{
    public List<TCollection> Collection
    {
        get;
        private set;
    }
    
    /// <summary>
    /// Store provided data in list.
    /// </summary>
    /// <param name="collection">List with specified type.</param>
    protected Repository(List<TCollection> collection)
    {
        Collection = collection;
    }
    
    /// <summary>
    /// Method for OrderBy in LINQ syntax.
    /// </summary>
    /// <param name="keySelector">Key selector for LINQ.</param>
    /// <typeparam name="TKey">Type of returning data.</typeparam>
    public void OrderBy<TKey>(Func<TCollection,TKey> keySelector)
    {
        Collection = Collection.OrderBy(keySelector).ToList();
    }

    public string ToJson()
    {
        var serializerOptions = new JsonSerializerOptions { WriteIndented = true };
        return JsonSerializer.Serialize(Collection, serializerOptions);
    }

    public IEnumerator<TCollection> GetEnumerator()
    {
        return Collection.GetEnumerator();
    }

    /// <summary>
    /// Allows use iteration.
    /// </summary>
    /// <returns>Collection of data.</returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public TCollection this[int index] => Collection[index];
}