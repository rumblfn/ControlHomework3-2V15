using System.Globalization;
using JsonWorkerApp.Panel;
using JsonWorkerApp.Panel.Components;
using JsonWorkerLib.Models.Doctor;
using JsonWorkerLib.Models.Patient;
using Utils;

namespace JsonWorkerApp;

/// <summary>
/// Provides menu panels for working with program,
/// </summary>
public class TemplatesScript
{
    private readonly PatientsRepository _patientsRepository;
    
    public TemplatesScript(PatientsRepository patientsRepository)
    {
        _patientsRepository = patientsRepository;
    }
    
    private void Sort<TKey>(Func<Patient,TKey> keySelector, string fieldName)
    {
        _patientsRepository.OrderBy(keySelector);
        ConsoleMethod.NicePrint($"Sort by {fieldName} completed.");
    }

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

    private void HandleUpdateDoctorPanel(Doctor doctor)
    {
        var groups = new MenuGroup[] 
        {
            new ("Id", new MenuItem[] {new(doctor.DoctorId.ToString(), null)}),
            new ("Name", new MenuItem[] {new(doctor.Name, () => {})}),
            new ("Appointment count", new MenuItem[]
            {
                new(doctor.AppointmentCount.ToString(), null),
            }),
        };

        var dp = new DataPanel(new MenuTable(groups));
        dp.Run($"Selected doctor: {doctor.DoctorId}. Select field that you want to edit.");
    }

    private void HandleUpdatePatientPanel(Patient patient)
    {
        MenuItem[] doctorsItems = patient.Doctors.Select(
            doctor => new MenuItem($"{doctor.DoctorId} {doctor.Name}", 
                () => HandleUpdateDoctorPanel(doctor))).ToArray();
        
        var groups = new MenuGroup[] 
        {
            new ("Id", new MenuItem[] { new(patient.PatientId.ToString(), null), }),
            new ("Name", new MenuItem[] { new(patient.Name, () => {}), }),
            new ("Age", new MenuItem[] { new(patient.Age.ToString(), () => {}), }),
            new ("Gender", new MenuItem[] { new(patient.Gender, () => {}), }),
            new ("Diagnosis", new MenuItem[] { new(patient.Diagnosis, () => {}), }),
            new ("Heart rate", new MenuItem[] { new(patient.HeartRate.ToString(), () => {}), }),
            new ("Temperature", new MenuItem[]
            {
                new(patient.Temperature.ToString(CultureInfo.InvariantCulture), () => {}),
            }),
            new ("Oxygen saturation", new MenuItem[]
            {
                new(patient.OxygenSaturation.ToString(), () => {}),
            }),
            new ("Select doctor to update", doctorsItems),
        };

        var dp = new DataPanel(new MenuTable(groups));
        dp.Run("Patient selected. Select field that you want to edit.");
    }
    
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