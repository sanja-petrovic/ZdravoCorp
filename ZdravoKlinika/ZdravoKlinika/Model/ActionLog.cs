using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.Model
{
    internal class ActionLog
    {
        private String id;
        private DateTime time;
        private String action;

        private RegisteredPatient registeredPatient;

        public ActionLog() { }
        public ActionLog(String id, DateTime time, String action, RegisteredPatient patient)
        {
            this.id = id;
            this.time = time;
            this.action = action;
            this.RegisteredPatient = patient;
        }
        public string Id { get => id; set => id = value; }
        public DateTime Time { get => time; set => time = value; }
        public string Action { get => action; set => action = value; }
        public RegisteredPatient RegisteredPatient { get => registeredPatient; set => registeredPatient = value; }
    }
}
