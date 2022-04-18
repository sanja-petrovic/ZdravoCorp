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
    public partial class AppointmentInfo : UserControl
    {
        private Model.AppointmentViewModel viewModel;
        private DateTime dateTime;

        public AppointmentInfo()
        {

            InitializeComponent();
        }
        public AppointmentInfo(DateTime selected)
        {
            viewModel = new Model.AppointmentViewModel();
            this.dateTime = selected;
            setViewModel();
            DataContext = viewModel;
            InitializeComponent();
        }

        public void setViewModel()
        {
            this.viewModel.DoctorId = viewModel.DoctorId;
            this.viewModel.DateTime = this.dateTime;
            
        }
    }
}
