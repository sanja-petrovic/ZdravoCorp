using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Controller;

namespace ZdravoKlinika.View.Manager.ViewModel
{
    public class ManagerOrderViewModel : ManagerViewModelBase
    {
        private EquipmentController equipmentController;
        private MedicationController medicationController;
        private ObservableCollection<Equipment> equipment;
        private ObservableCollection<Medication> medications;

        public ObservableCollection<Equipment> Equipment { get => equipment; set => equipment = value; }
        public ObservableCollection<Medication> Medications { get => medications; set => medications = value; }

        public ManagerOrderViewModel()
        {
            this.equipmentController = new EquipmentController();
            Equipment = new ObservableCollection<Equipment>(this.equipmentController.GetAll());

            this.medicationController = new MedicationController();
            Medications = new ObservableCollection<Medication>(this.medicationController.GetAll());
        }
    }
}
