public static class EncounterSystem
{
    public static void TriggerRandomEvent(Player player)
    {
        int roll = Random.Shared.Next(130);

        if (roll < 40)
        {
            Console.WriteLine("Nothing happens...");
        }
        else if (roll < 70)
        {
            Console.WriteLine("You found supplies!");
            player.Stats.Attack = Math.Min(100, player.Stats.Attack + 5);
            player.Stats.Defense = Math.Min(100, player.Stats.Defense + 5);
            Console.WriteLine("Your attack and defense increased by 5!");
        }
        else if (roll < 90)
        {
            Console.WriteLine("You found supplies!");
            player.Stats.Attack = Math.Min(100, player.Stats.Attack + 5);
            Console.WriteLine("Your attack increased by 5!");
        }
        else if (roll < 110)
        {
            Console.WriteLine("You found supplies!");
            player.Stats.Defense = Math.Min(100, player.Stats.Defense + 5);
            Console.WriteLine("Your attack increased by 5!");
        }
        else
        {
            Console.WriteLine("\nEnemy encounter!");

            string[] enemyTypes =
            {
                "Rat",
                "Seagent",
                "Mutated Creature"
            };

            string randomEnemy = enemyTypes[Random.Shared.Next(enemyTypes.Length)];

            Enemy enemy = new Enemy(randomEnemy);
            CombatSystem.Fight(player, enemy);
        }
    }
}