using System.Text.Json;
using JsonWorkerLib.Models;

namespace JsonWorkerLib;

public class AutoSaver : Models._interfaces.IObserver<ModelUpdatedEventArgs>
{
    private readonly List<Model> _modelsCollection = new ();
    private DateTime _lastUpdateTime = DateTime.MinValue;
    private readonly string _originalJsonFilePathWithoutExtension;

    public AutoSaver()
    {
        _originalJsonFilePathWithoutExtension = string.Empty;
    }
    
    public AutoSaver(string originalJsonFilePath)
    {
        _originalJsonFilePathWithoutExtension = Path.GetFileNameWithoutExtension(originalJsonFilePath);
    }

    public void Update(object? sender, ModelUpdatedEventArgs updateEvent)
    {
        if ((updateEvent.UpdateDateTime - _lastUpdateTime).TotalSeconds <= 15)
        {
            SaveToJson();
        }
        
        _lastUpdateTime = updateEvent.UpdateDateTime;
    }

    private void SaveToJson()
    {
        string tmpFileName = _originalJsonFilePathWithoutExtension + "_tmp.json";

        var serializerOptions = new JsonSerializerOptions { WriteIndented = true };
        string jsonData = JsonSerializer.Serialize(_modelsCollection, serializerOptions);

        File.WriteAllText(tmpFileName, jsonData);
    }
}