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
using ZdravoKlinika.View.DoctorPages.Model;

namespace ZdravoKlinika.View.DoctorPages
{
    /// <summary>
    /// Interaction logic for CreateApptSchedule.xaml
    /// </summary>
    public partial class CreateApptSchedule : Window
    {

        private AppointmentViewModel viewModel;
        public CreateApptSchedule(Doctor doctor)
        {
            viewModel = new AppointmentViewModel() { DoctorName = doctor.ToString() };
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}
