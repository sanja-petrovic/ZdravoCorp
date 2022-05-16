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

namespace ZdravoKlinika.ViewModel
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
            viewModel.Notifs = viewModel.NotifController.GetAll();
            foreach(PatientMedicationNotification pmn in viewModel.Notifs)
            {
                if ((pmn.Prescription.DateOfCreation - DateTime.Now).TotalHours < 5 & (pmn.Prescription.DateOfCreation - DateTime.Now).TotalHours > 0 )
                {
                    App.Current.Dispatcher.Invoke((Action)delegate
                    {
                        viewModel.NotificationTexts.Add(pmn.generateNotification());
                    });
                }
            }
        }
    }
}
