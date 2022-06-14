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
using ZdravoKlinika.View.Secretary.SecretaryViewModel;

namespace ZdravoKlinika.View.Secretary
{
    /// <summary>
    /// Interaction logic for Wizzard.xaml
    /// </summary>
    public partial class Wizzard : Window
    {
        public Wizzard()
        {
            Wizzard wizzard = this;
            DataContext = new WizzardViewModel(wizzard);
            InitializeComponent();
        }
    }
}
