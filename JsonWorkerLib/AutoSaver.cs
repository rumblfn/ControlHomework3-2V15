using JsonWorkerLib.Models;
using JsonWorkerLib.Models.Patient;
using Utils;

namespace JsonWorkerLib;

public class AutoSaver : Models._interfaces.IObserver<ModelUpdatedEventArgs>
{
    private readonly PatientsRepository _patientsRepository = new ();
    private DateTime _lastUpdateTime = DateTime.MinValue;
    private readonly string _fileNameWithoutExtension;

    public AutoSaver()
    {
        _fileNameWithoutExtension = string.Empty;
    }
    
    public AutoSaver(PatientsRepository patientsRepository, string filePath)
    {
        _patientsRepository = patientsRepository;
        _fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
    }

    public void Update(object? sender, ModelUpdatedEventArgs updateEvent)
    {
        ConsoleMethod.NicePrint("AutoSaver called.");
        
        if ((updateEvent.UpdateDateTime - _lastUpdateTime).TotalSeconds <= 15)
        {
            ConsoleMethod.NicePrint("Trying to save new data.");
            SaveToJson();
        }
        
        _lastUpdateTime = updateEvent.UpdateDateTime;
        ConsoleMethod.NicePrint("It's been more than 15 seconds. Data not saved.");
    }

    private void SaveToJson()
    {
        string tmpFileName = _fileNameWithoutExtension + "_tmp.json";
        
        File.WriteAllText(tmpFileName, _patientsRepository.ToJson());
        ConsoleMethod.NicePrint("The data has been saved successfully.");
    }
}