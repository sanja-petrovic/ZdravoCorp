using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Controller;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.View.PatientPages.ViewModel
{
    public class PatientNotesViewModel : INotifyPropertyChanged
    {
        private string patientId;
        private List<PatientNotes> patientNotes;
        private DateTime selectedDateTime;
        private PatientNotesController notesController;
        private RegisteredPatientController patientController;
        private MyICommand loadNotesCommand;
        private MyICommand addNotesCommand;
        private MyICommand editNotesCommand;
        private MyICommand removeNotesCommand;
        private String note;
        private PatientNotes selectedNote;

        public string PatientId { get => patientId; set => patientId = value; }
        public List<PatientNotes> PatientNotes { get => patientNotes; set => patientNotes = value; }
        public DateTime SelectedDateTime { get => selectedDateTime;
            set
            {
                selectedDateTime = value;
                OnPropertyChanged("");
            }
        }

        public PatientNotesController NotesController { get => notesController; set => notesController = value; }
        public MyICommand LoadNotesCommand { get => loadNotesCommand; set => loadNotesCommand = value; }
        public string Note { get => note;
            set
            {
                note = value;
                OnPropertyChanged("");
            }

        }

        public RegisteredPatientController PatientController { get => patientController; set => patientController = value; }
        public MyICommand AddNotesCommand { get => addNotesCommand; set => addNotesCommand = value; }
        public MyICommand EditNotesCommand { get => editNotesCommand; set => editNotesCommand = value; }
        public MyICommand RemoveNotesCommand { get => removeNotesCommand; set => removeNotesCommand = value; }
        public PatientNotes SelectedNote { get => selectedNote; set
            {
                selectedNote = value;
                OnPropertyChanged("");
            }
        }

        public PatientNotesViewModel(String id)
        {
            PatientId = id;
            notesController = new PatientNotesController();
            patientController = new RegisteredPatientController();
            selectedNote = new PatientNotes();
            selectedNote.Trigger = DateTime.Now;
            LoadNotesCommand = new MyICommand(LoadNotes);
            LoadNotesCommand.Execute(this);
            addNotesCommand = new MyICommand(AddNote, CanExecuteAddNote);
            editNotesCommand = new MyICommand(EditNote, CanExecuteEditNote);
            removeNotesCommand = new MyICommand(DeleteNote, CanExecuteDeleteNote);

        }
        public void LoadNotes(object data)
        {
            patientNotes = notesController.GetByPatientId(PatientId);
            OnPropertyChanged("");
        }
      
        private void AddNote(object data)
        {
            notesController.CreateNote(patientController.GetById(patientId), selectedNote.NotificationText, selectedNote.Trigger);
            LoadNotesCommand.Execute(this);
        }
        private bool CanExecuteAddNote(object data)
        {
            bool result = false;
            if(selectedNote != null)
            {
                if (selectedNote.NotificationText != null)
                {
                    if (!selectedNote.NotificationText.Equals(""))
                    {
                        result = true;
                    }
                }
            }
            return result;
        }
        private void EditNote(object data)
        {
            notesController.UpdateNote(selectedNote.NotificationId, patientController.GetById(patientId), selectedNote.NotificationText, selectedNote.Trigger);
            LoadNotesCommand.Execute(this);
        }
        private bool CanExecuteEditNote(object data)
        {
            bool result = false;
            if (selectedNote != null)
            {
                if (selectedNote.NotificationText != null )
                {
                    if (!selectedNote.NotificationText.Equals("") && selectedNote.NotificationId != -1)
                    {
                        result = true;
                    }
                }
            }
                return result;
        }
        private void DeleteNote(object data)
        {
            notesController.DeleteNote(selectedNote.NotificationId);
            LoadNotesCommand.Execute(this);
        }
        private bool CanExecuteDeleteNote(object data)
        {
            bool result = false;
            if (selectedNote != null)
            {
                if(selectedNote.NotificationId != -1)
                {
                    result = true;
                }
            }
            return result;
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
