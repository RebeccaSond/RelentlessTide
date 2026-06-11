// This is the enemy class: how the enemy attacks, takes damage, and their stats
public class Enemy
{
    public string Name { get; set; }

    public CharacterStats Stats;

    public Enemy(string name)
    {
        Name = name;

        // This is all the enemies the player can get attacked by
        switch (name.ToLower())
        {
            case "rat":
                Stats = new CharacterStats
                {
                    Health = 15,
                    Attack = 3,
                    Defense = 2
                };
                break;

            case "seagent":
                Stats = new CharacterStats
                {
                    Health = 80,
                    Attack = 8,
                    Defense = 8
                };
                break;

            case "mutated creature":
                Stats = new CharacterStats
                {
                    Health = 100,
                    Attack = 4,
                    Defense = 1
                };
                break;

            default:
                Stats = new CharacterStats
                {
                    Health = 50,
                    Attack = 45,
                    Defense = 20
                };
                break;
        }
    }

    // This is the function connected to the enemy that helps take damage
    public void TakeDamage(DamageValue damage)
    {
        int finalDamage;

        if (damage.Kind == DamageKind.Physical)
        {
            finalDamage = damage.Physical - Stats.Defense;
            Console.WriteLine($"{Name} takes physical damage!");
        }
        else
        {
            finalDamage = damage.Special - Stats.Defense;
            Console.WriteLine($"{Name} takes special damage!");
        }

        if (finalDamage < 1)
            finalDamage = 1;

        Stats.Health -= finalDamage;

        if (Stats.Health < 0)
            Stats.Health = 0;

        Console.WriteLine($"{Name} takes {finalDamage} damage!");
        Console.WriteLine($"{Name}'s Health: {Stats.Health}");
    }

    // This is the function connected to the enemy that helps attack the player
    public void Attack(Player player)
    {

        int baseDamage = Stats.Attack;
        int damage = Random.Shared.Next(baseDamage - 3, baseDamage + 4);

        int roll = Random.Shared.Next(1, 101);

        // Missed hit chance
        bool missedAttack = roll <= 10;
        // Critical hit chance
        bool isCritical = roll >= 91;

        Console.WriteLine($"\n{Name} attacks!");
        Thread.Sleep(500);

        if (missedAttack)
        {
            Console.WriteLine("Enemy attack missed!");
        }

        if (isCritical)
        {
            damage *= 2;
        }

        if (missedAttack)
        {
            damage = 0;
        }

        player.TakeDamage(damage);

        if (isCritical)
        {
            Console.WriteLine("ENEMY MADE A CRITICAL HIT!");
        }
    }

    // This is a bool function that checks if the enemy has been defeated by the player
    public bool IsDefeated()
    {
        return Stats.Health <= 0;
    }
}