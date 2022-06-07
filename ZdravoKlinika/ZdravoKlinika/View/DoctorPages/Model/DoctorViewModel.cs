using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ZdravoKlinika.Controller;

using ZdravoKlinika.Model;

namespace ZdravoKlinika.View.DoctorPages.Model
{
    public class DoctorViewModel : ViewModelBase
    {
        private Visibility isEditVisible;
        private Visibility areButtonsVisible;
        private Doctor doctor;
        private double doctorGrade;
        private string gender;
        private string address;
        private string street;
        private string number;
        private string city;
        private string country;
        private string phone;
        private string email;
        private string dateOfBirth;
        private string properText;
        private string validationError;
        private bool isEditable;
        public DelegateCommand EditCommand { get; set; }
        public DelegateCommand ConfirmCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }
        private Messenger.Messenger messenger;


        public DoctorViewModel()
        {
            Messenger = new Messenger.Messenger();
            EditCommand = new DelegateCommand(ExecuteEdit);
            ConfirmCommand = new DelegateCommand(ExecuteConfirm);
            CancelCommand = new DelegateCommand(ExecuteCancel);
            IsEditable = false;
            Doctor = RegisteredUserController.UserToDoctor(App.User);
            Gender = Doctor.GenderToString();
            Street = Doctor.Address.Street;
            Number = Doctor.Address.Number;
            City = Doctor.Address.City;
            Country = Doctor.Address.Country;
            Phone = Doctor.Phone;
            DateOfBirth = Doctor.DateOfBirth.ToString("dd.MM.yyyy.");
            int lastDigit = Doctor.YearsOfService % 10;
            if(lastDigit <= 1 || lastDigit >= 5)
            {
                ProperText = "godina iskustva";
            } else
            {
                ProperText = "godine iskustva";
            }
            AreButtonsVisible = Visibility.Collapsed;
            
        }

        public void LoadSuccessMessage()
        {
            messenger.ProfileSuccessMessage();
        }

        public Doctor Doctor { get => doctor; set => SetProperty(ref doctor, value); }
        public double DoctorGrade { get => doctorGrade; set => SetProperty(ref doctorGrade, value); }
        public string Gender { get => gender; set => SetProperty(ref gender, value); }
        public string Address { get => address; set => SetProperty(ref address, value); }
        public string Phone { get => phone; set => SetProperty(ref phone, value); }
        public string DateOfBirth { get => dateOfBirth; set => SetProperty(ref dateOfBirth, value); }
        public string ProperText { get => properText; set => SetProperty(ref properText, value); }
        public string Email { get => email; set => SetProperty(ref email, value); }
        public string ValidationError { get => validationError; set => SetProperty(ref validationError, value); }
        public bool IsEditable { get => isEditable; set => SetProperty(ref isEditable, value); }
        public string Street { get => street; set => SetProperty(ref street, value); }
        public string Number { get => number; set => SetProperty(ref number, value); }
        public string City { get => city; set => SetProperty(ref city, value); }
        public string Country { get => country; set => SetProperty(ref country, value); }
        public Visibility IsEditVisible { get => isEditVisible; set => SetProperty(ref isEditVisible, value); }
        public Visibility AreButtonsVisible { get => areButtonsVisible; set => SetProperty(ref areButtonsVisible, value); }
        public Messenger.Messenger Messenger { get => messenger; set => messenger = value; }

        public void ExecuteEdit()
        {
            IsEditable = true;
            IsEditVisible = Visibility.Collapsed;
            AreButtonsVisible = Visibility.Visible;
        }

        public void ExecuteCancel()
        {
            Address = Doctor.Address.ToString();
            Phone = Doctor.Phone;
            AreButtonsVisible = Visibility.Collapsed;
            IsEditVisible = Visibility.Visible;
            IsEditable = false;
        }

        public void ExecuteConfirm()
        {
            DoctorController doctorController = new DoctorController();
            doctorController.UpdateDoctor(Doctor, Phone, Street, Number, City, Country);
            AreButtonsVisible = Visibility.Collapsed;
            IsEditVisible= Visibility.Visible;
            IsEditable = false;
            LoadSuccessMessage();
        }
    }
}
