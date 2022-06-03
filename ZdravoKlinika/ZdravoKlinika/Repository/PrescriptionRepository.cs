using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Data_Handler;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.Repository
{
    internal class PrescriptionRepository
    {

        private List<Prescription> prescriptions;
        private PrescriptionDataHandler prescriptionDataHandler;
        private MedicationRepository medicationRepository;
        private DoctorRepository doctorRepository;
        private RegisteredPatientRepository registeredPatientRepository;
        private PatientRepository patientRepository;

        public PrescriptionRepository()
        {
            prescriptionDataHandler = new PrescriptionDataHandler();
            ReadDataFromFile();
           
            medicationRepository = new MedicationRepository();
            registeredPatientRepository = new RegisteredPatientRepository();
            patientRepository = new PatientRepository();
            doctorRepository = new DoctorRepository();
        }

        private void ReadDataFromFile()
        {
            prescriptions = prescriptionDataHandler.Read();
            if (prescriptions == null) prescriptions = new List<Prescription>();
        }

        public List<Prescription> GetAll()
        {
            ReadDataFromFile();
            foreach (Prescription p in prescriptions)
            {
                UpdateReferences(p);
            }
            return prescriptions;
        }

        public Prescription? GetById(int id)
        {
            ReadDataFromFile();
            Prescription? prescriptionToReturn = null;
            foreach(Prescription p in prescriptions)
            {
                if(p.Id == id)
                {
                    UpdateReferences(p);
                    prescriptionToReturn = p;
                    break;
                }
            }

            return prescriptionToReturn;
        }
        

        public void UpdateReferences(Prescription prescription)
        {
            prescription.Medication = medicationRepository.GetById(prescription.Medication.MedicationId);
            prescription.Doctor = doctorRepository.GetById(prescription.Doctor.PersonalId);
            prescription.Patient = patientRepository.GetById(prescription.Patient.GetPatientId());  
        }

        public void Prescribe(Prescription prescription)
        {
            prescriptions.Add(prescription);
            prescriptionDataHandler.Write(prescriptions);        
        }
        
    }
}
