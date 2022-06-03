using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZdravoKlinika.Controller;

namespace ZdravoKlinika.View.DoctorPages
{
    public partial class DoctorSchedule : UserControl
    {
        private DateTime selected;
        private Doctor doctor;

        private DialogHelper.DialogService dialogService;

        public DoctorSchedule()
        {
            DataContext = this;
            dialogService = new DialogHelper.DialogService();
            this.doctor = RegisteredUserController.UserToDoctor(App.User);
            InitializeComponent();
            ApptTabPanel.Parent1 = this;
            ApptTabPanel.Doctor = doctor;
            ApptTabPanel.Selected = DateTime.Today;
            TimeOffView.Load();
        }
        private DelegateCommand showCreateDialog;
        private DelegateCommand showTimeOffDialog;
        private DelegateCommand recordCommand;

        public DateTime Selected { get => selected; set => selected = value; }
        public Doctor Doctor { get => doctor; set => doctor = value; }
        public DelegateCommand ShowCreateDialog => showCreateDialog ?? (showCreateDialog = new DelegateCommand(ExecuteShowCreateDialog));
        public DelegateCommand ShowTimeOffDialog => showTimeOffDialog ?? (showTimeOffDialog = new DelegateCommand(ExecuteShowTimeOffDialog));
        public DelegateCommand RecordCommand => recordCommand ?? (recordCommand = new DelegateCommand(ExecuteGoToRecord));


        private void ExecuteGoToRecord()
        {

        }

        public void CalendarSelectionChanged(object sender, RoutedEventArgs e) {

            ApptTabPanel.Selected = (DateTime) Cal.SelectedDate;
        }

        public void goToMedicalRecord(string patientId)
        {
            DoctorMedicalRecord doctorMedicalRecord = new DoctorMedicalRecord();

            doctorMedicalRecord.Init(patientId);
            //this.NavigationService.Navigate(doctorMedicalRecord);
        }


        private void RequestButton_Click(object sender, RoutedEventArgs e)
        {
            TimeOffRequestView timeOffRequestView = new TimeOffRequestView();
            timeOffRequestView.ShowDialog();
            TimeOffView.Load();
        }

        private void ExecuteShowTimeOffDialog()
        {
            dialogService.ShowTimeOffDialog();
            TimeOffView.Load();
        }

        private void ExecuteShowCreateDialog()
        {
            dialogService.ShowCreateApptScheduleDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CreateApptSchedule createAppt = new CreateApptSchedule();
            createAppt.ShowDialog();
        }
    }
}
