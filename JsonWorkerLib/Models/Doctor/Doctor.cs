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
            ConsoleMethod.NicePrint($"Doctor name updates {Name} -> {value}");
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
            ConsoleMethod.NicePrint(
                $"Doctors (Id: {DoctorId}) appointment count updates: {AppointmentCount} -> {value}");
            
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
