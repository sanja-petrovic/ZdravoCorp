using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.View.Model
{
    internal class DoctorViewModel : ViewModelBase
    {
        private string name;
        private string specialty;

        public DoctorViewModel()
        {
            
        }

        public string Name { get => name; set => SetProperty(ref name, value); }
        public string Specialty { get => specialty; set => SetProperty(ref specialty, value); }
    }
}
