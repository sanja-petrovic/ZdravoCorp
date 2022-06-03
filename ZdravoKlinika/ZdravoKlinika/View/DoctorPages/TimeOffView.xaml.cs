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

namespace ZdravoKlinika.View.DoctorPages
{
    public partial class TimeOffView : UserControl
    {
        private AllRequestsViewModel viewModel;
        public TimeOffView()
        {

            this.viewModel = new AllRequestsViewModel();
            DataContext = this.viewModel;
            InitializeComponent();
        }


        public void Load()
        {
            this.viewModel = new AllRequestsViewModel();
            DataContext = this.viewModel;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TimeOffRequestView timeOffRequestView = new TimeOffRequestView();
            timeOffRequestView.ShowDialog();
            this.viewModel.Load();
        }

    }
}
