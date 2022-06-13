﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using ZdravoKlinika.Repository;

namespace ZdravoKlinika.View.Manager.ViewModel
{
    public class ManagerScheduleViewModel : ManagerViewModelBase
    {
        private ManagerFormScheduleViewModel formScheduleViewModel = new ManagerFormScheduleViewModel();
        DoctorRepository doctorRepository;
        List<Doctor> doctors;
        private DateTime selected;

        public DateTime Selected { get => selected; set => selected = value; }
        public List<Doctor> Doctors { get => doctors; set => doctors = value; }

        public ManagerScheduleViewModel()
        {
            Selected = DateTime.Today;
            doctorRepository = new DoctorRepository();
            Doctors = doctorRepository.GetAll();
        }

        public ManagerFormScheduleViewModel FormScheduleViewModel
        {
            get { return formScheduleViewModel; }
            set
            {
                SetProperty(ref formScheduleViewModel, value);
            }
        }
    }
}
