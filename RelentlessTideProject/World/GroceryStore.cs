// This is the class for the building: Local Grocery Store
public class LocalGroceryStore : Building
{
    private bool backRoomSearched = false;
    public LocalGroceryStore() : base("Local Grocery Store") { }
    public override void Enter(Player player)
    {
        Console.WriteLine("\nYou decided to go to the local grocery store. There was plenty of food and accesories there for you to find.");

        bool leave = false;

        while (!leave && player.Stats.Health > 0)
        {
            Console.WriteLine("\nWhat do you do?");
            Console.WriteLine("1 - Search the shelves");
            Console.WriteLine("2 - Check the back room");
            Console.WriteLine("3 - Check the register");
            Console.WriteLine("4 - Check player stats");
            Console.WriteLine("5 - Leave grocery store");
            Console.Write("Choice: ");

            string choice = Console.ReadLine()?.Trim();

            switch (choice)
            {
                case "1":
                    Shelves(player);
                    break;

                case "2":
                    BackRoom(player);
                    backRoomSearched = true;
                    break;

                case "3":
                    Register(player);
                    break;

                case "4":
                    player.ShowStats();
                    Console.WriteLine("\nPress ENTER to continue...");
                    Console.ReadLine();
                    break;

                case "5":
                    leave = true;
                    break;

                default:
                    Console.WriteLine("Invalid choice.\n");
                    break;
            }
        }

        if (player.Stats.Health > 0)
        {
            Console.WriteLine("\nYou leave the grocery store...");
        }
    }

    private void Shelves(Player player)
    {
        Console.WriteLine("\nYou check the shelves...");

        EncounterSystem.TriggerRandomEvent(player);
    }

    private void BackRoom(Player player)
    {

        if (backRoomSearched)
        {
            Console.WriteLine("\nNot going in there again...");
            Thread.Sleep(500);
            return;
        }

        Console.WriteLine("\nYou search the back room...");
        Console.WriteLine($"As you search the dark room, you notice a rustle of movement from your side. You get your {player.EquippedWeapon.Name} ready as something suddenly launches at you!");
        Console.WriteLine("\nPress ENTER to continue...");
        Console.ReadLine();

        Enemy enemy = new Enemy("Rat");
        CombatSystem.Fight(player, enemy);
    }

    private void Register(Player player)
    {
        Console.WriteLine("\nYou check the register...");
        Console.WriteLine("\nPress ENTER to continue...");
        Console.ReadLine();

        EncounterSystem.TriggerRandomEvent(player);
    }
}