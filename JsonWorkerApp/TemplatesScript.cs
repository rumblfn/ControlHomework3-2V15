using JsonWorkerApp.Components;
using JsonWorkerLib.Models.Patient;
using Utils;

namespace JsonWorkerApp;

/// <summary>
/// Provides menu panels for working with program,
/// </summary>
public class TemplatesScript
{
    private readonly PatientsList _patientsList;
    
    public TemplatesScript(PatientsList patientsList)
    {
        _patientsList = patientsList;
    }

    private void HandleSortPanel()
    {
        var groups = new MenuGroup[] {
            new ("", new MenuItem[]
            {
                new("Patient id", () => _patientsList.OrderBy(patient => patient.PatientId)),
                new("Age", () => _patientsList.OrderBy(patient => patient.Age)),
                new("Heart rate", () => _patientsList.OrderBy(patient => patient.HeartRate)),
            }),
            new ("", new MenuItem[]
            {
                new("Name", () => _patientsList.OrderBy(patient => patient.Name)),
                new("Gender", () => _patientsList.OrderBy(patient => patient.Gender)),
                new("Diagnosis", () => _patientsList.OrderBy(patient => patient.Diagnosis)),
                new("Temperature", () => _patientsList.OrderBy(patient => patient.Temperature)),
                new("Oxygen Saturation", () => _patientsList.OrderBy(patient => patient.OxygenSaturation)),
            }),
            new ("", new MenuItem[]
            {
                new("Temperature", () => _patientsList.OrderBy(patient => patient.Temperature)),
                new("Oxygen Saturation", () => _patientsList.OrderBy(patient => patient.OxygenSaturation)),
            })
        };
        
        var dp = new DataPanel(groups);
        dp.Run("Select sort field");
        HandleActionPanel();
    }

    private void HandleSelectPatientPanel()
    {
        var groups = new List<MenuGroup>();

        int rowsCount = (int)Math.Ceiling(_patientsList.Collection.Count / 5.0);
        for (int i = 0; i < rowsCount; i++)
        {
            var items = new List<MenuItem>();

            int maxColumnNumber = Math.Min(i * 5 + 5, _patientsList.Collection.Count);
            for (int j = i * 5; j < maxColumnNumber; j++)
            {
                Patient patient = _patientsList[j];
                items.Add(new MenuItem(patient.PatientId.ToString(), 
                    () => HandleUpdatePatientPanel(patient)));
            }

            if (items.Count > 0)
            {
                groups.Add(new MenuGroup("", items.ToArray()));
            }
        }
        
        var dp = new DataPanel(groups.ToArray());
        dp.Run("Select patient that you want update by id");
    }

    private void HandleUpdatePatientPanel(Patient panel)
    {
        
    }

    private void HandleShowData()
    {
        ConsoleMethod.NicePrint(_patientsList.ToJson());
        ConsoleMethod.NicePrint("Enter any key to continue.");
        ConsoleMethod.ReadKey();
        
        HandleActionPanel();
    }
    
    public void HandleActionPanel()
    {
        var groups = new MenuGroup[] {
            new ("Action type", new MenuItem[]
            {
                new("Sort", HandleSortPanel),
                new("Update", HandleSelectPatientPanel),
                new("Show data", HandleShowData),
            })
        };
        
        var dp = new DataPanel(groups);
        dp.Run("Select what you want to do with data.");
    }
}