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
        Model.AppointmentsListViewModel viewModel;

        public AppointmentsTabPanel()
        {
            viewModel = new Model.AppointmentsListViewModel();
            DataContext = viewModel;
            InitializeComponent();
            //https://stackoverflow.com/questions/5650812/how-do-i-bind-a-tabcontrol-to-a-collection-of-viewmodels
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
                viewModel.DateTime = value;
            }
        }

    }
}
