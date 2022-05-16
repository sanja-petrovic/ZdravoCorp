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
    /// Interaction logic for TherapyView.xaml
    /// </summary>
    public partial class TherapyView : UserControl
    {
        public TherapyView()
        {
            InitializeComponent();
        }

        private void MedCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(DataContext != null)
            {
                if (((TherapyTab)DataContext).AllergyCheck(MedCB.SelectedIndex))
                {
                    MedCB.Foreground = new SolidColorBrush(Color.FromRgb(85, 85, 87));
                    AllergyTB.Visibility = Visibility.Hidden;
                    AddButton.IsEnabled = true;
                }
                else
                {
                    MedCB.Foreground = new SolidColorBrush(Color.FromRgb(254, 93, 122));
                    AllergyTB.Visibility = Visibility.Visible;
                    AddButton.IsEnabled = false;
                }
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            ((TherapyTab)DataContext).Add(MedCB.SelectedIndex);
        }
    }
}
