using JsonWorkerLib.Abstractions;

namespace JsonWorkerLib.Models.Patient;

public class PatientsRepository : Repository<Patient>
{
    public PatientsRepository() : base(new List<Patient>())
    {
        
    }
    
    public PatientsRepository(List<Patient> collection) : base(collection)
    {
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
}