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
        private List<MedicalRecord> medicalRecords;

        public MedicalRecordRepository()
        {
            MedicalRecordDataHandler = new MedicalRecordDataHandler();
            medicalRecords = MedicalRecordDataHandler.Read();
        }
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
                    return;
                }
            }

            this.medicalRecords.Add(medicalRecord);
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
            // TODO medication
            MedicalRecordDataHandler.Write(MedicalRecords);
        }

        public MedicalRecord? GetById(String id)
        {
            foreach (MedicalRecord record in this.medicalRecords)
            {
                if (record.MedicalRecordId.Equals(id))
                {
                    return record;
                }
            }
            return null;
        }

        public void AddCurrentMedication(MedicalRecord medicalRecord, Medication medication)
        {
            medicalRecord.AddCurrentMedication(medication);
            this.DeleteMedicalRecord(medicalRecord);
            this.medicalRecords.Add(medicalRecord);

            MedicalRecordDataHandler.Write(this.medicalRecords);
        }

        public void AddCurrentMedication(String id, Medication medication)
        {
            MedicalRecord medicalRecord = this.GetById(id);
            medicalRecord.AddCurrentMedication(medication);
            this.DeleteMedicalRecord(medicalRecord);
            this.medicalRecords.Add(medicalRecord);

            MedicalRecordDataHandler.Write(this.medicalRecords);
        }

    }
}
