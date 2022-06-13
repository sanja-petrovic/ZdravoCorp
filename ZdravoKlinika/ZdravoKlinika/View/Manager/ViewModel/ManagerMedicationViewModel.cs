using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Controller;

namespace ZdravoKlinika.View.Manager.ViewModel
{
    public class ManagerMedicationViewModel : ManagerViewModelBase
    {
        private MedicationController medicationController;
        private ObservableCollection<Medication> medications;

        public ObservableCollection<Medication> Medications { get => medications; set => medications = value; }

        public ManagerMedicationViewModel()
        {
            this.medicationController = new MedicationController();
            Medications = new ObservableCollection<Medication>(this.medicationController.GetAll());
        }

    }
}
