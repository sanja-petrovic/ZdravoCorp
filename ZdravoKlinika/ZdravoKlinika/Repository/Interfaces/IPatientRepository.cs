using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.Repository.Interfaces
{
    internal interface IPatientRepository : IRepositoryBase<Model.IPatient, String>
    {
        public void CreateNewGuestPatient(Model.GuestPatient guestPatient);
    }
}
