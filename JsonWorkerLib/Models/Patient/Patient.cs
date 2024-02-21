using System.Text.Json;
using System.Text.Json.Serialization;
using JsonWorkerLib._interfaces;
using JsonWorkerLib.Abstractions;
using JsonWorkerLib.Models._shared;

namespace JsonWorkerLib.Models.Patient;

/// <summary>
/// Patient.
/// </summary>
public class Patient : Model, ISerializable, _interfaces.IObservable<StateChange>
{
    public new event EventHandler<StateChange>? Updated;

    private readonly int _patientId;
    private int _age;
    
    private string _name = string.Empty;
    private string _gender = string.Empty;
    private string _diagnosis = string.Empty;
    
    private int _heartRate;
    private int _oxygenSaturation;
    private double _temperature;

    [JsonPropertyName("patient_id")]
    public int PatientId
    {
        get => _patientId;
        private init => _patientId = value;
    }
    
    [JsonPropertyName("name")]
    public string Name
    {
        get => _name;
        set
        {
            Logger.Info($"Patient Id: {PatientId}, name updates {Name} -> {value}");
            
            _name = value;
            OnUpdated();
        }
    }
    
    [JsonPropertyName("age")]
    public int Age
    {
        get => _age;
        set
        {
            Logger.Info($"Patient Id: {PatientId}, age updates {Age} -> {value}");
            
            _age = value;
            OnUpdated();
        }
    }
    
    [JsonPropertyName("gender")]
    public string Gender
    {
        get => _gender;
        set
        {
            Logger.Info($"Patient Id: {PatientId}, gender updates {Gender} -> {value}");
            
            _gender = value;
            OnUpdated();
        }
    }
    
    [JsonPropertyName("diagnosis")]
    public string Diagnosis
    {
        get => _diagnosis;
        set
        {
            Logger.Info($"Patient Id: {PatientId}, diagnosis updates {Diagnosis} -> {value}");
            
            _diagnosis = value;
            OnUpdated();
        }
    }
    
    [JsonPropertyName("heart_rate")]
    public int HeartRate
    {
        get => _heartRate;
        set
        {
            Logger.Info($"Patient Id: {PatientId}, heart rate updates {HeartRate} -> {value}");
            
            StateChange stateChange = Handlers.GetChangedStatus(
                _heartRate, value, 60, 100);
            OnUpdated(stateChange);
            
            _heartRate = value;
            OnUpdated();
        }
    }
    
    [JsonPropertyName("temperature")]
    public double Temperature
    {
        get => _temperature;
        set
        {
            Logger.Info($"Patient Id: {PatientId}, temperature updates {Temperature} -> {value}");
            
            StateChange stateChange = Handlers.GetChangedStatus(
                _temperature, value, 36, 38);
            OnUpdated(stateChange);
            
            _temperature = value;
            OnUpdated();
        }
    }
    
    [JsonPropertyName("oxygen_saturation")]
    public int OxygenSaturation
    {
        get => _oxygenSaturation;
        set
        {
            Logger.Info($"Patient Id: {PatientId}, oxygen saturation updates {OxygenSaturation} -> {value}");
            
            StateChange stateChange = Handlers.GetChangedStatus(
                _oxygenSaturation, value, 95, 100);
            OnUpdated(stateChange);
            
            _oxygenSaturation = value;
            OnUpdated();
        }
    }
    
    [JsonPropertyName("doctors")]
    public List<Doctor.Doctor> Doctors { get; }

    public Patient()
    {
        Age = 0;
        PatientId = 0;
        HeartRate = 0;
        Temperature = 0;
        OxygenSaturation = 0;
        
        Name = string.Empty;
        Gender = string.Empty;
        Diagnosis = string.Empty;
        
        Doctors = new List<Doctor.Doctor>();
    }

    [JsonConstructor]
    public Patient(
        int patientId,
        string name,
        int age,
        string gender,
        string diagnosis,
        int heartRate,
        double temperature,
        int oxygenSaturation,
        List<Doctor.Doctor> doctors)
    {
        PatientId = patientId;
        Name = name;
        Age = age;
        Gender = gender;
        Diagnosis = diagnosis;
        HeartRate = heartRate;
        Temperature = temperature;
        OxygenSaturation = oxygenSaturation;
        Doctors = doctors;
    }
    
    public void Subscribe(EventHandler<StateChange> updateEvent)
    {
        Updated += updateEvent;
    }

    private void OnUpdated(StateChange stateChange)
    {
        if (stateChange == StateChange.Default)
        {
            return;
        }
        
        Logger.Info($"Patient Id: {PatientId}, state changed to {stateChange}");
        Updated?.Invoke(this, stateChange);
    }

    public string ToJson()
    {
        return JsonSerializer.Serialize(this);
    }
}