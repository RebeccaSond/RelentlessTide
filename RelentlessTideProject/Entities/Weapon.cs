// Abstract requirment (still trying to figure out its purpose)
public abstract class Weapon
{
    public string Name { get; }

    public int DamageAmount { get; protected set; }

    protected Weapon(string name)
    {
        Name = name;
    }

    public abstract void Use();
}

// These are the classes for the weapons. First is hatchet
public class Hatchet : Weapon
{
    public Hatchet() : base("Hatchet")
    {
        DamageAmount = 30;
    }

    public override void Use()
    {
        Console.WriteLine("You slash with the hatchet!");
    }
}

// Second is spiked baseball bat
public class SpikedBaseballBat : Weapon
{
    public SpikedBaseballBat() : base("Spiked Baseball Bat")
    {
        DamageAmount = 23;
    }

    public override void Use()
    {
        Console.WriteLine("You swing with the spiked baseball bat");
    }
}

// And last is shotgun
public class Shotgun : Weapon
{
    public Shotgun() : base("Shotgun")
    {
        DamageAmount = 15;
    }

    public override void Use()
    {
        Console.WriteLine("You fire the shotgun");
    }
}