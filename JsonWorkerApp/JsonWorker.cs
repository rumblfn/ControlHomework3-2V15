using JsonWorkerLib.Models.Patient;
using JsonWorkerLib.Models.Doctor;
using System.Text.Json;
using JsonWorkerLib;

namespace JsonWorkerApp;

/// <summary>
/// Handles working with json file.
/// </summary>
internal class JsonWorker
{
    private readonly PatientsRepository _patientsRepository;
    private readonly AutoSaver _autoSaver;

    /// <summary>
    /// Reads data. Creates auto saver with read data.
    /// Subscribes auto saver to all models and doctors to their patients.
    /// </summary>
    /// <param name="path">Path to file with data in json format.</param>
    public JsonWorker(string path)
    {
        _patientsRepository = HandleReadData(path);
        _autoSaver = new AutoSaver(_patientsRepository, path);
        SubscribeAllModels();
    }

    /// <summary>
    /// Reads data to patients repository.
    /// </summary>
    /// <param name="path">Path to data in json format.</param>
    /// <returns></returns>
    /// <exception cref="Exception">Data not provided or is empty.</exception>
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

    /// <summary>
    /// Subscribes auto saver to all models and
    /// subscribes doctors to their patients.
    /// </summary>
    private void SubscribeAllModels()
    {
        // To avoid excess auto savers subscriptions.
        var uniqDoctorsIds = new HashSet<int>();
        
        foreach (Patient patient in _patientsRepository)
        {
            // One patient can has duplicated doctors.
            var uniqPatientDoctorsIds = new HashSet<int>();
            
            patient.Subscribe(_autoSaver.Update);
            foreach (Doctor doctor in patient.Doctors)
            {
                if (uniqPatientDoctorsIds.Add(doctor.DoctorId))
                {
                    patient.Subscribe(doctor.Update);
                }
                if (uniqDoctorsIds.Add(doctor.DoctorId))
                {
                    doctor.Subscribe(_autoSaver.Update);
                }
            }
        }
    }
    
    /// <summary>
    /// Runs menu interface to work with parsed data.
    /// </summary>
    public void Run()
    {
        var templatesScript = new TemplatesScript(_patientsRepository);
        templatesScript.HandleActionPanel();
    }
}