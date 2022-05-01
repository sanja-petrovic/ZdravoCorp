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
using System.Windows.Shapes;
using ZdravoKlinika.Controller;
using ZdravoKlinika.View.DoctorPages.Model;

namespace ZdravoKlinika.View.DoctorPages
{
    /// <summary>
    /// Interaction logic for PrescriptionView.xaml
    /// </summary>
    public partial class PrescriptionView : Window
    {

        private string patientId;
        private string doctorId;

        private PrescriptionController prescriptionController;
        private PrescriptionViewModel viewModel;

        public PrescriptionView()
        {
            prescriptionController = new PrescriptionController();
            
        }

        public void init(string doctorId, string patientId)
        {
            viewModel = new PrescriptionViewModel();
            viewModel.init(doctorId, patientId);
            DataContext = viewModel;
            InitializeComponent();
        }

        public string PatientId { get => patientId; set => patientId = value; }
        public string DoctorId { get => doctorId; set => doctorId = value; }

        private void Confirm(object sender, RoutedEventArgs e)
        {
            var selected = (String) RepeatCB.SelectedItem;
            var note = (String)NoteTB.Text;
            if(viewModel.Save(MedCB.SelectedIndex, selected, note))
            {
                this.Close();
            } else
            {
                MedCB.Foreground = new SolidColorBrush(Color.FromRgb(254, 93, 122));
                AllergyTB.Visibility = Visibility.Visible;
            }
        }

        private void MedCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(viewModel.AllergyCheck(MedCB.SelectedIndex))
            {
                MedCB.Foreground = new SolidColorBrush(Color.FromRgb(85, 85, 87));
                AllergyTB.Visibility = Visibility.Hidden;
            } else
            {
                MedCB.Foreground = new SolidColorBrush(Color.FromRgb(254, 93, 122));
                AllergyTB.Visibility = Visibility.Visible;
            }
        }
    }
}
