﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using ZdravoKlinika.Util;

namespace ZdravoKlinika.View.DoctorPages
{
    /// <summary>
    /// Interaction logic for DoctorBasePage.xaml
    /// </summary>
    public partial class DoctorBasePage : Window
    {
        Model.DoctorViewModel viewModel;

        private bool settingsVisible = false;
        private DoctorHomePage doctorHomePage;
        private DoctorSchedule doctorSchedule;
        private DoctorMedicationsView doctorMedicationsView;
        private DoctorProfileView doctorProfileView;
        private DoctorAllPatientsView doctorAllPatientsView;
        private FeedbackView feedbackView;

        public DoctorBasePage()
        {
            this.viewModel = new Model.DoctorViewModel();
            DataContext = this.viewModel;
            InitializeComponent();
            doctorHomePage = new DoctorHomePage();
            doctorSchedule = new DoctorSchedule();
            doctorMedicationsView = new DoctorMedicationsView();
            doctorProfileView = new DoctorProfileView();
            doctorAllPatientsView = new DoctorAllPatientsView();
            feedbackView = new FeedbackView();
            MainFrame.Navigate(doctorHomePage);
        }

        private void GoToSchedule(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(doctorSchedule);
        }

        private void GoToHome(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(doctorHomePage);
        }


        private void GoToMeds(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(doctorMedicationsView);
        }

        private void GoToProfile(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(doctorProfileView);
        }

        private void GoToPatients(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(doctorAllPatientsView);
        }

        private void GoToFeedback(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(feedbackView);
        }

        private void SignOut(object sender, MouseButtonEventArgs e)
        {
            RegisteredUserController registeredUserController = new RegisteredUserController();
            registeredUserController.ForgetUser();
            SignInWindow signInWindow = new SignInWindow();
            signInWindow.Show();
            this.Close();
        }

        private void ShowSettings(object sender, MouseButtonEventArgs e)
        {
            Settings.Visibility = settingsVisible ? Visibility.Collapsed : Visibility.Visible;
            settingsVisible = !settingsVisible;
        }
    }
}
