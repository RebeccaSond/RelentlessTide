// This is the class for the building: Hospital
public class Hospital : Building
{
    private bool pharmacySearched = false;
    public Hospital() : base("Hospital") { }

    public override BuildingResult Enter(Player player)
    {
        Console.WriteLine("\nYou enter the hospital. It’s dark and silent...");

        bool leave = false;

        while (!leave && player.Stats.Health > 0)
        {
            Console.WriteLine("\nWhat do you do?");
            Console.WriteLine("1 - Search patient rooms");
            Console.WriteLine("2 - Check pharmacy");
            Console.WriteLine("3 - Rest in waiting area");
            Console.WriteLine("4 - Check player stats");
            Console.WriteLine("5 - Leave hospital");
            Console.Write("Choice: ");

            string choice = Console.ReadLine()?.Trim();

            switch (choice)
            {
                case "1":
                    SearchRooms(player);
                    break;

                case "2":
                    Pharmacy(player);
                    break;

                case "3":
                    Rest(player);
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
                    Console.WriteLine("\nInvalid choice.");
                    break;
            }
        }

        if (player.Stats.Health > 0)
        {
            Console.WriteLine("\nYou leave the hospital...");
            return BuildingResult.Leave;
        }
        else
        {
            Console.WriteLine("\nGAME OVER\n");
            return BuildingResult.PlayerDied;
        }
    }

    // Function for player searching patient rooms
    private void SearchRooms(Player player)
    {
        Console.WriteLine("\nYou search the patient rooms...");

        EncounterSystem.TriggerRandomEvent(player);
        Console.WriteLine("\nPress ENTER to continue...");
        Console.ReadLine();
    }

    // Function for player searching pharmacy
    private void Pharmacy(Player player)
    {

        if (pharmacySearched)
        {
            Console.WriteLine("\nYou took all that you could in the Pharmacy. There were no more supplies left.");
            Console.WriteLine("\nPress ENTER to continue...");
            Console.ReadLine();
            return;
        }

        pharmacySearched = true;
        Console.WriteLine("\nYou enter the pharmacy...");

        Console.WriteLine("You find painkillers... Your attack increased by 2.");
        player.Stats.Attack += 2;
        Console.WriteLine("\nPress ENTER to continue...");
        Console.ReadLine();
    }

    // Function for player entering waiting area
    private void Rest(Player player)
    {
        Console.WriteLine("\nYou rest quietly... You feel healthier!");
        player.Stats.Health = Math.Min(100, player.Stats.Health + 10);
        Console.WriteLine("\nPress ENTER to continue...");
        Console.ReadLine();
    }
}