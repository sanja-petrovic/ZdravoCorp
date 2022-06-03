using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Data_Handler;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.Repository
{
    public class GuestPatientRepository : Interfaces.IGuestPatientRepository
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
                RemoveAll();
                if (value != null)
                {
                    foreach (GuestPatient oGuests in value)
                        Add(oGuests);
                }
            }
        }
        public GuestPatientDataHandler GuestPatientDataHandler { get => guestPatientDataHandler; set => guestPatientDataHandler = value; }

        public GuestPatientRepository()
        { 
            GuestPatientDataHandler = new GuestPatientDataHandler();
            guests = GuestPatientDataHandler.Read();
        }

        public void Add(GuestPatient newGuest)
        {
            if (newGuest == null)
                return;
            if (this.guests == null)
                this.guests = new List<GuestPatient>();
            if (!this.guests.Contains(newGuest))
                this.guests.Add(newGuest);
            GuestPatientDataHandler.Write(Guests);
        }
        public void Remove(GuestPatient oldGuest)
        {
            if (oldGuest == null)
                return;
            if (this.guests != null)
                if (this.guests.Contains(oldGuest))
                    this.guests.Remove(oldGuest);
            GuestPatientDataHandler.Write(Guests);
        }
        public void RemoveAll()
        {
            if (guests != null)
                guests.Clear();
            GuestPatientDataHandler.Write(Guests);
        }

        public List<GuestPatient> GetAll() 
        {
            return guests;
        }

        public GuestPatient? GetById(String id) 
        {
            GuestPatient? guestToReturn = null;
            foreach (GuestPatient guest in Guests)
            {
                if (guest.PersonalId.Equals(id))
                {
                    guestToReturn = guest;
                    break;
                }
            }
            return guestToReturn;
        }

        public void Update(GuestPatient item)
        {
            throw new NotImplementedException();
        }
    }
}
