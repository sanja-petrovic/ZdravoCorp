using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using ZdravoKlinika.Repository;

namespace ZdravoKlinika.View.Manager.ViewModel
{
    public class ManagerFormScheduleViewModel : ManagerViewModelBase
    {
        private DateTime selected;
        DoctorRepository doctorRepository;
        List<Doctor> doctors;

        public DateTime Selected { get => selected; set => selected = value; }
        public List<Doctor> Doctors { get => doctors; set => doctors = value; }

        public ManagerFormScheduleViewModel()
        {
            Selected = DateTime.Today;
            doctorRepository = new DoctorRepository();
            Doctors = doctorRepository.GetAll();
        }
    }
}
