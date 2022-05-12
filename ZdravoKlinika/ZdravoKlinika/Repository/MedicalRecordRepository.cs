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
        

        public List<MedicalRecord> MedicalRecords
        {
            get
            {
                if (medicalRecords == null)
                    medicalRecords = new List<MedicalRecord>();
                return medicalRecords;
            }
            set
            {
                DeleteAllMedicalRecord();
                if (value != null)
                {
                    foreach (MedicalRecord oMedicalRecord in value)
                        CreateMedicalRecord(oMedicalRecord);
                }
            }
        }

        public MedicalRecordDataHandler MedicalRecordDataHandler { get => medicalRecordDataHandler; set => medicalRecordDataHandler = value; }

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
            if (this.medicalRecords == null)
                this.medicalRecords = new List<MedicalRecord>();

            foreach (MedicalRecord med in medicalRecords)
            {
                if (med.MedicalRecordId == medicalRecord.MedicalRecordId)
                {
                    throw new Exception("BAD");
                }
            }

            this.medicalRecords.Add(medicalRecord);
            MedicalRecordDataHandler.Write(medicalRecords);
            return;
        }
        public void UpdateMedicalRecord(MedicalRecord medicalRecord)
        {
            int index = -1;
            foreach (MedicalRecord recordObject in this.medicalRecords)
            {
                if (recordObject.MedicalRecordId.Equals(medicalRecord.MedicalRecordId))
                {
                    index = medicalRecords.IndexOf(recordObject);
                }
            }

            if (index == -1)
            {
                throw new Exception("BAD");
            }

            medicalRecords[index] = medicalRecord;
            MedicalRecordDataHandler.Write(medicalRecords);
            return;
        }
        public void DeleteAllMedicalRecord()
        {
            if (medicalRecords != null)
                medicalRecords.Clear();
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
            foreach (MedicalRecord record in this.medicalRecords)
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
            medicalRecord.AddCurrentMedication(medication);
            DeleteMedicalRecord(medicalRecord);
            medicalRecords.Add(medicalRecord);

            MedicalRecordDataHandler.Write(medicalRecords);

        }

    }
}
