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

namespace ZdravoKlinika.View.Manager.Views
{
    /// <summary>
    /// Interaction logic for ManagerTutorialWindow.xaml
    /// </summary>
    public partial class ManagerTutorialWindow : Window
    {
        public ManagerTutorialWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void PlayMedia(object sender, RoutedEventArgs e)
        {
            myMediaElement.Play();
            InitializePropertyValues();
        }

        private void PauseMedia(object sender, RoutedEventArgs e)
        {
            myMediaElement.Pause();
        }

        private void RestartMedia(object sender, RoutedEventArgs e)
        {
            myMediaElement.Stop();
        }

        private void ChangeMediaVolume(object sender, RoutedPropertyChangedEventArgs<double> args)
        {
            myMediaElement.Volume = (double)volumeSlider.Value;
        }

        private void Element_MediaEnded(object sender, EventArgs e)
        {
            myMediaElement.Stop();
        }

        private void InitializePropertyValues()
        { 
            myMediaElement.Volume = (double)volumeSlider.Value;
        }    
    }
}
