using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace ZdravoKlinika.View.PatientPages.ViewModel
{
    public class PatientAppointmentReviewViewModel : INotifyPropertyChanged
    {
        public PatientAppointmentReviewViewModel()
        {
            LoadQuestions();
            RadioButtonCommand = new MyICommand(RadioButtonClicked);
            AddCommand = new MyICommand(AddGrading, CanExecuteAddGrading);
            appointmentController = new AppointmentController();
        }

        private int appointmentId;
        private List<Question> questionsAndGroups;
        private int[] grades;
        private String selectedContent;
        private AppointmentController appointmentController;

        public MyICommand AddCommand { get; private set; }
        public MyICommand RadioButtonCommand { get; private set; }

        public void RadioButtonClicked(object data)
        {
            int[] values = (int[])data;
            grades[values[0] - 1] = values[1];
        }
        public void AddGrading(object data)
        {
            appointmentController.AddGrading(appointmentId, grades);
            MessageBox.Show("Vasa ocena je zabelezena", "ocenjivanje", MessageBoxButton.OK);
            resetBaseView();

        }
        public bool CanExecuteAddGrading(object data)
        {
            foreach(int i in grades)
            {
                if (i == 0) return false;
            }
            return true;
        }

        public List<Question> QuestionsAndGroups { get => questionsAndGroups; set => questionsAndGroups = value; }
        public int[] Grades { get => grades; set => grades = value; }
        
        private void OnPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        public void LoadQuestions()
        {
            List<Question> questions = new List<Question>();
            questions.Add(new Question("1. Lekar je bio ljubazan.","_1"));
            questions.Add(new Question("2. Lekar je bio profesionalan.", "_2"));
            questions.Add(new Question("3. Lekar mi je pomogao sa mojim problemom.", "_3"));
            questions.Add(new Question("4. Ordinacija je bila adekvatno opremljena.", "_4"));
            questions.Add(new Question("5. Higijena prostorije i opreme je zadovoljavajuca.", "_5"));
            questions.Add(new Question("6. Pregled je obavljen u zakazano vreme uz minimalno čekanje.", "_6"));
            questions.Add(new Question("7. Zadovoljan sam uslugom lekara.", "_7"));
            questions.Add(new Question("8. Zadovoljan sam uslugom bolnice.", "_8"));
            questionsAndGroups = questions;
            grades = new int[questions.Count];
            for(int i = 0; i < grades.Count(); i++)
            {
                grades[i] = 0;
            }
        }
        
        
        public string SelectedContent { get => selectedContent; set => selectedContent = value; }
        public AppointmentController AppointmentController { get => appointmentController; set => appointmentController = value; }
        public int AppointmentId { get => appointmentId; set => appointmentId = value; }

       
        public class Question
        {
            String content;
            String radioGroup;
            public Question(String content, String radioGroup)
            {
                this.content = content;
                this.radioGroup = radioGroup;
            }
            public string Content { get => content; set => content = value; }
            public string RadioGroup { get => radioGroup; set => radioGroup = value; }

        }
        private void resetBaseView()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.Name == "patientBase")
                {
                    PatientViewBase baseWindow = (PatientViewBase)window;
                    baseWindow.refreshAppointmentView();
                }
            }
        }

    }
}
