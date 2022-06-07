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
    /// <summary>
    /// Interaction logic for AnamnesisView.xaml
    /// </summary>
    public partial class AnamnesisView : UserControl
    {
        private AnamnesisTab viewModel;
        private int selectedAppointmentId;
        public AnamnesisView()
        {
            InitializeComponent();
        }

        public AnamnesisView(AnamnesisTab model)
        {
            this.viewModel = model;
            this.viewModel.Load();
            DataContext = model;
            InitializeComponent();
        }

        public int SelectedAppointmentId { get => selectedAppointmentId; set => selectedAppointmentId = value; }
        public AnamnesisTab ViewModel { get => viewModel; set => viewModel = value; }
    }
}
