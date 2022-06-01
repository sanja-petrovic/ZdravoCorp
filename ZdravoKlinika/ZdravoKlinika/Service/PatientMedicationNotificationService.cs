using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using ZdravoKlinika.Repository;
using ZdravoKlinika.Util;

namespace ZdravoKlinika.Service
{
    internal class PatientMedicationNotificationService
    {
        private PatientMedicationNotificationRepository patientMedicationNotificationRepository;

        public PatientMedicationNotificationRepository PatientMedicationNotificationRepository { get => patientMedicationNotificationRepository; set => patientMedicationNotificationRepository = value; }

        public PatientMedicationNotificationService()
        {
        this.patientMedicationNotificationRepository = new PatientMedicationNotificationRepository();

        }

        public List<PatientMedicationNotification> GetAll()
        {
            return patientMedicationNotificationRepository.GetAll();
        }
        public PatientMedicationNotification GetById(int id)
        {
            return this.patientMedicationNotificationRepository.GetById(id);
        }
        public List<PatientMedicationNotification> GetByPatientId(String id)
        {
            List <PatientMedicationNotification> retVal =  new List<PatientMedicationNotification>();
            foreach(PatientMedicationNotification notification in this.GetAll())
            {
                if (notification.Prescription.Patient.GetPatientId().Equals(id))
                {
                    retVal.Add(notification);
                }
            }
            return retVal;
        }
        public List<PatientMedicationNotification> GetByPatientForDate(String id, DateTime date)
        {
            List<PatientMedicationNotification> retVal = new List<PatientMedicationNotification>();
            foreach(PatientMedicationNotification notification in this.GetByPatientId(id))
            {
                if (notification.Prescription.Repeat != null)
                {
                    if (notification.Prescription.Repeat.Equals("dnevno"))
                    {
                        PatientMedicationNotification currentNotif = GetDailyNotifications(notification, date);
                        if (currentNotif != null)
                        {
                            retVal.Add(currentNotif);
                        }
                    }
                    else if (notification.Prescription.Repeat.Equals("nedeljno"))
                    {
                        //TODO finish
                    }
                    else
                    {
                        //err
                    }
                }
                
            }
            return retVal;
        }

        private PatientMedicationNotification GetDailyNotifications(PatientMedicationNotification notification, DateTime date)
        {
           PatientMedicationNotification retVal = null;
           for (int currDay = 1; currDay <= notification.Prescription.Duration; currDay++)
            {
                if (notification.Prescription.DateOfCreation.AddDays(currDay).Date.Equals(date.Date))
                {
                    retVal = notification;
                    break;
                }
            }
            return retVal;
        }
        private List<PatientMedicationNotification> GetWeeklyNotifications()
        {
            // frequency values corespodention to days:
            // frequency 1, every monday
            // frequency 2, every monday and thursday
            // frequency 3, every monday wednesday and sunday
            // frequency 4, every monday, wednesday, friday and sunday
            // frequency 5, 
            return null;
        }
        public List<DateTime> GetNotificationDatesForPatient(String id)
        {
            List<DateTime> retVal = new List<DateTime>();
            foreach (PatientMedicationNotification notification in this.GetByPatientId(id))
            {
                if (notification.Prescription.Repeat != null)
                {
                    if (notification.Prescription.Repeat.Equals("dnevno"))
                    {
                        retVal.AddRange(GetAllNotificationDates(notification));
                    }
                    else if (notification.Prescription.Repeat.Equals("nedeljno"))
                    {
                        //TODO finish
                    }
                    else
                    {
                        //err
                    }
                }
            }
            return retVal;
        }

        private List<DateTime> GetAllNotificationDates(PatientMedicationNotification notification)
        {
            List<DateTime> retVal = new List<DateTime>();
            for (int currDay = 1; currDay <= notification.Prescription.Duration; currDay++) // curr day starts with 1 so that he starts taking therapy the day after perscribingDate
            {
                retVal.Add(notification.Prescription.DateOfCreation.Date.AddDays(currDay));
            }
            return retVal;
        }

        public List<DateTime> GetPossibleTriggerTimes(PatientMedicationNotification notification)
        {
            List<DateTime> retVal = new List<DateTime>();   
            if (notification != null && notification.Prescription.Repeat != null )
            {
                if (notification.Prescription.Repeat.Equals("dnevno"))
                {
                    retVal = GetPossibleDailyTimes(notification);
                }
                else if (notification.Prescription.Repeat.Equals("nedeljno"))
                {
                    //TODO finish
                }
                else
                {
                    //err
                }
            }
            return retVal;
        }

        private List<DateTime> GetPossibleDailyTimes(PatientMedicationNotification notification)
        {
            List<DateTime> retVal = new List<DateTime>();
            switch (notification.Prescription.Frequency)
            {
                case 1: //using DateTime.MinValue since the date part has no use, use .TimeOfDay or custom Parse when calculating when to trigger and display 
                    retVal = DateBlock.GetHourlyIntervals(DateTime.MinValue.Date.AddHours(8), DateTime.MinValue.Date.AddHours(20)); //can pick anytime that he wants to consume the medicine
                    break;
                case 2: case 3:
                    retVal = DateBlock.GetHourlyIntervals(DateTime.MinValue.Date.AddHours(8), DateTime.MinValue.Date.AddHours(12)); // anything before noon, the second dose is 12 hours into the future, or 6 and then another 6 in case od 3 times a day
                    break;
            }
            return retVal;
        }
        public void CreateNotification(PatientMedicationNotification notification)
        {
            List<PatientMedicationNotification> notifications = new List<PatientMedicationNotification>();
            int newId;
            notifications = patientMedicationNotificationRepository.GetAll();
            if (notifications.Count > 0)
            {
                newId = notifications.Last().NotificationId + 1;
            }
            else
            {
                newId = 1;
            }
            notification.NotificationId = newId;
            patientMedicationNotificationRepository.CreateNotification( notification );
        }
        public void DeleteNotification(int id)
        {
            patientMedicationNotificationRepository.DeleteNotification(patientMedicationNotificationRepository.GetById(id));
        }
        public void DeleteAllNotifications()
        {
            patientMedicationNotificationRepository.DeleteAllNotifications();
        }
        
        public void UpdateNotification(PatientMedicationNotification notification)
        { 
            patientMedicationNotificationRepository.UpdateNotification( notification );
        }
    }
    
}
