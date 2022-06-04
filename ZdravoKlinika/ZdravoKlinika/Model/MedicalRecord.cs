using System;
using System.Collections.Generic;

namespace ZdravoKlinika.Model
{
    public class MedicalRecord
    {
        private String medicalRecordId;
        private List<String> diagnoses;
        private List<String> allergies;
        private List<Medication> currentMedication;
        private List<Medication> pastMedication;
        private List<Report> reports;

        public MedicalRecord(String patient, List<string> diagnoses, List<string> allergies)
        {
            this.MedicalRecordId = patient;
            this.diagnoses = diagnoses;
            this.allergies = allergies;
        }

        public MedicalRecord() { }

        public List<string> Diagnoses { get => diagnoses; set => diagnoses = value; }
        public List<string> Allergies { get => allergies; set => allergies = value; }

        public static MedicalRecord Parse(String id)
        {
            MedicalRecord medicalRecord = new MedicalRecord();
            medicalRecord.MedicalRecordId = id;
            return medicalRecord;
        }

        public List<Medication> CurrentMedication
        {
            get
            {
                if (currentMedication == null)
                    currentMedication = new List<Medication>();
                return currentMedication;
            }
            set
            {
                RemoveAllCurrentMedication();
                if (value != null)
                {
                    foreach (Medication oMedication in value)
                        AddCurrentMedication(oMedication);
                }
            }
        }
        public void AddCurrentMedication(Medication newMedication)
        {
            if (newMedication == null)
                return;
            if (this.currentMedication == null)
                this.currentMedication = new List<Medication>();
            if (!this.currentMedication.Contains(newMedication))
                this.currentMedication.Add(newMedication);
        }
        public void RemoveCurrentMedication(Medication oldMedication)
        {
            if (oldMedication == null)
                return;
            if (this.currentMedication != null)
                if (this.currentMedication.Contains(oldMedication))
                    this.currentMedication.Remove(oldMedication);
        }
        public void RemoveAllCurrentMedication()
        {
            if (currentMedication != null)
                currentMedication.Clear();
        }


        public List<Medication> PastMedication
        {
            get
            {
                if (pastMedication == null)
                    pastMedication = new List<Medication>();
                return pastMedication;
            }
            set
            {
                RemoveAllPastMedication();
                if (value != null)
                {
                    foreach (Medication oMedication in value)
                        AddPastMedication(oMedication);
                }
            }
        }
        public void AddPastMedication(Medication newMedication)
        {
            if (newMedication == null)
                return;
            if (this.pastMedication == null)
                this.pastMedication = new List<Medication>();
            if (!this.pastMedication.Contains(newMedication))
                this.pastMedication.Add(newMedication);
        }
        public void RemovePastMedication(Medication oldMedication)
        {
            if (oldMedication == null)
                return;
            if (this.pastMedication != null)
                if (this.pastMedication.Contains(oldMedication))
                    this.pastMedication.Remove(oldMedication);
        }
        public void RemoveAllPastMedication()
        {
            if (pastMedication != null)
                pastMedication.Clear();
        }

        public List<Report> Reports
        {
            get
            {
                if (reports == null)
                    reports = new List<Report>();
                return reports;
            }
            set
            {
                RemoveAllReports();
                if (value != null)
                {
                    foreach (Report oReport in value)
                        AddReports(oReport);
                }
            }
        }

        public string MedicalRecordId { get => medicalRecordId; set => medicalRecordId = value; }

        public void AddReports(Report newReport)
        {
            if (newReport == null)
                return;
            if (this.reports == null)
                this.reports = new List<Report>();
            if (!this.reports.Contains(newReport))
                this.reports.Add(newReport);
        }

        public void RemoveReports(Report oldReport)
        {
            if (oldReport == null)
                return;
            if (this.reports != null)
                if (this.reports.Contains(oldReport))
                    this.reports.Remove(oldReport);
        }

        public void RemoveAllReports()
        {
            if (reports != null)
                reports.Clear();
        }
    }
}
