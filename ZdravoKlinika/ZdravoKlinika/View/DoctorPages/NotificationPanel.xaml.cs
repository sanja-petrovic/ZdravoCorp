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
    /// Interaction logic for NotificationPanel.xaml
    /// </summary>
    public partial class NotificationPanel : UserControl
    {
        private Model.NotifPanelViewModel viewModel;
        public NotificationPanel()
        {
            ViewModel = new NotifPanelViewModel();
            DataContext = ViewModel;
            InitializeComponent();
        }

        public NotifPanelViewModel ViewModel { get => viewModel; set => viewModel = value; }
    }
}
