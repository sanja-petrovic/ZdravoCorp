﻿using System;
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
using ZdravoKlinika.View.DoctorPages.Model;

namespace ZdravoKlinika.View.DoctorPages
{
    /// <summary>
    /// Interaction logic for DoctorMedicalRecord.xaml
    /// </summary>
    public partial class DoctorMedicalRecord : Page
    {
        Model.DoctorMedicalRecordViewModel viewModel;
        string patientId;
        List<PastAppointmentView> pastAppointmentViews;
        public DoctorMedicalRecord()
        {
            pastAppointmentViews = new List<PastAppointmentView>();
        }

        public void init(string patientId)
        {
            
            viewModel = new Model.DoctorMedicalRecordViewModel();
            DataContext = viewModel;
            viewModel.init(patientId);
            InitializeComponent();
        }

        public string PatientId { get => patientId; set => patientId = value; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PrescriptionView prescriptionView = new PrescriptionView();
            prescriptionView.init("456", viewModel.Id);
            prescriptionView.Show();
            prescriptionView.Closed += (s, eventarg) =>
            {
                viewModel.MedicationAdded();
            };
        }

        public void MedicationAdded()
        {
            viewModel.MedicationAdded();
        }

        private void EditAppointment(object sender, RoutedEventArgs e)
        {
            EditAppointmentWindow editAppointmentWindow = new EditAppointmentWindow();
            var selected = (PastViewModel)PastLB.SelectedItem;
            editAppointmentWindow.SelectedApptId = selected.AppointmentId;
            editAppointmentWindow.Init();
            editAppointmentWindow.Show();
            editAppointmentWindow.Closed += (s, eventarg) =>
            {
                viewModel.Edited();
            };
        }
    }
}