using JsonWorkerLib.Models.Patient;
using JsonWorkerLib.Models.Doctor;
using MenuInterface.Components;
using System.Globalization;
using MenuInterface;
using Utils;
using Handlers = Utils.Handlers;

namespace JsonWorkerApp;

/// <summary>
/// Provides menu panels for managing patients and doctors data.
/// It allows sorting the patient data based on different fields
/// and updating the information of patients and doctors.
/// </summary>
public class TemplatesScript
{
    private readonly PatientsRepository _patientsRepository;
    
    /// <summary>
    /// Provides instance of the PatientsRepository class
    /// used to store and retrieve patient data.
    /// </summary>
    /// <param name="patientsRepository">PatientsRepository.</param>
    public TemplatesScript(PatientsRepository patientsRepository)
    {
        _patientsRepository = patientsRepository;
    }
    
    /// <summary>
    /// Sorts the patient data based on the specified field.
    /// </summary>
    /// <param name="keySelector">Key selector in LINQ syntax.</param>
    /// <param name="fieldName">Humanized sorting field.</param>
    /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
    private void Sort<TKey>(Func<Patient,TKey> keySelector, string fieldName)
    {
        _patientsRepository.OrderBy(keySelector);
        ConsoleMethod.NicePrint($"Sort by {fieldName} completed.");
    }

    /// <summary>
    /// Panel for sorting data.
    /// Creates menu interface and runs it.
    /// </summary>
    private void HandleSortPanel()
    {
        var groups = new MenuGroup[] 
        {
            new ("About", new MenuItem[]
            {
                new("Patient id", () => Sort(patient => patient.PatientId, "patient id")),
                new("Age", () => Sort(patient => patient.Age, "patient age")),
                new("Name", () => Sort(patient => patient.Name, "patient name")),
                new("Gender", () => Sort(patient => patient.Gender, "patient gender")),
            }),
            new ("Editable", new MenuItem[]
            {
                new("Heart rate", 
                    () => Sort(patient => patient.HeartRate, "patient heart rate")),
                new("Temperature", 
                    () => Sort(patient => patient.Temperature, "patient temperature")),
                new("Oxygen Saturation", () => Sort(
                    patient => patient.OxygenSaturation, "patient oxygen saturation")),
            }),
            new ("Other", new MenuItem[]
            {
                new("Diagnosis", 
                    () => Sort(patient => patient.Diagnosis, "patient diagnosis")),
            }),
        };
        
        var dp = new DataPanel(new MenuTable(groups));
        dp.Run("Select sort field");
        HandleActionPanel();
    }

    /// <summary>
    /// Handles the select patient panel, allowing the user to select a patient to update.
    /// </summary>
    private void HandleSelectPatientPanel()
    {
        var groups = new List<MenuGroup>();

        int rowsCount = (int)Math.Ceiling(_patientsRepository.Collection.Count / 5.0);
        for (int i = 0; i < rowsCount; i++)
        {
            var items = new List<MenuItem>();

            int maxColumnNumber = Math.Min(i * 5 + 5, _patientsRepository.Collection.Count);
            for (int j = i * 5; j < maxColumnNumber; j++)
            {
                Patient patient = _patientsRepository[j];
                items.Add(new MenuItem($"{patient.PatientId} {patient.Name}", 
                    () => HandleUpdatePatientPanel(patient)));
            }

            if (items.Count > 0)
            {
                groups.Add(new MenuGroup("", items.ToArray()));
            }
        }
        
        var dp = new DataPanel(new MenuTable(groups.ToArray()));
        dp.Run("Select patient that you want update by id");
        HandleActionPanel();
    }

    /// <summary>
    /// Handles the update doctor panel, allowing update fields of a selected doctor.
    /// </summary>
    /// <param name="doctor">Selected doctor.</param>
    private static void HandleUpdateDoctorPanel(Doctor doctor)
    {
        var groups = new MenuGroup[] 
        {
            new ("Id", new MenuItem[] {new(doctor.DoctorId.ToString(), null)}),
            new ("Name", new MenuItem[]
            {
                new(doctor.Name, () =>
                {
                    ConsoleMethod.NicePrint("> Enter new doctor name: ");
                    doctor.Name = ConsoleMethod.ReadLine();
                })
            }),
            new ("Appointment count", new MenuItem[]
            {
                new(doctor.AppointmentCount.ToString(), null),
            }),
        };
        
        var dp = new DataPanel(new MenuTable(groups));
        dp.Run($"Selected doctor: {doctor.DoctorId}. Select field that you want to edit.");
    }

    /// <summary>
    /// Handles the update patient panel,
    /// allowing the user to update the information of a selected patient.
    /// </summary>
    /// <param name="patient">Selected patient.</param>
    private static void HandleUpdatePatientPanel(Patient patient)
    {
        MenuItem[] doctorsItems = patient.Doctors.Select(
            doctor => new MenuItem($"{doctor.DoctorId} {doctor.Name}", 
                () => HandleUpdateDoctorPanel(doctor))).ToArray();
        
        var groups = new MenuGroup[] 
        {
            new ("Id", new MenuItem[] { new(patient.PatientId.ToString(), null), }),
            new ("Name", new MenuItem[]
            {
                new(patient.Name, () =>
                {
                    ConsoleMethod.NicePrint("> Enter new patient name:");
                    patient.Name = ConsoleMethod.ReadLine();
                }),
            }),
            new ("Age", new MenuItem[]
            {
                new(patient.Age.ToString(), () =>
                {
                    ConsoleMethod.NicePrint("> Enter new patient age:");
                    patient.Age = Handlers.GetValue(0);
                }),
            }),
            new ("Gender", new MenuItem[]
            {
                new(patient.Gender, () =>
                {
                    ConsoleMethod.NicePrint("> Enter new patient gender:");
                    patient.Gender = ConsoleMethod.ReadLine();
                }),
            }),
            new ("Diagnosis", new MenuItem[]
            {
                new(patient.Diagnosis, () =>
                {
                    ConsoleMethod.NicePrint("> Enter new patient diagnosis:");
                    patient.Diagnosis = ConsoleMethod.ReadLine();
                }),
            }),
            new ("Heart rate", new MenuItem[]
            {
                new(patient.HeartRate.ToString(), () =>
                {
                    ConsoleMethod.NicePrint("> Enter new patient heart rate:");
                    patient.HeartRate = Handlers.GetValue(0);
                }),
            }),
            new ("Temperature", new MenuItem[]
            {
                new(patient.Temperature.ToString(CultureInfo.InvariantCulture), () =>
                {
                    ConsoleMethod.NicePrint("> Enter new patient temperature:");
                    patient.Temperature = Handlers.GetValue(0.0);
                }),
            }),
            new ("Oxygen saturation", new MenuItem[]
            {
                new(patient.OxygenSaturation.ToString(), () =>
                {
                    ConsoleMethod.NicePrint("> Enter new patient oxygen saturation:");
                    patient.OxygenSaturation = Handlers.GetValue(0);
                }),
            }),
            new ("Select doctor to update", doctorsItems),
        };

        var dp = new DataPanel(new MenuTable(groups));
        dp.Run("Patient selected. Select field that you want to edit.");
    }
    
    /// <summary>
    /// Handles the action panel, allowing the user to select an action to perform on the data.
    /// </summary>
    public void HandleActionPanel()
    {
        var groups = new MenuGroup[] 
        {
            new ("Action type", new MenuItem[]
            {
                new("Sort", HandleSortPanel),
                new("Update", HandleSelectPatientPanel),
            })
        };
        
        var dp = new DataPanel(new MenuTable(groups));
        dp.Run($"Select what you want to do with data (Patients count: {_patientsRepository.Collection.Count}).");
    }
}