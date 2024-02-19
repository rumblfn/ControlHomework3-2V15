using System.Text.Json;
using JsonWorkerLib.Models;
using JsonWorkerLib.Models.Interfaces;

namespace JsonWorkerLib;

public class DataManager : ISerializable
{
    private List<Model> _modelsCollection;

    public DataManager()
    {
        _modelsCollection = new List<Model>();
    }
    
    public DataManager(List<Model> models)
    {
        _modelsCollection = models;
    }

    public void Sort()
    {
        
    }

    public string ToJSON()
    {
        return JsonSerializer.Serialize(_modelsCollection);
    }
}