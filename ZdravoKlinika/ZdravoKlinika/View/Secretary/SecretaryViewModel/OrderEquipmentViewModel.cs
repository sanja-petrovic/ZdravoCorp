using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.View.Secretary.SecretaryViewModel
{
    internal class OrderEquipmentViewModel : ViewModelBase
    {
        private String testic = "";

        public OrderEquipmentViewModel()
        {

        }
        public string Testic { get => testic; set => SetProperty(ref testic, value); }
    }
}
