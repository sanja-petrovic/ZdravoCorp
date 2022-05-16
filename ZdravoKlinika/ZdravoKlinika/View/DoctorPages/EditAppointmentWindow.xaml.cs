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
    /// Interaction logic for EditAppointmentWindow.xaml
    /// </summary>
    public partial class EditAppointmentWindow : Window
    {
        private ApptLogViewModel viewModel;
        private int selectedApptId;
        public EditAppointmentWindow()
        {
           
        }

        public int SelectedApptId { get => selectedApptId; set => selectedApptId = value; }

        public void Init()
        {
            //viewModel = new ApptLogViewModel();
            //viewModel.AppointmentId = SelectedApptId;
            //viewModel.Load();
            //DataContext = viewModel;
            InitializeComponent();
        }

        public void Save(object sender, RoutedEventArgs e)
        {
            //viewModel.save(NoteTB.Text);
            this.Close();
        }
    }
}
