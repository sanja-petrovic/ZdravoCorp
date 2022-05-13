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
using System.Windows.Shapes;

using ZdravoKlinika.View.DoctorPages.Model;

namespace ZdravoKlinika.View.DoctorPages
{
    /// <summary>
    /// Interaction logic for TimeOffRequestView.xaml
    /// </summary>
    public partial class TimeOffRequestView : Window
    {
        private TimeOffRequestViewModel viewModel;

        public TimeOffRequestView(Doctor doctor)
        {
            this.viewModel = new TimeOffRequestViewModel(doctor);
            DataContext = this.viewModel;
            InitializeComponent();
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            this.viewModel.Save();
            this.Close();
        }
    }
}
