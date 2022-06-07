using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using ZdravoKlinika.Repository;

namespace ZdravoKlinika.Service
{
    public class PatientNotesService
    {
        private PatientNotesRepository repository;

        public PatientNotesRepository Repository { get => repository; set => repository = value; }

        public PatientNotesService()
        {
            repository = new PatientNotesRepository();
        }
        public List<PatientNotes> GetAll()
        {
            return repository.GetAll();
        }
        public PatientNotes GetById(int id)
        {
            return repository.GetById(id);
        }
        public List<PatientNotes> GetByPatientId(String id)
        {
           return repository.GetAll().FindAll(items => items.Receiver.PersonalId == id);
        }
        public List<PatientNotes> GetForDate(string id, DateTime date)
        {
            return this.GetByPatientId(id).FindAll(items => items.Trigger.Date.Equals(date.Date));
        }
        public List<PatientNotes> GetUpcommingNotes(String id, int hours)
        {
            return this.GetForDate(id, DateTime.Now).FindAll(items => items.Trigger > DateTime.Now && items.Trigger < DateTime.Now.AddHours(hours));
        }
        public void CreateNote(PatientNotes note) 
        {
            note.NotificationId = GetAvailableId();
            repository.Add(note);
        }
        public void DeleteNote(int id)
        {
            repository.Remove(repository.GetById(id));
        }
        public void DeleteAllNotes()
        {
            repository.RemoveAll();
        }
        public void UpdateNote(PatientNotes note)
        {
            repository.Update(note);
        }
        private int GetAvailableId()
        {
            int retVal;
            List<PatientNotes> patientNotes = repository.GetAll();
            if (patientNotes.Count > 0)
            {
               retVal = patientNotes.Last().NotificationId + 1;

            }
            else
            {
                retVal = 1;
            }
            return retVal;
        }
    }
}
