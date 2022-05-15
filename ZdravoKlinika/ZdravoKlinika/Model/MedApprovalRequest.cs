using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.Model
{
    public class MedApprovalRequest
    {
        private int id;
        private Medication medication;
        private Doctor reviewer;
        private bool pending;
        private string comment;

        public Medication Medication { get => medication; set => medication = value; }
        public Doctor Reviewer { get => reviewer; set => reviewer = value; }
        public bool Pending { get => pending; set => pending = value; }
        public string Comment { get => comment; set => comment = value; }
        public int Id { get => id; set => id = value; }

        public MedApprovalRequest() { }

        public MedApprovalRequest(Medication medication, Doctor doctor)
        {
            this.medication = medication;
            this.reviewer = doctor;
            this.pending = false;
        }

    }
}
