using System.Text.Json;
using System.Text.Json.Serialization;
using JsonWorkerLib.Models.Interfaces;

namespace JsonWorkerLib.Models;

/// <summary>
/// Patient model.
/// </summary>
public class Patient : Model, ISerializable
{
    private int _id;
    private int _age;
    private int _heartRate;
    private int _oxygenSaturation;
    private double _temperature;
    private string _name = string.Empty;
    private string _gender = string.Empty;
    private string _diagnosis = string.Empty;
    private List<Doctor> _doctors = new ();

    [JsonPropertyName("patient_id")]
    public int PatientId
    {
        get => _id;
        set
        {
            _id = value;
            OnUpdated();
        }
    }
    
    [JsonPropertyName("name")]
    public string Name
    {
        get => _name;
        set
        {
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
            _oxygenSaturation = value;
            OnUpdated();
        }
    }
    
    [JsonPropertyName("doctors")]
    public List<Doctor> Doctors
    {
        get => _doctors;
        set
        {
            _doctors = value;
            OnUpdated();
        }
    }

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
        
        Doctors = new List<Doctor>();
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
        List<Doctor> doctors)
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

    public string ToJSON()
    {
        return JsonSerializer.Serialize(this);
    }
}