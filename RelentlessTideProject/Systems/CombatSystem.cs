public static class CombatSystem
{
    public static void Fight(Player player, Enemy enemy)
    {
        while (!enemy.IsDefeated() && player.Stats.Health > 0)
        {
            Console.WriteLine("\nPress ENTER to attack...");
            Console.ReadLine();

            Console.WriteLine("\n----------------------------------------\n");

            player.Attack(enemy);

            if (!enemy.IsDefeated())
            {
                enemy.Attack(player);
            }
        }

        if (player.Stats.Health <= 0)
        {
            Console.WriteLine($"\n{player.Name} has been defeated!\n");
            return;
        }

        if (enemy.IsDefeated())
        {
            Console.WriteLine($"\n{enemy} was defeated!\n");
        }
    }
}