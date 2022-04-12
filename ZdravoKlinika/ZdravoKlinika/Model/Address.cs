using System;

public class Address
{
    private String street;
    private String number;
    private String city;
    private String country;

    public Address(string street, string number, string city, string country)
    {
        this.street = street;
        this.number = number;
        this.city = city;
        this.country = country;
    }

    public Address(String data) 
    {
        String[] dataSplit = data.Split(" ");
        street = dataSplit[0];
        number = dataSplit[1];
        city = dataSplit[2];
        country = dataSplit[3];
    }

    public Address() { }

    public string Street { get => street; set => street = value; }
    public string Number { get => number; set => number = value; }
    public string City { get => city; set => city = value; }
    public string Country { get => country; set => country = value; }
}