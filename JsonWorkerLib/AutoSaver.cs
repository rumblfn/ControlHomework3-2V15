using JsonWorkerLib.Models.Patient;
using JsonWorkerLib.Models._shared;
using Handlers = Utils.Handlers;

namespace JsonWorkerLib;

/// <summary>
/// Auto saver for all parsed models.
/// </summary>
public class AutoSaver : _interfaces.IObserver<ModelUpdatedEventArgs>
{
    private readonly PatientsRepository _patientsRepository;
    private DateTime _lastUpdateTime = DateTime.MinValue;
    private readonly string _tmpFilePath;

    private const string FileNameEnd = "_tmp.json";

    /// <summary>
    /// Default constructor.
    /// </summary>
    public AutoSaver()
    {
        _patientsRepository = new PatientsRepository();
        _tmpFilePath = FileNameEnd;
    }
    
    /// <summary>
    /// Common constructor.
    /// Converts provided path to right format.
    /// </summary>
    /// <param name="patientsRepository">Repository of patients.</param>
    /// <param name="filePath">Path to file with data.</param>
    public AutoSaver(PatientsRepository patientsRepository, string filePath)
    {
        _patientsRepository = patientsRepository;
        _tmpFilePath = Handlers.GetFilePathWithoutExtension(filePath) + FileNameEnd;
    }

    /// <summary>
    /// Updates time of last event and logs actions.
    /// If interval between two events less or equal 15 saves data to new file
    /// </summary>
    /// <param name="sender">Object that triggered event.</param>
    /// <param name="updateEvent">Event data.</param>
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