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

namespace ZdravoKlinika.View.DoctorPages
{
    /// <summary>
    /// Interaction logic for AppointmentsTabPanel.xaml
    /// </summary>
    public partial class AppointmentsTabPanel : UserControl
    {
        private DateTime selected;
        Model.ScheduleViewModel viewModel;

        public AppointmentsTabPanel()
        {
            selected = DateTime.Today;
            viewModel = new Model.ScheduleViewModel();
            viewModel.Selected = selected;
            DataContext = viewModel;
            InitializeComponent();
        }

        public DateTime Selected
        {
            get
            {
                return selected;
            }
            set
            {
                this.selected = value;
                viewModel.Selected = value;
                viewModel.infoChange();
                TabTab.SelectedIndex = 0;
                if(TabTab.Items.Count == 0)
                {
                    HiddenTb.Visibility = Visibility.Visible;
                } else
                {
                    HiddenTb.Visibility = Visibility.Hidden;
                }
            }
        }

    }
}
