using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.View.DoctorPages.Model
{
    public class AnamnesisTab : ViewModelBase, ITabViewModel
    {
        private string header;

        public string Header { get => header; set => SetProperty(ref header, value); }
    }
}
