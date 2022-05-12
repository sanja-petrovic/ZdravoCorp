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
            this.prescriptions = prescriptionDataHandler.Read();
            this.medicationRepository = new MedicationRepository();
            registeredPatientRepository = new RegisteredPatientRepository();
            patientRepository = new PatientRepository();
            doctorRepository = new DoctorRepository();
            UpdateReferences();
        }

        public List<Prescription> GetAll()
        {
            return this.prescriptionDataHandler.Read();
        }

        public Prescription GetById(int id)
        {
            UpdateReferences();
            foreach(Prescription p in this.prescriptions)
            {
                if(p.Id == id)
                {
                    return p;
                }
            }

            return null;
        }
        

        public void UpdateReferences()
        {

            foreach(Prescription p in this.prescriptions)
            {
                p.Medication = medicationRepository.GetById(p.Medication.MedicationId);
                p.Doctor = doctorRepository.GetById(p.Doctor.PersonalId);
                p.Patient = patientRepository.GetById(p.Patient.GetPatientId());
            }

            
        }

        public void Prescribe(Prescription prescription)
        {
            this.prescriptions.Add(prescription);
            this.prescriptionDataHandler.Write(this.prescriptions);
        }
        
    }
}
