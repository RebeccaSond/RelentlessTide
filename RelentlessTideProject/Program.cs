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

        Console.WriteLine($"One day, you realize you are low on stocked supplies...");

        while (!player.IsDead)
        {
            Building building = ChooseBuilding(player);

            building.Enter(player);

            if (player.IsDead)
            {
                Console.WriteLine("GAME OVER\n");
                return;
            }
        }
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
                    Console.WriteLine("\nInvalid choice.");
                    break;
            }

        } while (selectedWeapon == null);

        return selectedWeapon;
    }

    // This is the function for the player's choosen building
    static Building ChooseBuilding(Player player)
    {
        Building selectedBuilding = null;

        do
        {
            Console.WriteLine("\nChoose the first building you wish to visit:");
            Console.WriteLine("1 - Hospital");
            Console.WriteLine("2 - Local Grocery Store");
            Console.WriteLine("3 - Allegany State Park");
            Console.WriteLine("4 - Check player stats");
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

                case "4":
                case "checkstats":
                case "check stats":
                case "checkplayerstats":
                case "check player stats":
                    player.ShowStats();
                    Console.WriteLine("\nPress ENTER to continue...");
                    Console.ReadLine();
                    break;

                default:
                    Console.WriteLine("\nInvalid choice.");
                    break;
            }

        } while (selectedBuilding == null);

        return selectedBuilding;
    }
}

// Structure requirment (still trying to figure out its purpose)
public struct CharacterStats
{
    public int Health;
    public int Attack;
    public int Defense;
}

// Union requirment (still trying to figure out its purpose) Only ONE value is meant to exist at a time
[StructLayout(LayoutKind.Explicit)]
public struct DamageType
{
    [FieldOffset(0)]
    public int PhysicalDamage;

    [FieldOffset(0)]
    public int SpecialDamage;
}