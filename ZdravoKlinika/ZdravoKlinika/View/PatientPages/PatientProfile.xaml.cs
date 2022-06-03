using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZdravoKlinika.Model;
using ZdravoKlinika.PatientPages.ViewModel;

namespace ZdravoKlinika.View.PatientPages
{
    /// <summary>
    /// Interaction logic for PatientProfile.xaml
    /// </summary>
    public partial class PatientProfile : Page
    {
        private PatientProfileViewModel viewModel;
        public PatientProfile(String patientId)
        {
            viewModel = new PatientProfileViewModel(patientId);
            InitializeComponent();
            this.DataContext = viewModel;
            //UpdateNotificationsField();

            Timer execute = new Timer();
            TimeSpan fireInterval = new TimeSpan(0, 0, 15);
            execute.Interval = fireInterval.TotalMilliseconds;
            execute.Elapsed += OnTimedEvent;
            execute.AutoReset = true;
            execute.Start();
        }

        private void OnTimedEvent(object? sender, ElapsedEventArgs e)
        {
            //TODO on window close terminate this thread, and edit check only for patientId notifs
            App.Current.Dispatcher.Invoke((Action)delegate
                    {
                        viewModel.NotificationTexts.Clear();
                    });
            int hours = 5;
            String id;
            if (viewModel.Patient != null)
            {
                id = viewModel.Patient.GetPatientId();
                foreach (PatientMedicationNotification pmn in viewModel.NotifController.GetUpcomingNotifications(id, hours))
                {
                    App.Current.Dispatcher.Invoke((Action)delegate
                    {
                        viewModel.NotificationTexts.Add(pmn.GenerateDailyNotification());
                    });
                }
                foreach( PatientNotes note in viewModel.NotesController.GetUpcommingNotes(id, hours))
                {
                    App.Current.Dispatcher.Invoke((Action)delegate
                    {
                        viewModel.NotificationTexts.Add("Beleska: " +note.NotificationId+ Environment.NewLine+ note.NotificationText +", "+note.Trigger.ToString());
                    });
                }
            }
            
        }
        private void UpdateNotificationsField()
        {
            viewModel.NotificationTexts.Clear();
            int hours = 5;
            String id;
            if (viewModel.Patient != null)
            {
                id = viewModel.Patient.GetPatientId();
                foreach (PatientMedicationNotification pmn in viewModel.NotifController.GetUpcomingNotifications(id, hours))
                {
                     viewModel.NotificationTexts.Add(pmn.GenerateDailyNotification());
                }
                foreach (PatientNotes note in viewModel.NotesController.GetUpcommingNotes(id, hours))
                {
                     viewModel.NotificationTexts.Add("Beleska: " + note.NotificationId + Environment.NewLine + note.NotificationText + ", " + note.Trigger.ToString());
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //"Beleska: 5\r\nAAAAA, 03.06.2022. 10:16:09"
            String notifString = ((Button)sender).DataContext as String;
            String[] importantBits = notifString.Split("\r\n")[0].Split(":");
            if (importantBits[0] == "Beleska")
            {
                viewModel.NotesController.DeleteNote(Int32.Parse(importantBits[1]));
                UpdateNotificationsField();
            }
            else{
                viewModel.NotifController.DeleteNotification(Int32.Parse(importantBits[1]));
                UpdateNotificationsField();
            }
        }
    }
}
