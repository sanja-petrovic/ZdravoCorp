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
using ZdravoKlinika.View.Secretary.SecretaryViewModel;

namespace ZdravoKlinika.View.Secretary
{
    /// <summary>
    /// Interaction logic for SecretaryProcessTimeOffRequests.xaml
    /// </summary>
    public partial class SecretaryProcessTimeOffRequests : Page
    {
        public SecretaryProcessTimeOffRequests(RegisteredUser usr)
        {
            DataContext = new TimeOffViewModel(usr);
            InitializeComponent();
        }
    }
}
