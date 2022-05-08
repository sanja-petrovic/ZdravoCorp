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
        public ObservableCollection<ITabViewModel> TabViewModels { get; set; }
        private int appointmentId;
        private ITabViewModel selectedViewModel;
        public ApptLogViewModel()
        {
            TabViewModels = new ObservableCollection<ITabViewModel>();
        }

        public void Init()
        {
            TabViewModels.Add(new AnamnesisTab { Header = "Anamneza", AppointmentId = appointmentId }) ;
            TabViewModels.Add(new TherapyTab { Header = "Terapija", AppointmentId = appointmentId });
            TabViewModels.Add(new ReferralTab { Header = "Uput za specijalistu", AppointmentId = appointmentId });
            selectedViewModel = TabViewModels[0];
            TabViewModels[0].Load();
        }

        public ITabViewModel SelectedViewModel { get => selectedViewModel; set => SetProperty(ref selectedViewModel, value); }
        public int AppointmentId { get => appointmentId; set => SetProperty(ref appointmentId, value); }
    }
}
