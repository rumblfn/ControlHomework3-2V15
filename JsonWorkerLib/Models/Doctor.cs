using System.Text.Json;
using System.Text.Json.Serialization;
using JsonWorkerLib.Models.Interfaces;

namespace JsonWorkerLib.Models;

/// <summary>
/// The doctor's model is associated with the patient <see cref="Patient"/>>.
/// </summary>
public class Doctor : Model, ISerializable
{
    private int _id;
    private int _appointmentCount;
    private string _name = string.Empty;

    [JsonPropertyName("doctor_id")]
    public int DoctorId
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
    
    [JsonPropertyName("appointment_count")]
    public int AppointmentCount
    {
        get => _appointmentCount;
        set
        {
            _appointmentCount = value;
            OnUpdated();
        }
    }
    
    public Doctor()
    {
        DoctorId = 0;
        AppointmentCount = 0;
        Name = string.Empty;
    }

    [JsonConstructor]
    public Doctor(int doctorId, string name, int appointmentCount)
    {
        DoctorId = doctorId;
        Name = name;
        AppointmentCount = appointmentCount;
    }
    
    public string ToJSON()
    {
        return JsonSerializer.Serialize(this);
    }
}
