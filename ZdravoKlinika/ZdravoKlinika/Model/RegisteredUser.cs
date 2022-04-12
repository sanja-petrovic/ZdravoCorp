using System;

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
}