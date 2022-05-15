using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.View.PatientPages.ViewModel
{
    internal class PatientAppointmentReviewViewModel : INotifyPropertyChanged
    {
        public PatientAppointmentReviewViewModel()
        {
            LoadQuestions();
        }

        private List<Question> questionsAndGroups;

        public List<Question> QuestionsAndGroups { get => questionsAndGroups; set => questionsAndGroups = value; }

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
    }
}
