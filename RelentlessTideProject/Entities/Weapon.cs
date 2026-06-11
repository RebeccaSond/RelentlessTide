// Abstract requirment: a grouped structure for all weapons in game
public abstract class Weapon
{
    public string Name { get; }

    public DamageValue Damage { get; protected set; }

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
        Damage = new DamageValue
        {
            Physical = 30,
            Kind = DamageKind.Physical
        };
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
        Damage = new DamageValue
        {
            Physical = 23,
            Kind = DamageKind.Physical
        };
    }

    public override void Use()
    {
        Console.WriteLine("You swing with the spiked baseball bat!");
    }
}

// And last is shotgun
public class Shotgun : Weapon
{
    public Shotgun() : base("Shotgun")
    {
        Damage = new DamageValue
        {
            Special = 20,
            Kind = DamageKind.Special
        };
    }

    public override void Use()
    {
        Console.WriteLine("You fire the shotgun!");
    }
}