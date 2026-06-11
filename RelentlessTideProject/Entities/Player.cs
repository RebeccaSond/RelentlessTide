// This is the player class: how the player attacks, takes damage, and their stats
public class Player
{
    public string Name { get; set; }

    public CharacterStats Stats;

    public Weapon EquippedWeapon { get; set; }

    public bool IsDead => Stats.Health <= 0;

    public Player(string name)
    {
        Name = name;

        Stats = new CharacterStats
        {
            Health = 100,
            Attack = 15,
            Defense = 10
        };
    }

    // This is the function connected to the player that helps attack enemies
    public void Attack(Enemy enemy)
    {
        int damage = EquippedWeapon.DamageAmount;

        // Critical hit chance
        bool isCritical = Random.Shared.Next(1, 101) <= 10;
        // Missed hit chance
        bool missedAttack = Random.Shared.Next(1, 101) <= 10;

        if (isCritical)
        {
            damage *= 2;
        }

        if (missedAttack)
        {
            damage = 0;
        }

        if (missedAttack)
        {
            Console.WriteLine("Player attack missed!");
        }

        Thread.Sleep(500);
        EquippedWeapon.Use();
        enemy.TakeDamage(damage);
        Thread.Sleep(500);

        if (isCritical)
        {
            Console.WriteLine("PLAYER MADE A CRITICAL HIT!");
        }
    }

    // This is the function connected to the player that helps take damage
    public void TakeDamage(int damage)
    {
        int reducedDamage = (int)(damage * (100 - Stats.Defense) / 100.0);

        if (reducedDamage < 1)
            reducedDamage = 1;

        Stats.Health -= reducedDamage;

        if (Stats.Health < 0)
            Stats.Health = 0;

        Console.WriteLine($"{Name} takes {reducedDamage} damage!");
        Console.WriteLine($"{Name} HP: {Stats.Health}");
    }

    public void ShowStats()
    {
        Console.WriteLine("\n----- PLAYER STATS -----");
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Health: {Stats.Health}");
        Console.WriteLine($"Attack: {Stats.Attack}");
        Console.WriteLine($"Defense: {Stats.Defense}");
        Console.WriteLine($"Weapon: {EquippedWeapon.Name}");
        Console.WriteLine("--------------------------");
    }
}