using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace ZdravoKlinika.View
{
    /// <summary>
    /// Interaction logic for AppointmentView.xaml
    /// </summary>
    public partial class AppointmentView : Window
    {
        private DateTime dateAndTime;
        private String patientName;
        private String type;
        private String emergency;

        public AppointmentView()
        {
            InitializeComponent();
            DataContext = this;
        }

        public DateTime DateAndTime
        {
            get => dateAndTime;
            set
            {
                if(dateAndTime != value)
                {
                    dateAndTime = value;
                    OnPropertyChanged();
                }
            }
        }


        public String PatientName
        {
            get => patientName;
            set
            {
                if(patientName != value)
                {
                    patientName = value;
                    OnPropertyChanged();
                }
            }
        }

        public String Type
        {
            get => type;
            set
            {
                if(type != value)
                {
                    type = value;
                    OnPropertyChanged();
                }
            }
        }

        public String Emergency
        {
            get => emergency;
            set
            {
                if(emergency != value)
                {
                    emergency = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
