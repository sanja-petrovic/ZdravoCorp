using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ZdravoKlinika.View;
using ZdravoKlinika.View.PatientPages;

namespace ZdravoKlinika.PatientPages.ViewModel
{
    internal class PatientViewModelBase : INotifyPropertyChanged
    {
        private String patientId;

        public PatientViewModelBase( String patientId)
        {
            RegisteredPatientController controller = new RegisteredPatientController();
            this.patientId = patientId;
            profilePage = new PatientProfile(patientId);
            patientApointmentView = new PatientAppointmentView(patientId);
            applicationReviewView = new ApplicationReviewView(patientId);


        }
        private ImageSource logoImageSource = new BitmapImage(new Uri(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + Path.DirectorySeparatorChar + "Resources" + Path.DirectorySeparatorChar + "Images" + Path.DirectorySeparatorChar + "zdravoklinika.png"));
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

        private PatientProfile profilePage;
        public PatientProfile ProfilePage
        {
            get { return this.profilePage; }
            set
            {
                    this.profilePage = value;
            }
        }

        private PatientAppointmentView patientApointmentView;
        private PatientTherapyView patientTherapyView;
        public PatientAppointmentView PatientApointmentView
        {
            get { return this.patientApointmentView; }
            set { this.patientApointmentView = value; }
        }

        private ApplicationReviewView applicationReviewView;
        public string PatientId { get => patientId; set => patientId = value; }
        public ApplicationReviewView ApplicationReviewView { get => applicationReviewView; set => applicationReviewView = value; }
        public PatientTherapyView PatientTherapyView { get => patientTherapyView; set => patientTherapyView = value; }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
