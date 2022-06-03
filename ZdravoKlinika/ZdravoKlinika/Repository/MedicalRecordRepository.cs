using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Data_Handler;
using ZdravoKlinika.Model;

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
            ReadDataFromFile();

            medicationRepository = new MedicationRepository();
        }

        private void ReadDataFromFile()
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

        public void Add(MedicalRecord medicalRecord)
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

        public MedicalRecord? GetById(String id)
        {
            ReadDataFromFile();
            MedicalRecord? medicalRecordToReturn = null;
            foreach (MedicalRecord record in this.MedicalRecords)
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

        public void AddCurrentMedication(String medicalRecordId, Medication medication)
        {
            MedicalRecord medicalRecord = this.GetById(medicalRecordId);
            if(!medicalRecord.CurrentMedication.Contains(medication))
            {
                medicalRecord.AddCurrentMedication(medication);
            }
            int i = FindIndexInList(medicalRecordId);
            this.MedicalRecords[i] = medicalRecord;

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

    }
}
