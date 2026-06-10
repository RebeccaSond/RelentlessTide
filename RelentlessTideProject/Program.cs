using System;
using System.Runtime.InteropServices;

class Program
{
    static void Main()
    {
        // Start of the game: Asking players name, weapon choice, etc.
        Console.Clear();
        string name = GetPlayerName();

        if (!IsReadyToPlay(name))
        {
            Console.WriteLine("See you soon!");
            return;
        }

        Player player = new Player(name);

        player.EquippedWeapon = ChooseWeapon();

        Console.WriteLine("\n----------------------------------------\n");
        Console.WriteLine($"{player.Name} chose the {player.EquippedWeapon.Name}!");

        Console.WriteLine($"\nPlayer Stats:");
        Console.WriteLine($"Health: {player.Stats.Health}");
        Console.WriteLine($"Attack: {player.Stats.Attack}");
        Console.WriteLine($"Defense: {player.Stats.Defense}");

        //Console.WriteLine($"\nIt's been 5 years since the incident at the nearby facility in your town. That's where it all started. Your name is {name}, and you have survived in this now desolate town. However, you are not alone, for your friends and family, now all lifeless corpses dancing the streets, seem to haunt you. The explosion was the cause of a zombie apolocolpyse, inflicing the town in a terrible disease. Your town was locked down, and anybody within is unable to leave, hence why you are still there yourself. It didn't matter to the outsiders that people like you were still in the town- All that mattered is that the sickness stayed in one place and it was going to stay in that one place.");
        Console.WriteLine($"\nIt's been 5 years since the nuclear war that took place in the pacific ocean. The fight for trade lines were a disaster as many countries fought for the same trade lines. An unexplainable force has come its way onto land, striking people down left and right. These creatures are what we human call Seagents. Seagents emerged due to the nuclear wars infecting aquatic features, giving them the ability to tread among land. However, these creatures were strangely given aggresive properties, and are extremely dangerous to interact with.");
        Console.WriteLine("\nPress ENTER to continue...");
        Console.ReadLine();
        //Console.WriteLine($"\nYou live in your vehicile, parked inbetween two buildings and covered by drapes. You never dare to start it up.. It would only attarct unwanted visitors if you did. You worked away on a word search puzzle you found in a nearby shop, but you were stopped by a thump heard nearby. You gently take your {player.EquippedWeapon.Name} from the back seat.. You carefully exit your vehicle from the passenger side, only to immedietly be confronted by danger.");
        Console.WriteLine($"Your name is {name}, and you live in the city Hapbell, NY. You've lived on your own ever since your father sacraficed himself protecting you from an unexpected Seagent attack. You try your best everyday to tend to your needs without getting into fights, but you always carry your {player.EquippedWeapon.Name}, just in case.");
        Console.WriteLine("\nPress ENTER to continue...");
        Console.ReadLine();

        // Player chooses building
        Console.WriteLine($"One day, you realize you are low on stocked supplies. You decide to visit three specific places to gather more material.");
        Building building = ChooseBuilding();
        building.Enter(player);

        // Seagent attacks player
        Enemy seagent = new Enemy("Seagent");
        Console.WriteLine("\nA Zombie appears!");

        while (!seagent.IsDefeated() && player.Stats.Health > 0)
        {
            Console.WriteLine("\nPress ENTER to attack...");
            Console.ReadLine();
            Thread.Sleep(100);

            Console.WriteLine("\n----------------------------------------\n");
            player.Attack(seagent);

            if (!seagent.IsDefeated())
            {
                seagent.Attack(player);
            }
        }
        Console.WriteLine($"\n{player.Name} defeated the Zombie!");
    }

    // This is the function that asks the player for a name
    static string GetPlayerName()
    {
        string name;

        do
        {
            Console.WriteLine("\n----------------------------------------\n");
            Console.Write("\nName your character: ");
            name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Please enter a valid name.");
            }

        } while (string.IsNullOrWhiteSpace(name));

        return name;
    }

    // This is the function that asks the player if they are ready to play
    static bool IsReadyToPlay(string name)
    {
        Console.Write($"Are you ready to play, {name}? (Yes/No): ");

        string answer = Console.ReadLine();

        return answer.Trim().Equals("Yes",
            StringComparison.OrdinalIgnoreCase);
    }

    // This is the function for everything related to the player's choosen weapon
    static Weapon ChooseWeapon()
    {
        Weapon selectedWeapon = null;

        do
        {
            Console.WriteLine("\n----------------------------------------\n");
            Console.WriteLine("\nChoose a weapon:");
            Console.WriteLine("\n⚠️ WARNING: You cannot change your weapon later. You will use this weapon for the remainder of the game! ⚠️\n");
            Console.WriteLine("1 - Hatchet");
            Console.WriteLine("2 - Spiked Baseball Bat");
            Console.WriteLine("3 - Shotgun");
            Console.Write("Choice: ");

            string choice = Console.ReadLine();

            switch (choice?.Trim().ToLower())
            {
                case "1":
                case "hatchet":
                    selectedWeapon = new Hatchet();
                    break;

                case "2":
                case "spiked baseball bat":
                case "spikedbaseballbat":
                    selectedWeapon = new SpikedBaseballBat();
                    break;

                case "3":
                case "shotgun":
                case "shot gun":
                    selectedWeapon = new Shotgun();
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }

        } while (selectedWeapon == null);

        return selectedWeapon;
    }

    // This is the function for the player's choosen building
    static Building ChooseBuilding()
    {
        Building selectedBuilding = null;

        do
        {
            Console.WriteLine("\nChoose the first building you wish to visit:");
            Console.WriteLine("1 - Hospital");
            Console.WriteLine("2 - Local Grocery Store");
            Console.WriteLine("3 - Allegany State Park");
            Console.Write("Choice: ");

            string choice = Console.ReadLine();

            switch (choice?.Trim().ToLower())
            {
                case "1":
                case "hospital":
                    selectedBuilding = new Hospital();
                    break;

                case "2":
                case "local grocery store":
                    selectedBuilding = new LocalGroceryStore();
                    break;

                case "3":
                case "allegany state park":
                    selectedBuilding = new AlleganyStatePark();
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }

        } while (selectedBuilding == null);

        return selectedBuilding;
    }
}

// Structure requirment (still trying to figure out its purpose)
struct CharacterStats
{
    public int Health;
    public int Attack;
    public int Defense;
}

// Union requirment (still trying to figure out its purpose) Only ONE value is meant to exist at a time
[StructLayout(LayoutKind.Explicit)]
struct DamageType
{
    [FieldOffset(0)]
    public int PhysicalDamage;

    [FieldOffset(0)]
    public int SpecialDamage;
}

// This is the player class: how the player attacks, takes damage, and their stats
class Player
{
    public string Name { get; set; }

    public CharacterStats Stats;

    public Weapon EquippedWeapon { get; set; }

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
        Stats.Health -= damage;

        if (Stats.Health < 0)
            Stats.Health = 0;

        Console.WriteLine($"{Name} takes {damage} damage!");
        Console.WriteLine($"{Name} HP: {Stats.Health}");
    }
}


// Abstract requirment (still trying to figure out its purpose)
abstract class Weapon
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
class Hatchet : Weapon
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
class SpikedBaseballBat : Weapon
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
class Shotgun : Weapon
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

abstract class Building
{
    public string Name { get; }

    protected Building(string name)
    {
        Name = name;
    }

    public abstract void Enter(Player player);
}

// These are the classes for the buildings. First is hospital
class Hospital : Building
{
    public Hospital() : base("Hospital") { }

    public override void Enter(Player player)
    {
        Console.WriteLine("You decided to go to the hospital. A hospital is bound to have medical supplies that could aid you if you ever got too wounded.");

        player.Stats.Health += 30;
        if (player.Stats.Health > 100)
            player.Stats.Health = 100;

        Console.WriteLine("You restored 30 HP!");
    }
}

// Second is Local Grocery Store
class LocalGroceryStore : Building
{
    public LocalGroceryStore() : base("Local Grocery Store") { }

    public override void Enter(Player player)
    {
        Console.WriteLine("You decided to go to the local grocery store. There was plenty of food and accesories there for you to find.");
        Console.WriteLine("Luckily, you found yourself some energy drinks, even your favorite flavored ones.");

        player.Stats.Attack += 5;
        Console.WriteLine("Your attack increased!");
    }
}

// And last is Allegany State Park
class AlleganyStatePark : Building
{
    public AlleganyStatePark() : base("Allegany State Park") { }

    public override void Enter(Player player)
    {
        Console.WriteLine("You decided to go to the Allegany State Park. It is a very dangerous idea since the place is swarmed by Seagents, but you had a gut feeling you would find something good there.");
        Console.WriteLine("You enter the park... something is wrong.");

        Enemy seagent = new Enemy("Mutated Creature");

        while (!seagent.IsDefeated() && player.Stats.Health > 0)
        {
            player.Attack(seagent);

            if (!seagent.IsDefeated())
                seagent.Attack(player);
        }

        Console.WriteLine("You survived the park... barely.");
    }
}

// This is the enemy class: how the enemy attacks, takes damage, and their stats
class Enemy
{
    public string Name { get; set; }

    public CharacterStats Stats;

    public Enemy(string name)
    {
        Name = name;

        Stats = new CharacterStats
        {
            Health = 50,
            Attack = 8,
            Defense = 5
        };
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