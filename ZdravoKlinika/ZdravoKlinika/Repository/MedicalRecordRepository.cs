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
    public class MedicalRecordRepository : IMedicalRecordRepository
    {
        private MedicalRecordDataHandler medicalRecordDataHandler;
        private MedicationRepository medicationRepository;
        private List<MedicalRecord> medicalRecords;

        public MedicalRecordDataHandler MedicalRecordDataHandler { get => medicalRecordDataHandler; set => medicalRecordDataHandler = value; }
        public List<MedicalRecord> MedicalRecords { get => medicalRecords; set => medicalRecords = value; }

        public MedicalRecordRepository()
        {
            MedicalRecordDataHandler = new MedicalRecordDataHandler();
            ReadDataFromFile();

            medicationRepository = new MedicationRepository();
        }

        private void ReadDataFromFile()
        {
            MedicalRecords = MedicalRecordDataHandler.Read();
            if (MedicalRecords == null) MedicalRecords = new List<MedicalRecord>();
        }

        private void UpdateReferences(MedicalRecord medicalRecord)
        {
            for(int i = 0; i < medicalRecord.CurrentMedication.Count; i++)
            {
                medicalRecord.CurrentMedication[i] = medicationRepository.GetById(medicalRecord.CurrentMedication[i].MedicationId);
            }

            for(int i = 0; i < medicalRecord.PastMedication.Count; i++)
            {
                medicalRecord.PastMedication[i] = medicationRepository.GetById(medicalRecord.PastMedication[i].MedicationId);
            }      
        }

        public void Add(MedicalRecord medicalRecord)
        {
            bool duplicate = false;
            foreach (MedicalRecord med in MedicalRecords)
            {
                if (med.MedicalRecordId == medicalRecord.MedicalRecordId)
                {
                    duplicate = true;
                }
            }

            if(!duplicate)
            {
                this.MedicalRecords.Add(medicalRecord);
                MedicalRecordDataHandler.Write(MedicalRecords);
            }
        }
        public void Update(MedicalRecord medicalRecord)
        {
            int index = -1;
            foreach (MedicalRecord recordObject in this.MedicalRecords)
            {
                if (recordObject.MedicalRecordId.Equals(medicalRecord.MedicalRecordId))
                {
                    index = MedicalRecords.IndexOf(recordObject);
                }
            }

            if (index == -1)
            {
                throw new Exception("BAD");
            }

            MedicalRecords[index] = medicalRecord;
            MedicalRecordDataHandler.Write(MedicalRecords);
            return;
        }
        public void RemoveAll()
        {
            if (MedicalRecords != null)
                MedicalRecords.Clear();
        }

        public void Remove(MedicalRecord record)
        {
            if (record == null)
                return;
            if (this.MedicalRecords != null)
                if (MedicalRecords.Contains(record))
                    MedicalRecords.Remove(record);

            MedicalRecordDataHandler.Write(MedicalRecords);
        }

        public MedicalRecord GetById(String id)
        {
            ReadDataFromFile();
            MedicalRecord? medicalRecordToReturn = null;
            foreach (MedicalRecord record in this.medicalRecordDataHandler.Read())
            {
                if (record.MedicalRecordId.Equals(id))
                {
                    UpdateReferences(record);
                    medicalRecordToReturn = record;
                    break;
                }
            }
            return medicalRecordToReturn;
        }

        public void AddCurrentMedication(MedicalRecord record, Medication medication)
        {
            if(!record.CurrentMedication.Contains(medication))
            {
                record.AddCurrentMedication(medication);
            }
            int i = GetIndex(record.MedicalRecordId);
            this.MedicalRecords[i] = record;

            MedicalRecordDataHandler.Write(MedicalRecords);

        }

        public void AddDiagnosis(String diagnosis, MedicalRecord record)
        {
            if(record != null)
            {
                record.Diagnoses.Add(diagnosis);
            }
            int i = GetIndex(record.MedicalRecordId);
            MedicalRecords[i] = record;

            MedicalRecordDataHandler.Write(MedicalRecords);
        }

        public int GetIndex(string id)
        {
            int retVal = -1;
            for(int i = 0; i < this.MedicalRecords.Count(); i++)
            {
                if(this.MedicalRecords[i].MedicalRecordId.Equals(id))
                {
                    retVal = i;
                    break;
                }
            }

            return retVal;
        }

        public List<string> GetDiagnosesAndAllergies(MedicalRecord medicalRecord)
        {
            List<string> list = new List<string>();

            foreach(string allergy in medicalRecord.Allergies)
            {
                list.Add("Alergija: " + allergy);
            }
            foreach(string diagnosis in medicalRecord.Diagnoses)
            {
                list.Add(diagnosis);
            }

            return list;
        }

        public List<MedicalRecord> GetAll()
        {
            ReadDataFromFile();
            foreach (MedicalRecord record in this.medicalRecords)
            {
                UpdateReferences(record);
            }

            return this.medicalRecords;
        }

    }
}
