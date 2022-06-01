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

        public String generateNotification()
        {
            return this.prescription.DoctorsNote + " " + this.prescription.Medication + " " + this.prescription.Amount;
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
