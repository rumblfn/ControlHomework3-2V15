using Utils;
using JsonWorkerLib;
using System.Text.Json;
using JsonWorkerLib.Models.Doctor;
using JsonWorkerLib.Models.Patient;

namespace JsonWorkerApp;

internal class JsonWorker
{
    private string _filePath = string.Empty;
    private PatientsRepository _patientsRepository = new();
    private AutoSaver _autoSaver = new();
    
    private void HandlePathInput()
    {
        ConsoleMethod.NicePrint("> Enter path to json data", Color.Condition);
        _filePath = ConsoleMethod.ReadLine();
    }

    private void HandleReadData()
    {
        string jsonString = File.ReadAllText(_filePath);
        var data = JsonSerializer.Deserialize<List<Patient>>(jsonString);

        if (data is null)
        {
            throw new Exception("Data not provided");
        }

        if (data.Count == 0)
        {
            throw new Exception("Empty collection of data.");
        }

        _patientsRepository = new PatientsRepository(data);
    }

    private void SubscribeAllModels()
    {
        foreach (Patient patient in _patientsRepository.Collection)
        {
            patient.Subscribe(_autoSaver.Update);
            foreach (Doctor doctor in patient.Doctors)
            {
                doctor.Subscribe(_autoSaver.Update);
                patient.Subscribe(doctor.Update);
            }
        }
    }
    
    public void Run()
    {
        HandlePathInput();
        _autoSaver = new AutoSaver(_filePath);
        
        HandleReadData();
        SubscribeAllModels();

        var templatesScript = new TemplatesScript(_patientsRepository);
        templatesScript.HandleActionPanel();
    }
}