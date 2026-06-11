// This is the class for the building: Allegany State Park
public class AlleganyStatePark : Building
{
    private static bool parkVisited = false;
    public AlleganyStatePark() : base("Allegany State Park") { }

    public override void Enter(Player player)
    {

        if (parkVisited)
        {
            Console.WriteLine("\nYou would be a fool going back there again... Not without something strong to defend yourself with...");
            return;
        }

        parkVisited = true;
        Console.WriteLine("\nYou decided to go to the Allegany State Park. It is a very dangerous idea since the place is swarmed by Seagents, but you had a gut feeling you would find something good there.");
        Console.WriteLine("\nPress ENTER to continue...");
        Console.ReadLine();
        Console.WriteLine("You enter the park... something is wrong.");
        Console.WriteLine("\nPress ENTER to continue...");
        Console.ReadLine();
        Console.WriteLine("Immediately upon entering the park, you are jumped by an extremely powerful mutated Seagent. The Seagent deals a lot of damage, but you flee before he can do anymore.\n");
        player.Stats.Health = 10;

        Console.WriteLine("You survived the park... barely.");
        Thread.Sleep(500);
        return;
    }
}