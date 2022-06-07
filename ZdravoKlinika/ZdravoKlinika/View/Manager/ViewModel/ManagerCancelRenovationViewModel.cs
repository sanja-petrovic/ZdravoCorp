using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.View.Manager.ViewModel
{
    public class ManagerCancelRenovationViewModel : ManagerViewModelBase
    {
        private RenovationController renovationController;
        private ObservableCollection<Renovation> renovations;

        public ObservableCollection<Renovation> Renovations { get => renovations; set => renovations = value; }

        public ManagerCancelRenovationViewModel()
        {
            this.renovationController = new RenovationController();
            Renovations = new ObservableCollection<Renovation>(this.renovationController.GetAll());
        }
    }
}
