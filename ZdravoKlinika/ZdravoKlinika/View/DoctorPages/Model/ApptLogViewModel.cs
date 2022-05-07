using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.View.DoctorPages.Model
{
    public class ApptLogViewModel : ViewModelBase
    {
        public ObservableCollection<ITabViewModel> TabViewModels;
        private ITabViewModel selectedViewModel;
        public ApptLogViewModel()
        {
            TabViewModels = new ObservableCollection<ITabViewModel>();
            TabViewModels.Add(new AnamnesisTab { Header = "Anamneza" });
            TabViewModels.Add(new TherapyTab { Header = "Terapija" });
            TabViewModels.Add(new ReferralTab { Header = "Uput za specijalistu" });
            selectedViewModel = TabViewModels[0];
        }

        public ITabViewModel SelectedViewModel { get => selectedViewModel; set => SetProperty(ref selectedViewModel, value); }
    }
}
