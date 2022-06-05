using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.Model
{
    public class PatientMedicationNotification : Notification
    {
        private Prescription prescription;
        private String note;
        private DateTime triggerTime;
        public Prescription Prescription { get => prescription; set => prescription = value; }
        public string Note { get => note; set => note = value; }
        public DateTime TriggerTime { get => triggerTime; set => triggerTime = value; }

        public String GenerateDailyNotification()
        {
            String retVal =""; 
            if(this.Prescription != null)
            {
                switch (this.Prescription.Frequency)
                {
                    case 1:
                        retVal = GenerateDailyRepeatingOnce();
                        break;
                    case 2:
                        retVal = GenerateDailyRepeatingTwice();
                        break;
                    case 3:
                       retVal = GenerateDailyRepeatingThreeTimes();
                        break;
                    default:
                        break;
                }
            }
            return retVal;
        }
        private String GenerateDailyRepeatingOnce()
        {
            return "Terapija: " + this.NotificationId + Environment.NewLine + this.prescription.DoctorsNote + " " + this.prescription.Medication + " " + this.prescription.Amount + Environment.NewLine + this.triggerTime;
        }
        private String GenerateDailyRepeatingTwice()
        {
            String retVal = "";
            if (DateTime.Now < DateTime.Now.Date.AddHours(12))
            {
                retVal = "Terapija: " + this.NotificationId + Environment.NewLine + this.prescription.DoctorsNote + " " + this.prescription.Medication + " " + this.prescription.Amount + Environment.NewLine + this.triggerTime;
            }
            else
            {
                retVal = "Terapija: " + this.NotificationId + Environment.NewLine + this.prescription.DoctorsNote + " " + this.prescription.Medication + " " + this.prescription.Amount + Environment.NewLine + this.triggerTime.AddHours(12);
            }
            return retVal;
        }
        private String GenerateDailyRepeatingThreeTimes()
        {
            String retVal = "";
            if (DateTime.Now < DateTime.Now.Date.AddHours(12))
            {
                retVal = "Terapija: " + this.NotificationId + Environment.NewLine + this.prescription.DoctorsNote + " " + this.prescription.Medication + " " + this.prescription.Amount + Environment.NewLine + this.triggerTime;
            }
            if (DateTime.Now > DateTime.Now.Date.AddHours(12) && DateTime.Now < DateTime.Now.Date.AddHours(18))
            {
                retVal = "Terapija: " + this.NotificationId + Environment.NewLine + this.prescription.DoctorsNote + " " + this.prescription.Medication + " " + this.prescription.Amount + Environment.NewLine + this.triggerTime.AddHours(6);
            }
            if (DateTime.Now > DateTime.Now.Date.AddHours(18))
            {
                retVal = "Terapija: " + this.NotificationId + Environment.NewLine + this.prescription.DoctorsNote + " " + this.prescription.Medication + " " + this.prescription.Amount + Environment.NewLine + this.triggerTime.AddHours(12);
            }
            return retVal;
        }
        public PatientMedicationNotification()
        {

        }
        public PatientMedicationNotification(int id, RegisteredUser sender, RegisteredUser reciever, string notificationText, Prescription prescription, String note, DateTime time)
        {
            this.NotificationId = id;
            this.Sender = sender;
            this.Reciver = reciever;
            this.NotificationText = notificationText;
            this.Prescription = prescription;
            this.note = note;
            this.triggerTime = time;
        }
    }
}
