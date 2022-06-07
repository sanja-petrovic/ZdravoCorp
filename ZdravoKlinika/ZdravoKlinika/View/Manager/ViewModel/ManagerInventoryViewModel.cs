using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.View.Manager.ViewModel
{
    public class ManagerInventoryViewModel : ManagerViewModelBase
    {
        private ManagerMedicationViewModel medicationViewModel = new ManagerMedicationViewModel();
        private ManagerOrderViewModel orderViewModel = new ManagerOrderViewModel();
        private ManagerViewModelBase currentViewModel;
        private EquipmentController equipmentController;
        private ObservableCollection<Equipment> equipment;

        public ObservableCollection<Equipment> Equipment { get => equipment; set => equipment = value; }

        public ManagerInventoryViewModel()
        {
            this.equipmentController = new EquipmentController();
            Equipment = new ObservableCollection<Equipment>(this.equipmentController.GetAll());
        }

        public ManagerViewModelBase CurrentViewModel
        {
            get { return currentViewModel; }
            set
            {
                SetProperty(ref currentViewModel, value);
            }
        }

        public ManagerMedicationViewModel MedicationViewModel
        {
            get { return medicationViewModel; }
            set
            {
                SetProperty(ref medicationViewModel, value);
            }
        }

        public ManagerOrderViewModel OrderViewModel
        {
            get { return orderViewModel; }
            set
            {
                SetProperty(ref orderViewModel, value);
            }
        }

    }
}
