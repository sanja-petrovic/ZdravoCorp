using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Data_Handler;

namespace ZdravoKlinika.Repository
{
    public class MedicalRecordRepository
    {
        private MedicalRecordDataHandler medicalRecordDataHandler;
        private MedicationRepository medicationRepository;
        private List<MedicalRecord> medicalRecords;

        public MedicalRecordDataHandler MedicalRecordDataHandler { get => medicalRecordDataHandler; set => medicalRecordDataHandler = value; }
        public List<MedicalRecord> MedicalRecords { get => medicalRecords; set => medicalRecords = value; }

        public MedicalRecordRepository()
        {
            MedicalRecordDataHandler = new MedicalRecordDataHandler();
            ReadDataFromFiles();

            medicationRepository = new MedicationRepository();
        }

        private void ReadDataFromFiles()
        {
            MedicalRecords = MedicalRecordDataHandler.Read();
            if (MedicalRecords == null) MedicalRecords = new List<MedicalRecord>();
        }

        public void UpdateReferences(MedicalRecord medicalRecord)
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

        public void CreateMedicalRecord(MedicalRecord medicalRecord)
        {
            if (medicalRecord == null)
                return;
            if (this.MedicalRecords == null)
                this.MedicalRecords = new List<MedicalRecord>();

            foreach (MedicalRecord med in MedicalRecords)
            {
                if (med.MedicalRecordId == medicalRecord.MedicalRecordId)
                {
                    return; // throw new Exception("BAD");
                }
            }

            this.MedicalRecords.Add(medicalRecord);
            MedicalRecordDataHandler.Write(MedicalRecords);
            return;
        }
        public void UpdateMedicalRecord(MedicalRecord medicalRecord)
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
        public void DeleteAllMedicalRecord()
        {
            if (MedicalRecords != null)
                MedicalRecords.Clear();
        }

        public void DeleteMedicalRecord(MedicalRecord record)
        {
            if (record == null)
                return;
            if (this.MedicalRecords != null)
                if (MedicalRecords.Contains(record))
                    MedicalRecords.Remove(record);

            MedicalRecordDataHandler.Write(MedicalRecords);
        }

        public MedicalRecord? GetById(String id)
        {
            ReadDataFromFiles();
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
            int i = FindIndexInList(record.MedicalRecordId);
            this.MedicalRecords[i] = record;

            MedicalRecordDataHandler.Write(MedicalRecords);

        }

        public void AddDiagnosis(String diagnosis, MedicalRecord record)
        {
            if(record != null)
            {
                record.Diagnoses.Add(diagnosis);
            }
            int i = FindIndexInList(record.MedicalRecordId);
            MedicalRecords[i] = record;

            MedicalRecordDataHandler.Write(MedicalRecords);
        }

        public int FindIndexInList(string id)
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

    }
}
