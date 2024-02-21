using System.Text.Json;
using System.Collections;
using JsonWorkerLib._interfaces;

namespace JsonWorkerLib.Abstractions;

public abstract class Repository<TCollection> : ISerializable, IEnumerable<TCollection>
{
    public List<TCollection> Collection
    {
        get;
        private set;
    }
    
    protected Repository(List<TCollection> collection)
    {
        Collection = collection;
    }
    
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

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public TCollection this[int index] => Collection[index];
}