using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.ViewModel
{
    internal class PatientProfileViewModel : INotifyPropertyChanged
    {
        private string name;
        private string lastName;
        private string parentName;
        private string dateOfBirth;
        private string gender;
        private string phoneNumber;
        private string email;
        private string profilePictureLocation;
        private string bloodType;
        private string ocupation;
        private string emergacyContact;
        private string emergencyNumber;

        private List<String> alergies;
        private List<String> diagnoses;

        //private List<PatientMedicationNotification> notifs;

        public PatientProfileViewModel(MedicalRecord record)
        {
            //TODO
            //this.name = record.
            //etc etc
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
