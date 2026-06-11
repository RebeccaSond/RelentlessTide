// This is the enemy class: how the enemy attacks, takes damage, and their stats
public class Enemy
{
    public string Name { get; set; }

    public CharacterStats Stats;

    public Enemy(string name)
    {
        Name = name;

        switch (name.ToLower())
        {
            case "rat":
                Stats = new CharacterStats
                {
                    Health = 15,
                    Attack = 3,
                    Defense = 1
                };
                break;

            case "seagent":
                Stats = new CharacterStats
                {
                    Health = 50,
                    Attack = 8,
                    Defense = 5
                };
                break;

            case "mutated creature":
                Stats = new CharacterStats
                {
                    Health = 80,
                    Attack = 50,
                    Defense = 8
                };
                break;

            default:
                Stats = new CharacterStats
                {
                    Health = 50,
                    Attack = 8,
                    Defense = 5
                };
                break;
        }
    }

    // This is the function connected to the enemy that helps take damage
    public void TakeDamage(int damage)
    {
        Stats.Health -= damage;

        if (Stats.Health < 0)
            Stats.Health = 0;

        Console.WriteLine($"{Name} takes {damage} damage!");
        Console.WriteLine($"{Name}'s Health: {Stats.Health}");
    }

    // This is the function connected to the enemy that helps attack the player
    public void Attack(Player player)
    {

        int baseDamage = Stats.Attack;
        int damage = Random.Shared.Next(baseDamage - 3, baseDamage + 4);

        // Critical hit chance
        bool isCritical = Random.Shared.Next(1, 101) <= 10;
        // Missed hit chance
        bool missedAttack = Random.Shared.Next(1, 101) <= 10;

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