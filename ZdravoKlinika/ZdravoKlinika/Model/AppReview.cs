using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.Model
{
    internal class AppReview
    {
        private String id;
        private RegisteredPatient registeredPatient;
        private DateTime time;
        private int[] grades;
        private String comment;

        public AppReview() { }

        public AppReview(String id, RegisteredPatient registeredPatient, DateTime time, int[] grades, String comment)
        {
            this.id = id;
            this.RegisteredPatient = registeredPatient;
            this.time = time;
            this.grades = grades;
            this.comment = comment;
        }

        public string Id { get => id; set => id = value; }
        
        public DateTime Time { get => time; set => time = value; }
        public int[] Grades { get => grades; set => grades = value; }
        public string Comment { get => comment; set => comment = value; }
        public RegisteredPatient RegisteredPatient { get => registeredPatient; set => registeredPatient = value; }
    }
}
