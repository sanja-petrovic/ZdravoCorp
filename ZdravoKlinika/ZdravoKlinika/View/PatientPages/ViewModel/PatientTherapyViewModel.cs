﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ZdravoKlinika.Controller;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.View.PatientPages.ViewModel
{
    public class PatientTherapyViewModel : INotifyPropertyChanged
    {
        private String patientId;
        private List<DateTime> notificationDates = new List<DateTime>();
        private List<DateTime> notificationTimes = new List<DateTime>();
        private PatientMedicationNotificationController controller;
        private DateTime selectedDate;
        private PatientMedicationNotification selectedNotification;
        private DateTime currentDate;
        private MyICommand loadNotificationsCommand;
        private MyICommand loadTimesCommand;
        private ObservableCollection<PatientMedicationNotification> notifications;
        public PatientTherapyViewModel(String id)
        {
            selectedDate = DateTime.Now.Date;
            PatientId = id;
            Controller = new PatientMedicationNotificationController();
            NotificationDates = Controller.GetNotificationDatesForPatient(PatientId);
            LoadNotificationsCommand = new MyICommand(LoadNotifications, CanExecuteLoadNotifications);
            LoadTimesCommand = new MyICommand(LoadTimes, CanExecuteLoadTimes);
        }

        private void OnPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        public string PatientId { get => patientId; set => patientId = value; }
        public List<DateTime> NotificationDates { get => notificationDates; set => notificationDates = value; }
        public DateTime SelectedDate { get => selectedDate;
            set
            {
                selectedDate = value;
                LoadNotificationsCommand.RaiseCanExecuteChanged();
                LoadNotificationsCommand.Execute(this);
            }
        }
        internal PatientMedicationNotificationController Controller { get => controller; set => controller = value; }
        public ObservableCollection<PatientMedicationNotification> Notifications
        {
            get => notifications;

            set
            {
                notifications = value;
                OnPropertyChanged("");
            }
        }
        public DateTime CurrentDate { get => currentDate; set => currentDate = value; }
        public MyICommand LoadNotificationsCommand { get => loadNotificationsCommand; set => loadNotificationsCommand = value; }
        public PatientMedicationNotification SelectedNotification { get => selectedNotification;

            set
            {
                selectedNotification = value;
                
                LoadTimesCommand.RaiseCanExecuteChanged();
                LoadTimesCommand.Execute(this);
                OnPropertyChanged("");
            }
        
        }

        public List<DateTime> NotificationTimes { get => notificationTimes; set => notificationTimes = value; }
        public MyICommand LoadTimesCommand { get => loadTimesCommand; set => loadTimesCommand = value; }

        public void LoadNotifications(object data)
        {
            Notifications = new ObservableCollection<PatientMedicationNotification>(controller.GetByPatientForDate(patientId, selectedDate));
        }

        public bool CanExecuteLoadNotifications(object data)
        {
            bool retVal = false;
            if (selectedDate != null)
            {
                retVal= true;
            }
            return retVal;
        }

        public void LoadTimes(object data)
        {
            notificationTimes = controller.GetPossibleTriggerTimes(selectedNotification);
        }
        public bool CanExecuteLoadTimes(object data)
        {
            bool retVal = false;
            if(selectedNotification != null)
            {
                retVal = true;  
            }
            return retVal;
        }
    }
}
