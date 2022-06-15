using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ZdravoKlinika.View.Secretary.SecretaryViewModel
{
    public class WizzardViewModel : ViewModelBase
    {
        String imageSource;
        String title;
        String buttonText;
        private ICommand nextCommand;
        int currentPage;
        Wizzard wizzard;
        public WizzardViewModel(Wizzard wizzard) 
        {
            imageSource = @"/Resources/Images/1.png";
            title = "Dugmad za meni";
            currentPage = 1;
            buttonText = "Dalje";
            this.wizzard = wizzard;
        }

        public string ImageSource { get => imageSource; set => SetProperty(ref imageSource , value); }
        public string Title { get => title; set => SetProperty(ref title, value); }
        public ICommand NextCommand
        {
            get
            {
                return nextCommand ?? (nextCommand = new MyICommand(() => NextWizzardPage(), () => NextCanExecute));
            }
        }

        private void NextWizzardPage()
        {
            if (currentPage == 1)
            {
                ImageSource = @"/Resources/Images/2.png";
                Title = "Hamburger meni";
                currentPage = 2;
                return;
            }
            else if (currentPage == 2)
            {
                ImageSource = @"/Resources/Images/3.png";
                Title = "Notifikacije";
                currentPage = 3;
                return;
            }
            else if (currentPage == 3)
            {
                ImageSource = @"/Resources/Images/4.png";
                Title = "Podesavanja";
                ButtonText = "Zavrsi";
                currentPage = 4;
                return;
            }
            else if (currentPage == 4)
            {
                wizzard.Close();
            }
        }

        public bool NextCanExecute { get => true; }
        public string ButtonText { get => buttonText; set => SetProperty(ref buttonText, value); }
    }
}
