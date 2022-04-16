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
using System.Windows.Navigation;

namespace ZdravoKlinika
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class SignInWindow : Window
    {
        public SignInWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            View.DoctorPages.DoctorSchedule doctorSchedule = new View.DoctorPages.DoctorSchedule();
            MainFrame.Navigate(doctorSchedule); 

        }

    }
}
