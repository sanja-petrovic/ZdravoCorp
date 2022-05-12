using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using ZdravoKlinika.Repository;

namespace ZdravoKlinika.Service
{
    internal class PrescriptionService
    {
        private PrescriptionRepository prescriptionRepository;
        private MedicalRecordRepository medicalRecordRepository;
        private RegisteredPatientRepository registeredPatientRepository;
        private DoctorRepository doctorRepository;
        private RegisteredPatientRepository patientRepository;
        private MedicationRepository medicationRepository;
        
        private MedicalRecordService medicalRecordService;
        private PatientMedicationNotificationService patientMedicationNotificationService;
        //private MedicalRecordRepository medicalRecordRepository;

        public PrescriptionService()
        {
            prescriptionRepository = new PrescriptionRepository();
            //medicalRecordService = new MedicalRecordService();
            this.registeredPatientRepository = new RegisteredPatientRepository();
            medicalRecordRepository = new MedicalRecordRepository();
            this.doctorRepository = new DoctorRepository();
            this.patientRepository = new RegisteredPatientRepository();
            this.medicationRepository = new MedicationRepository();
            medicalRecordService = new MedicalRecordService();
            patientMedicationNotificationService = new PatientMedicationNotificationService();        

        }

        public List<Prescription> GetAll()
        {
            return this.prescriptionRepository.GetAll();
        }

        public Prescription GetById(int id)
        {
            return this.prescriptionRepository.GetById(id);
        }

        public void Prescribe(Doctor doctor, RegisteredPatient patient, Medication medication, int amount, int duration, int frequency, string singleDose, string repeat, string doctorsNote)
        {
            int prescriptionId = (this.prescriptionRepository.GetAll().Count > 0) ? this.prescriptionRepository.GetAll().Last().Id + 1 : 1;

            Prescription prescription = new Prescription(doctor, patient, medication, amount, duration, frequency, singleDose, repeat, doctorsNote, prescriptionId);
            prescription.DateOfCreation = DateTime.Now;
            medicalRecordRepository.AddCurrentMedication(patient.MedicalRecord.MedicalRecordId, medication);
            this.registeredPatientRepository.RecordUpdated(patient);

            this.prescriptionRepository.Prescribe(prescription);
            //make a notif
            patientMedicationNotificationService.CreateNotification(doctor, patient, "prepisan lek", prescription);
        }

        public void Prescribe(Prescription prescription)
        {
            int prescriptionId = (this.prescriptionRepository.GetAll().Count > 0) ? this.prescriptionRepository.GetAll().Last().Id + 1 : 1;
            prescription.Id = prescriptionId;
            medicalRecordRepository.AddCurrentMedication(prescription.RegisteredPatient.MedicalRecord.MedicalRecordId, prescription.Medication);
            this.registeredPatientRepository.RecordUpdated(prescription.RegisteredPatient);
            this.prescriptionRepository.Prescribe(prescription);
            //make a notif
            patientMedicationNotificationService.CreateNotification(prescription.Doctor, prescription.RegisteredPatient, "prepisan lek", prescription);
        }

        public void CreatePrescription(String doctorId, String patientId, String medicationId, int amount, int duration, int frequency, string singleDose, string repeat, string doctorsNote)
        {
            Doctor doctor = this.doctorRepository.GetById(doctorId);
            RegisteredPatient patient = this.patientRepository.GetById(patientId);
            Medication medication = this.medicationRepository.GetById(medicationId);
            //this.prescriptionService.Create(doctorId, patientId, medicationId, amount, duration, frequency, singleDose, repeat, doctorsNote, noAlternatives, emergency);
        }
    }
}
