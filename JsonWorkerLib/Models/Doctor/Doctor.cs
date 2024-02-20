using System.Text.Json;
using System.Text.Json.Serialization;
using JsonWorkerLib.Models._interfaces;
using Utils;

namespace JsonWorkerLib.Models.Doctor;

/// <summary>
/// The doctor's model is associated with the patient <see cref="Patient"/>>.
/// </summary>
public class Doctor : Model, ISerializable, _interfaces.IObserver<StateChange>
{
    private int _appointmentCount;
    
    [JsonPropertyName("doctor_id")]
    public int DoctorId { get; }
    
    [JsonPropertyName("name")]
    public string Name { get; }

    [JsonPropertyName("appointment_count")]
    public int AppointmentCount
    {
        get => _appointmentCount;
        private set
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
            default:
                throw new ArgumentOutOfRangeException(nameof(updateEvent), updateEvent, null);
        }
    }
}
