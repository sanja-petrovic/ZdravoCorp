
using System;

public class Equipment
{
    private String id;
    private String name;
    private int amount;
    private bool expendable;

    public string Id { get => id; set => id = value; }
    public string Name { get => name; set => name = value; }
    public int Amount { get => amount; set => amount = value; }
    public bool Expendable { get => expendable; set => expendable = value; }
}
