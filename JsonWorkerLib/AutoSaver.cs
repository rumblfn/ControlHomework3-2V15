using JsonWorkerLib.Models.Patient;
using JsonWorkerLib.Models._shared;
using Handlers = Utils.Handlers;

namespace JsonWorkerLib;

public class AutoSaver : _interfaces.IObserver<ModelUpdatedEventArgs>
{
    private readonly PatientsRepository _patientsRepository;
    private DateTime _lastUpdateTime = DateTime.MinValue;
    private readonly string _tmpFilePath;

    private const string FileNameEnd = "_tmp.json";

    public AutoSaver()
    {
        _patientsRepository = new PatientsRepository();
        _tmpFilePath = FileNameEnd;
    }
    
    public AutoSaver(PatientsRepository patientsRepository, string filePath)
    {
        _patientsRepository = patientsRepository;
        _tmpFilePath = Handlers.GetFilePathWithoutExtension(filePath) + FileNameEnd;
    }

    public void Update(object? sender, ModelUpdatedEventArgs updateEvent)
    {
        if ((updateEvent.UpdateDateTime - _lastUpdateTime).TotalSeconds <= 15)
        {
            File.WriteAllText(_tmpFilePath, _patientsRepository.ToJson());
            Logger.Info("AutoSaver: The data has been saved successfully.");
        }
        else
        {
            Logger.Info("AutoSaver: It's been more than 15 seconds. Data not saved.");
        }
        
        _lastUpdateTime = updateEvent.UpdateDateTime;
    }
}