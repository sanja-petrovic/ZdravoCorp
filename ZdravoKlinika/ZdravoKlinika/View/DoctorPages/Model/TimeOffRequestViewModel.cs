﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Controller;
using ZdravoKlinika.Util;

namespace ZdravoKlinika.View.DoctorPages.Model
{
    public class TimeOffRequestViewModel : ViewModelBase
    {
        private DateTime start;
        private DateTime end;
        private bool emergency;
        private Doctor doctor;
        private string reason;
        private string emergencyString;
        private string startString;
        private string endString;
        private string status;

        private TimeOffRequestController controller;

        public TimeOffRequestViewModel(Doctor doctor)
        {
            this.doctor = doctor;
            this.start = DateTime.Today.AddDays(2);
            this.end = DateTime.Today.AddDays(2);
            this.controller = new TimeOffRequestController();
        }

        public DateTime Start { get => start; set => SetProperty(ref start, value); }
        public DateTime End { get => end; set => SetProperty(ref end, value);  }
        public bool Emergency { get { return emergency; } set { SetProperty(ref emergency, value); SetEmergencyString(); } }
        public Doctor Doctor { get => doctor; set => SetProperty(ref doctor, value); }
        public string Reason { get => reason; set => SetProperty(ref reason, value); }
        public string EmergencyString { get => emergencyString; set => SetProperty(ref emergencyString, value); }
        public string StartString { get => startString; set => SetProperty(ref startString, value); }
        public string EndString { get => endString; set => SetProperty(ref endString, value); }
        public string Status { get => status; set => SetProperty(ref status, value); }

        public void CreateRequest()
        {
            this.controller.CreateRequest(Doctor, Start, End, Reason, ZdravoKlinika.Model.RequestState.Pending, Emergency);
        }

        public void SetStartDateString()
        {
            StartString = Start.Date.ToString("dd.MM.yyyy.");
        }

        public void SetEndDateString()
        {
            EndString = End.Date.ToString("dd.MM.yyyy.");
        }

        public void SetEmergencyString()
        {
            if(emergency)
            {
                EmergencyString = "Da";
            } else
            {
                EmergencyString = "Ne";
            }
        }

        public bool CheckAppointments()
        {
            DateBlock period = new DateBlock(Start, End);
            return this.controller.HasScheduledAppointments(Doctor.PersonalId, period);
        }

        public bool CheckDuplicate()
        {
            DateBlock period = new DateBlock(Start, End);
            return this.controller.HasAlreadyMadeRequest(period, Doctor.PersonalId);
        }

        public bool CheckRequests()
        {
            DateBlock period = new DateBlock(Start, End);
            bool retVal = this.controller.IsAnotherSpecialistOff(period, Doctor.Specialty);
            if(emergency == true)
            {
                retVal = false;
            }

            return retVal;
        }

        public void Save()
        {
            SetEmergencyString();
            CreateRequest();
        }
    }
}