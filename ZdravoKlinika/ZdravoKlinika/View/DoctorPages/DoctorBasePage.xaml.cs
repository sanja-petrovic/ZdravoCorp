using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using ZdravoKlinika.Controller;
using ZdravoKlinika.Util;
using ZdravoKlinika.View.DoctorPages.Model;

namespace ZdravoKlinika.View.DoctorPages
{
    /// <summary>
    /// Interaction logic for DoctorBasePage.xaml
    /// </summary>
    public partial class DoctorBasePage : Window
    {
        Model.MainViewModel viewModel;

        public DoctorBasePage()
        {
            InitializeComponent();
            this.ViewModel = new Model.MainViewModel();
            DataContext = this.ViewModel;
        }

        public MainViewModel ViewModel { get => viewModel; set => viewModel = value; }
    }
}
