using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Data_Handler;
using ZdravoKlinika.Model;
using ZdravoKlinika.Repository.Interfaces;

namespace ZdravoKlinika.Repository
{
    internal class PrescriptionRepository : IPrescriptionRepository
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

        public void Add(Prescription item)
        {
            if(this.GetById(item.Id) == null)
                this.prescriptions.Add(item);
            this.prescriptionDataHandler.Write(this.prescriptions);
        }

        public void Remove(Prescription item)
        {
            if(this.GetById(item.Id) != null) 
                this.prescriptions.Remove(this.GetById(item.Id));
            this.prescriptionDataHandler.Write(this.prescriptions);
        }

        public void Update(Prescription item)
        {
            if(this.GetById(item.Id) != null)
            {
                int index = this.prescriptions.FindIndex(p => p.Id == item.Id);
                this.prescriptions[index] = item;
            }
            this.prescriptionDataHandler.Write(this.prescriptions);
        }

        public void RemoveAll()
        {
            this.prescriptions.Clear();
            this.prescriptionDataHandler.Write(this.prescriptions);
        }
    }
}
