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
            medicalRecordDataHandler = new MedicalRecordDataHandler();
            medicalRecords = medicalRecordDataHandler.Read();
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
                DeleteAllMedicalRecords();
                if (value != null)
                {
                    foreach (MedicalRecord oMedicalRecord in value)
                        CreateMedicalRecord(oMedicalRecord);
                }
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
                    return;
                }
            }

            this.medicalRecords.Add(medicalRecord);
            medicalRecordDataHandler.Write(medicalRecords);
            return;
        }
        public void DeleteAllMedicalRecords()
        {
            if (medicalRecords != null)
                medicalRecords.Clear();
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

    }
}
