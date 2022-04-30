﻿using System;
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
        private MedicalRecordService medicalRecordService;
        //private MedicalRecordRepository medicalRecordRepository;
        private RegisteredPatientRepository registeredPatientRepository;
        

        public PrescriptionService()
        {
            prescriptionRepository = new PrescriptionRepository();
            medicalRecordService = new MedicalRecordService();
            this.registeredPatientRepository = new RegisteredPatientRepository();
            //medicalRecordRepository = new MedicalRecordRepository();
        }

        public List<Prescription> GetAll()
        {
            return this.prescriptionRepository.GetAll();
        }

        public Prescription GetById(int id)
        {
            return this.prescriptionRepository.GetById(id);
        }

        public void Prescribe(Doctor doctor, RegisteredPatient patient, Medication medication, int amount, int duration, int frequency, string singleDose, string repeat, string doctorsNote, bool noAlternatives, bool emergency)
        {
            int prescriptionId = (this.prescriptionRepository.GetAll().Count > 0) ? this.prescriptionRepository.GetAll().Last().Id + 1 : 1;

            Prescription prescription = new Prescription(doctor, patient, medication, amount, duration, frequency, singleDose, repeat, doctorsNote, noAlternatives, emergency, prescriptionId);
            medicalRecordService.AddCurrentMedication(patient.MedicalRecord.MedicalRecordId, medication);
            this.registeredPatientRepository.recordUpdated(patient);
            //medicalRecordRepository.AddCurrentMedication(patient.MedicalRecord.MedicalRecordId, medication);
            this.prescriptionRepository.Prescribe(prescription);

        }
    }
}
