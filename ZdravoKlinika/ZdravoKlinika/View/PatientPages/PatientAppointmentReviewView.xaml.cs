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
using ZdravoKlinika.View.PatientPages.ViewModel;

namespace ZdravoKlinika.View.PatientPages
{
    /// <summary>
    /// Interaction logic for PatientAppointmentReviewView.xaml
    /// </summary>
    public partial class PatientAppointmentReviewView : Page
    {
        public PatientAppointmentReviewView(int appointmentId)
        {
            InitializeComponent();
            ((PatientAppointmentReviewViewModel)DataContext).AppointmentId = appointmentId;
        }
    }
}
