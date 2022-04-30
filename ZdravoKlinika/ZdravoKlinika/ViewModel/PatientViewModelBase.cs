﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ZdravoKlinika.View;

namespace ZdravoKlinika.ViewModel
{
    internal class PatientViewModelBase : INotifyPropertyChanged
    {
        private String patientId;
        private ImageSource logoImageSource = new BitmapImage(new Uri(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + Path.DirectorySeparatorChar + "Resources" + Path.DirectorySeparatorChar + "Images" + Path.DirectorySeparatorChar + "PatientViewLogo.png"));
        public ImageSource LogoImageSource 
        { 
            get 
            { 
                return this.logoImageSource; 
            }
            set
            {
                if(this.logoImageSource != value)
                {
                    this.logoImageSource = value;
                    NotifyPropertyChanged("logoImageSource");
                }
               
            }
        }

        private ImageSource srbFlagImageSource = new BitmapImage(new Uri(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + Path.DirectorySeparatorChar + "Resources" + Path.DirectorySeparatorChar + "Images" + Path.DirectorySeparatorChar + "IconFlagRs.png"));

        public ImageSource SrbFlagImageSource
        {
            get
            {
                return this.srbFlagImageSource;
            }
            set
            {
                if(this.srbFlagImageSource != value)
                {
                    this.srbFlagImageSource = value;
                    NotifyPropertyChanged("srbFlagImageSource");
                }
            }
        }

        private ImageSource ukFlagImageSource = new BitmapImage(new Uri(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + Path.DirectorySeparatorChar + "Resources" + Path.DirectorySeparatorChar + "Images" + Path.DirectorySeparatorChar + "IconFlagUk.png"));

        public ImageSource UkFlagImageSource
        {
            get
            {
                return this.ukFlagImageSource;
            }
            set
            {
                if (this.ukFlagImageSource != value)
                {
                    this.ukFlagImageSource = value;
                    NotifyPropertyChanged("ukFlagImageSource");
                }
            }
        }

        private PatientProfile profilePage = new PatientProfile();
        public PatientProfile ProfilePage
        {
            get { return this.profilePage; }
            set
            {
                    this.profilePage = value;
            }
        }

        private PatientAppointmentView patientApointmentView = new PatientAppointmentView("12345");
        public PatientAppointmentView PatientApointmentView
        {
            get { return this.patientApointmentView; }
            set { this.patientApointmentView = value; }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
