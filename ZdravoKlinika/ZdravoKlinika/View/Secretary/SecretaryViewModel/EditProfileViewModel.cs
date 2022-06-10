using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ZdravoKlinika.View.Secretary.SecretaryViewModel
{
    public class EditProfileViewModel : ViewModelBase
    {
        RegisteredUser user;
        String gender;
        private ICommand editCommand;
        private Visibility promt;

        public EditProfileViewModel(RegisteredUser user) 
        {
            this.User = user;
            Gender = user.GenderToString();
            Promt = Visibility.Collapsed;
        }

        public RegisteredUser User { get => user; set => user = value; }
        public string Gender { get => gender; set => gender = value; }
        public ICommand EditCommand
        {
            get
            {
                return editCommand ?? (editCommand = new MyICommand(() => EditProfile(), () => EditCommandCanExecute));
            }
        }

        private void EditProfile()
        {
            if(Promt == Visibility.Visible) 
            {
                Promt = Visibility.Collapsed;
            }
            else 
            {
                Promt = Visibility.Visible;
            }
        }

        public bool EditCommandCanExecute { get => true; }
        public Visibility Promt { get => promt; set => SetProperty(ref promt, value ); }
    }
}
