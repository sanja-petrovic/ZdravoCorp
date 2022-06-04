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
using ZdravoKlinika.View.DoctorPages.Model;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.View.DoctorPages
{
    public partial class TimeOffView : UserControl
    {
        private AllRequestsViewModel viewModel;
        private Doctor doctor;
        public TimeOffView()
        {
        }

        public void Load(Doctor doctor)
        {
            this.doctor = doctor;
            this.viewModel = new AllRequestsViewModel(doctor);
            DataContext = this.viewModel;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TimeOffRequestView timeOffRequestView = new TimeOffRequestView(this.doctor);
            timeOffRequestView.ShowDialog();
            this.viewModel.Load();
        }

    }
}
