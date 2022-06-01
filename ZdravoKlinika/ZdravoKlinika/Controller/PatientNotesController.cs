using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using ZdravoKlinika.Service;

namespace ZdravoKlinika.Controller
{
    public class PatientNotesController
    {
        private PatientNotesService service;

        public PatientNotesController()
        {
            service = new PatientNotesService();
        }

        public List<PatientNotes> GetAll()
        {
            return service.GetAll();
        }
        public PatientNotes GetById(int id)
        {
            return service.GetById(id);
        }
        public List<PatientNotes> GetByPatientId(String id)
        {
            return service.GetByPatientId(id);
        }
        public void CreateNote(RegisteredUser user, String note, DateTime time)
        {
            service.CreateNote(new PatientNotes(-1,time,user,note));
        }
        public void DeleteNote(int id)
        {
            service.DeleteNote(id);
        }
        public void DeleteAllNotes()
        {
            service.DeleteAllNotes();
        }
        public void UpdateNote(int id, RegisteredUser user, String note, DateTime time)
        {
            service.UpdateNote(new PatientNotes(id,time,user,note));
        }
    }
}
