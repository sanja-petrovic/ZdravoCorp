using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.View.DialogHelper
{
    public interface IDialogService
    {
        void ShowDialog(string name, Action<string> callback);
    }
}
