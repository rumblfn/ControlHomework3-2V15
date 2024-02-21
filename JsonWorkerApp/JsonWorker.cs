using JsonWorkerLib.Models.Patient;
using JsonWorkerLib.Models.Doctor;
using System.Text.Json;
using JsonWorkerLib;

namespace JsonWorkerApp;

internal class JsonWorker
{
    private readonly PatientsRepository _patientsRepository;
    private readonly AutoSaver _autoSaver;

    public JsonWorker(string path)
    {
        _patientsRepository = HandleReadData(path);
        _autoSaver = new AutoSaver(_patientsRepository, path);
        SubscribeAllModels();
    }

    private static PatientsRepository HandleReadData(string path)
    {
        string jsonString = File.ReadAllText(path);
        var data = JsonSerializer.Deserialize<List<Patient>>(jsonString);

        if (data is null)
        {
            throw new Exception("Data not provided");
        }

        if (data.Count == 0)
        {
            throw new Exception("Empty collection of data.");
        }

        return new PatientsRepository(data);
    }

    private void SubscribeAllModels()
    {
        foreach (Patient patient in _patientsRepository)
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
        var templatesScript = new TemplatesScript(_patientsRepository);
        templatesScript.HandleActionPanel();
    }
}