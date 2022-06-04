using System;
using System.Text;

public class RegisteredUser
{
    private UserType userType;
    private String personalId;
    private String name;
    private String lastname;
    private DateTime dateOfBirth;
    private Gender gender;
    private String phone;
    private String email;
    private String password;
    private String profilePicture;
    private DateTime lastLogin;
    private Address address;

    public UserType UserType { get => userType; set => userType = value; }
    public String UserTypeString
    {
        get 
        {
            String value = "";
            if (userType == UserType.Doctor)
                value = "Doktor";
            else if (userType == UserType.Manager)
                value = "Menadzer";
            else if (userType == UserType.Secretary)
                value = "Sekretar";
            else if (userType == UserType.Patient)
                value = "Pacijent";

            return value;
        }
            }
    public string PersonalId { get => personalId; set => personalId = value; }
    public string Name { get => name; set => name = value; }
    public string Lastname { get => lastname; set => lastname = value; }
    public DateTime DateOfBirth { get => dateOfBirth; set => dateOfBirth = value; }
    public Gender Gender { get => gender; set => gender = value; }
    public string Phone { get => phone; set => phone = value; }
    public string Email { get => email; set => email = value; }
    public string Password { get => password; set => password = value; }
    public string ProfilePicture { get => profilePicture; set => profilePicture = value; }
    public DateTime LastLogin { get => lastLogin; set => lastLogin = value; }
    public Address Address { get => address; set => address = value; }

    public string GetFullName()
    {
        return this.name + " " + this.lastname;
    }

    public string GenderToString()
    {
        if (this.gender == Gender.Female)
        {
            return "Ženski";
        }
        else
        {
            return "Muški";
        }
    }

    public static RegisteredUser ParseId(String id)
    {
        RegisteredUser user = new RegisteredUser();
        user.PersonalId = id;
        return user;
    }

    public static RegisteredUser ParseEmail(String email)
    {
        RegisteredUser user = new RegisteredUser();
        user.Email = email;
        return user;
    }

}