using System.Text.Json;
using JsonWorkerLib.Models._interfaces;

namespace JsonWorkerLib.Models.Patient;

public class PatientsList : ISerializable
{
    public List<Patient> Collection
    {
        get;
        private set;
    }

    public PatientsList()
    {
        Collection = new List<Patient>();
    }
    
    public PatientsList(List<Patient> models)
    {
        Collection = models;
        RemoveRepetitions();
    }

    private void RemoveRepetitions()
    {
        Dictionary<int, Doctor.Doctor> uniqDoctors = new();
        
        // Save uniq doctors.
        foreach (Doctor.Doctor doctor in Collection.SelectMany(patient => patient.Doctors))
        {
            uniqDoctors.TryAdd(doctor.DoctorId, doctor);
        }
        
        // Remove repetitions
        foreach (List<Doctor.Doctor> patientDoctors in Collection.Select(patient => patient.Doctors))
        {
            for (int i = 0; i < patientDoctors.Count; i++)
            {
                patientDoctors[i] = uniqDoctors[patientDoctors[i].DoctorId];
            }
        }
    }

    public void OrderBy<TKey>(Func<Patient,TKey> keySelector)
    {
        Collection = Collection.OrderBy(keySelector).ToList();
    }

    public string ToJson()
    {
        return JsonSerializer.Serialize(Collection);
    }

    public Patient this[int index] => Collection[index];
}