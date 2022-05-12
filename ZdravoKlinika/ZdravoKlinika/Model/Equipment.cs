
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

    public Equipment(string id, string name, int amount, bool expendable)
    {
        this.id = id;
        this.name = name;
        this.amount = amount;
        this.expendable = expendable;
    }

    public Equipment()
    {
    }

    public static Equipment Parse(String data)
    {
        String[] splitData = data.Split(",");
        Equipment eq = new Equipment();
        eq.Id = splitData[0];
        eq.amount = int.Parse(splitData[1]);
        return eq;
    }
}
