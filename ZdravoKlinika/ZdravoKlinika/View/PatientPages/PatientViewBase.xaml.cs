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
using ZdravoKlinika.PatientPages.ViewModel;
using ZdravoKlinika.Util;

namespace ZdravoKlinika.View.PatientPages
{
    /// <summary>
    /// Interaction logic for PatientViewBase.xaml
    /// </summary>
    public partial class PatientViewBase : Window
    {
        private PatientViewModelBase viewModel;
        private String id;
        private int theme;

        public string Id { get => id; set => id = value; }

        public PatientViewBase(string personalId)
        {
            
            InitializeComponent();
            id = personalId;
            viewModel = new PatientViewModelBase(id);
            this.DataContext = viewModel;
            mainFrame.Navigate(viewModel.ProfilePage);
            //lightBulb.Source = viewModel.LightbulbWhite;
            theme = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(viewModel.ProfilePage);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(viewModel.PatientApointmentView);
        }
        public void refreshAppointmentView()
        {
            viewModel.PatientApointmentView = new PatientAppointmentView(id);
            mainFrame.Navigate(viewModel.PatientApointmentView);
        }
        public void refreshProfileView()
        {
            mainFrame.Navigate(viewModel.ProfilePage);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            viewModel.ApplicationReviewView = new ApplicationReviewView(id);
            mainFrame.Navigate(viewModel.ApplicationReviewView);
        }

        private void Button_Terapija_Click(object sender, RoutedEventArgs e)
        {
            viewModel.PatientTherapyView = new PatientTherapyView(id);
            mainFrame.Navigate(viewModel.PatientTherapyView);
        }

        private void Button_Beleske_Click(object sender, RoutedEventArgs e)
        {
            viewModel.PatientNotesView = new PatientNotesView(id);
            mainFrame.Navigate(viewModel.PatientNotesView);
        }

        private void Button_Theme_Click(object sender, RoutedEventArgs e)
        {
            switch (theme)
            {
                case 0:
                    Application.Current.Resources["menuBackgroundActive"] = FindResource("menuBackgroundDark");
                    Application.Current.Resources["sliderButtonActive"] = FindResource("sliderButtonOff");
                    lightBulb.Source = viewModel.LightbulbBlack;
                    Application.Current.Resources["menuButtonActive"] = FindResource("menuButtonDark");
                    Application.Current.Resources["regularBackgroundActive"] = FindResource("regularBackgroundDark");
                    Application.Current.Resources["backgroundActive"] = FindResource("backgroundDark");
                    Application.Current.Resources["titleActive"] = FindResource("titleDark");
                    Application.Current.Resources["regularButtonActive"] = FindResource("regularButtonDark");
                    theme = 1;
                    break;
                case 1:
                    Application.Current.Resources["menuBackgroundActive"] = FindResource("menuBackgroundLight");
                    Application.Current.Resources["sliderButtonActive"] = FindResource("sliderButtonOn");
                    lightBulb.Source = viewModel.LightbulbWhite;
                    Application.Current.Resources["menuButtonActive"] = FindResource("menuButtonLight");
                    Application.Current.Resources["regularBackgroundActive"] = FindResource("regularBackgroundLight");
                    Application.Current.Resources["backgroundActive"] = FindResource("backgroundLight");
                    Application.Current.Resources["titleActive"] = FindResource("titleLight");
                    Application.Current.Resources["regularButtonActive"] = FindResource("regularButtonLight");
                    theme = 0;
                    break;
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (languageComboBox.SelectedIndex)
            {
                case 0: //EN
                    TranslationSource.SetLanguage("en");
                    break;
                case 1: //SRB
                    TranslationSource.SetLanguage("el-GR");
                    break;
            }
        }
    }
}
