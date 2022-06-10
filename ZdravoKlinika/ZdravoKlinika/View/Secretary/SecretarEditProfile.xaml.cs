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

namespace ZdravoKlinika.View.Secretary
{
    /// <summary>
    /// Interaction logic for SecretarEditProfile.xaml
    /// </summary>
    public partial class SecretarEditProfile : Page
    {
        public SecretarEditProfile(RegisteredUser usr)
        {
            InitializeComponent();
            DataContext = new SecretaryViewModel.EditProfileViewModel(usr);
        }
    }
}
