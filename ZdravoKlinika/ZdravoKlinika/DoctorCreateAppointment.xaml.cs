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

namespace ZdravoKlinika
{
    public partial class DoctorCreateAppointment : Window
    {

        private String type;
        private String time;
        private DateTime date;
        private String patient;
        public DoctorCreateAppointment()
        {
            InitializeComponent();
            DataContext = this;
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

        public String Patient
        {
            get => patient;
            set
            {
                if(patient != value)
                {
                    patient = value;
                    OnPropertyChanged();
                }
            }
        }


        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem ComboItem = (ComboBoxItem)patientCB.SelectedItem;
            var name = ComboItem.Content.ToString();
            string[] patientInfo = name.Split('(');
            string patientName = patientInfo[0];
            string patientId = patientInfo[1].Remove(patientInfo[1].Length - 1);


        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
