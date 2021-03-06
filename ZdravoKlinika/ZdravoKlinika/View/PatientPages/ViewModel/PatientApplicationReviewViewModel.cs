using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ZdravoKlinika.Controller;

namespace ZdravoKlinika.View.PatientPages.ViewModel
{
    public class PatientApplicationReviewViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private List<Question> questionsAndGroups;
        private int[] grades;
        private AppReviewController appReviewController;
        private MyICommand radioButtonCommand;
        private MyICommand addReviewCommand;
        private String comment;
        private String patientId;
        private RegisteredPatientController registeredPatientController;

        public PatientApplicationReviewViewModel()
        {
            LoadQuestions();
            RadioButtonCommand = new MyICommand(RadioButtonClicked);
            AddReviewCommand = new MyICommand(AddReview, CanExecuteAddReview);
            appReviewController = new AppReviewController();
            RegisteredPatientController = new RegisteredPatientController();
        }
        public List<Question> QuestionsAndGroups { get => questionsAndGroups; set => questionsAndGroups = value; }
        public int[] Grades { get => grades; set => grades = value; }
        public MyICommand RadioButtonCommand { get => radioButtonCommand; set => radioButtonCommand = value; }
        public MyICommand AddReviewCommand { get => addReviewCommand; set => addReviewCommand = value; }

        public string Comment { get => comment;
            set
            {
                comment = value;
                AddReviewCommand.RaiseCanExecuteChanged();
            }

        }
        public string PatientId { get => patientId; set => patientId = value; }
        public RegisteredPatientController RegisteredPatientController { get => registeredPatientController; set => registeredPatientController = value; }


        public void LoadQuestions()
        {
            List<Question> questions = new List<Question>();
            questions.Add(new Question("1. Osoblje je prijatno, profesionalno i uvek na usluzi.", "_1"));
            questions.Add(new Question("2. Prostorije za pregled su čiste i opremljene.", "_2"));
            questions.Add(new Question("3. Čekaonice su prijatne i prostrane.", "_3"));
            questions.Add(new Question("4. Muzika u čekaonicama je prijatna i umirujuca.", "_4"));
            questions.Add(new Question("5. Boravak u prostorijama i korišćenje usluga Zdravo korporacije je na mene ostavilo pozitivan uticaj.", "_5"));
            questions.Add(new Question("6. U budućnosti ću se ponovo opredeliti za usluge Zdravo korporacije.", "_6"));
            questions.Add(new Question("7. Aplikacija Zdravo Korporacije je adekvatna.", "_7"));
            questionsAndGroups = questions;
            Grades = new int[questions.Count];
            for (int i = 0; i < Grades.Count(); i++)
            {
                Grades[i] = 0;
            }
        }
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

        private void OnPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void RadioButtonClicked(object data)
        {
            int[] values = (int[])data;
            grades[values[0] - 1] = values[1];
        }

        public void AddReview(object data)
        {
            appReviewController.AddReview(registeredPatientController.GetById(PatientId), DateTime.Now, Grades, comment);
            MessageBox.Show("Vasa ocena je zabelezena", "ocenjivanje", MessageBoxButton.OK);
            resetBaseView();

        }
        public bool CanExecuteAddReview(object data)
        {
            bool retval = true;
            foreach (int i in grades)
            {
                if (i == 0) retval = false;
            }
            if (comment == null || comment=="")
            {
                retval = false;
            }
            return retval;
        }

        private void resetBaseView()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.Name == "patientBase")
                {
                    PatientViewBase baseWindow = (PatientViewBase)window;
                    baseWindow.refreshProfileView();
                }
            }
        }
    }
}
