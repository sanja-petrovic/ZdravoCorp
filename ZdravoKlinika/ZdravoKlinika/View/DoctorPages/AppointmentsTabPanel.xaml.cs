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
using ZdravoKlinika.Model;
using ZdravoKlinika.View.DoctorPages.Model;

namespace ZdravoKlinika.View.DoctorPages
{
    /// <summary>
    /// Interaction logic for AppointmentsTabPanel.xaml
    /// </summary>
    public partial class AppointmentsTabPanel : UserControl
    {
        private DateTime selected;
        ScheduleViewModel viewModel;
        private string clickedPatientId;
        private DoctorSchedule parent;
        private Doctor doctor;

        public AppointmentsTabPanel()
        {
            selected = DateTime.Today;
            viewModel = new ScheduleViewModel();
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
                viewModel.Doctor = this.doctor;
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

        public DoctorSchedule Parent1 { get => parent; set => parent = value; }
        public string ClickedPatientId { get => clickedPatientId; set => clickedPatientId = value; }
        public Doctor Doctor { get => doctor; set => doctor = value; }

        private void GoToRecord(object sender, RoutedEventArgs e)
        {
            var sel = (ScheduleTabItem) TabTab.SelectedItem;
            parent.goToMedicalRecord(sel.PatientId);
        }
        
    }
}
