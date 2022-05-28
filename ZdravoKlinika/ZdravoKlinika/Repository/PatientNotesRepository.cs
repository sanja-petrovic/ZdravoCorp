using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Data_Handler;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.Repository
{
    public class PatientNotesRepository
    {
        private PatientNotesDataHandler dataHandler;
        private List<PatientNotes> notes;

        public List<PatientNotes> Notes 
        { 
            get
            {
                if (notes == null)
                {
                    notes = new List<PatientNotes>();
                }
                return notes;
            }
            set
            {
                DeleteAllNotes();
                if(value != null)
                {
                    foreach(PatientNotes note in value)
                    {
                        CreateNote(note);
                    }
                }
            }
        }

        public PatientNotesDataHandler DataHandler { get => dataHandler; set => dataHandler = value; }

        public PatientNotesRepository()
        {
            DataHandler = new PatientNotesDataHandler();
            ReadDataFromFiles();
        }

        private void ReadDataFromFiles()
        {
            notes = DataHandler.Read();
            if(notes == null) Notes = new List<PatientNotes>(); 
        }

        public List<PatientNotes> GetAll()
        {
            ReadDataFromFiles();
            return notes;
        }

        public PatientNotes GetById(int id)
        {
            PatientNotes retVal = null;
            foreach(PatientNotes note in Notes)
            {
                if(note.NotificationId == id)
                {
                    retVal = note;
                    break;
                }
            }
            return retVal;
        }
        public void CreateNote(PatientNotes note)
        {
            if(note != null)
            {
                if(Notes == null)
                {
                    Notes = new List<PatientNotes>();
                }
                if(GetById(note.NotificationId) == null)
                {
                    notes.Add(note);
                    dataHandler.Write(Notes);
                }
            }
        }

        public void DeleteNote(PatientNotes note)
        {
            if(Notes != null)
            {
                if(Notes != null)
                {
                    if (Notes.Contains(note))
                    {
                        Notes.Remove(note);
                    }
                }
                DataHandler.Write(Notes);
            }
        }
        public void DeleteAllNotes()
        {
            if (Notes != null)
            {
                Notes.Clear();
            }
        }
        public void UpdateNote(PatientNotes note)
        {
            PatientNotes old = Notes.Find(item => item.NotificationId == note.NotificationId);

            if(old != null)
            {
                Notes[Notes.IndexOf(old)] = note;
                DataHandler.Write(Notes);
            }
        }
    }
}
