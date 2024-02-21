using System.Text.Json;
using JsonWorkerLib._interfaces;
using JsonWorkerLib.Abstractions;
using JsonWorkerLib.Models._shared;
using System.Text.Json.Serialization;

namespace JsonWorkerLib.Models.Doctor;

/// <summary>
/// The doctor's model is associated with the patient <see cref="Patient"/>>.
/// </summary>
public class Doctor : Model, ISerializable, _interfaces.IObserver<StateChange>
{
    private int _doctorId;
    private string _name = string.Empty;
    private int _appointmentCount;

    [JsonPropertyName("doctor_id")]
    public int DoctorId
    {
        get => _doctorId;
        private init => _doctorId = value;
    }

    [JsonPropertyName("name")]
    public string Name
    {
        get => _name;
        set
        {
            Logger.Info($"Doctor Id: {DoctorId}, name updates {Name} -> {value}");
            
            _name = value;
            OnUpdated();
        }
    }

    [JsonPropertyName("appointment_count")]
    public int AppointmentCount
    {
        get => _appointmentCount;
        private set
        {
            Logger.Info($"Doctor Id: {DoctorId}, appointment count updates: {AppointmentCount} -> {value}");
            
            _appointmentCount = value;
            OnUpdated();
        }
    }
    
    /// <summary>
    /// Default constructor.
    /// </summary>
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
    
    public string ToJson()
    {
        return JsonSerializer.Serialize(this);
    }

    public void Update(object? sender, StateChange updateEvent)
    {
        switch (updateEvent)
        {
            case StateChange.ReturnedToNormal:
                AppointmentCount -= 1;
                break;
            case StateChange.ExceededThresholds:
                AppointmentCount += 1;
                break;
            case StateChange.Default:
            default:
                throw new ArgumentOutOfRangeException(nameof(updateEvent), updateEvent, null);
        }
    }
}
