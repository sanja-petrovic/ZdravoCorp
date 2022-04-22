using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Data_Handler;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.Repository
{
    public class GuestPatientRepository
    {
        private GuestPatientDataHandler guestPatientDataHandler;
        private List<GuestPatient> guests;

        public List<GuestPatient> Guests
        {
            get
            {
                if (guests == null)
                    guests = new List<GuestPatient>();
                return guests;
            }
            set
            {
                RemoveAllGuestPatient();
                if (value != null)
                {
                    foreach (GuestPatient oGuests in value)
                        AddGuestPatient(oGuests);
                }
            }
        }
        public GuestPatientDataHandler GuestPatientDataHandler { get => guestPatientDataHandler; set => guestPatientDataHandler = value; }

        public GuestPatientRepository()
        { 
            GuestPatientDataHandler = new GuestPatientDataHandler();
            Guests = GuestPatientDataHandler.Read();
        }

        public void AddGuestPatient(GuestPatient newGuest)
        {
            if (newGuest == null)
                return;
            if (this.guests == null)
                this.guests = new List<GuestPatient>();
            if (!this.guests.Contains(newGuest))
                this.guests.Add(newGuest);
        }
        public void RemoveGuestPatient(GuestPatient oldGuest)
        {
            if (oldGuest == null)
                return;
            if (this.guests != null)
                if (this.guests.Contains(oldGuest))
                    this.guests.Remove(oldGuest);
        }
        public void RemoveAllGuestPatient()
        {
            if (guests != null)
                guests.Clear();
        }

        public List<GuestPatient> GetAll() 
        {
            return guests;
        }

        public GuestPatient GetById(String id) 
        {
            foreach (GuestPatient guest in Guests)
            {
                if (guest.PersonalId.Equals(id))
                {
                    return guest;
                }
            }
            return null;
        }
    }
}
