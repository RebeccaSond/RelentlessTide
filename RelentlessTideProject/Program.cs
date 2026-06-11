using System;
using System.Runtime.InteropServices;

class Program
{
    static void Main()
    {
        // Start of the game: Asking players name, weapon choice, etc.
        Console.Clear();
        StoryState state = StoryState.Intro;
        string name = GetPlayerName();

        if (!IsReadyToPlay(name))
        {
            Console.WriteLine("\nSee you soon!\n");
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

        Console.WriteLine($"\nIt's been 5 years since the nuclear war that took place in the Pacific Ocean. The fight for trade lines was a disaster as many countries fought for trade line control. An unexplainable force has come its way onto land, striking people down left and right. These creatures are what we human call Seagents. Seagents emerged due to the nuclear wars infecting aquatic creatures, giving them the ability to tread among land. However, these creatures were strangely given aggressive properties, and are extremely dangerous to interact with.");
        Console.WriteLine("\nPress ENTER to continue...");
        Console.ReadLine();
        Console.WriteLine($"Your name is {name}, and you live in the city Hapbell, NY. You've lived on your own ever since your father sacrificed himself protecting you from an unexpected Seagent attack.. But you don't believe he's truly gone, do you? You try your best every day to tend to your needs without getting into fights, but you always carry your {player.EquippedWeapon.Name}, just in case.");
        Console.WriteLine("\nPress ENTER to continue...");
        Console.ReadLine();

        Console.WriteLine($"One day, you realize you are low on stocked supplies...");

        // Start of combat gameplay: The player can now be attacked by enemies and enter buildings. They can also proceed to end of game
        while (!player.IsDead)
        {
            switch (state)
            {
                // Lets player explore buildings
                case StoryState.Intro:
                    Console.WriteLine("\nYou step out into the ruined city...");
                    Thread.Sleep(500);
                    state = StoryState.FirstExploration;
                    break;

                case StoryState.FirstExploration:
                    Building building = ChooseBuilding(player);
                    building.Enter(player);

                    state = StoryState.FoundClue;
                    break;

                // Player meets stranger
                case StoryState.FoundClue:
                    Console.WriteLine("\nAs you gather your materials, you continue on your way through the city. It's quieter than usual.. However, you notice movement ahead of you.");
                    Console.WriteLine("\nPress ENTER to continue...");
                    Console.ReadLine();
                    Console.WriteLine("Upon closer inspection, you discover that the movement was caused by a human. Uninterested, you turn away. Again, you wish to avoid fights... However, you notice the stranger has something in their hand. A simple pocket watch... Incased in golden vines wrapped around a somewhat dented silver lining... The exact one that your father owned.");
                    Console.WriteLine("\nPress ENTER to continue...");
                    Console.ReadLine();
                    state = StoryState.FinalStage;
                    break;

                // Player confronts stranger and decideds fate
                case StoryState.FinalStage:
                    Console.WriteLine("You quickly approach the stranger, startling them as you do.");

                    DialogueChoice choice = ChooseDialogueOption();

                    static DialogueChoice ChooseDialogueOption()
                    {
                        while (true)
                        {
                            Console.WriteLine("\nWhat do you say?");
                            Console.WriteLine("1 - Ask politely");
                            Console.WriteLine("2 - Demand answers");
                            Console.Write("Choice: ");

                            switch (Console.ReadLine())
                            {
                                case "1":
                                case "askpolitely":
                                case "ask politely":
                                    return DialogueChoice.Polite;

                                case "2":
                                case "demandanswers":
                                case "demand answers":
                                    return DialogueChoice.Rude;

                                default:
                                    Console.WriteLine("\nInvalid choice.");
                                    break;
                            }
                        }
                    }

                    // Player chooses polite or rude fate
                    switch (choice)
                    {
                        case DialogueChoice.Polite:
                            Console.WriteLine("\n\"Excuse me... Do you know where you got that pocket watch?\"");
                            Console.WriteLine("\"Why does it matter to you?\" the stranger replied harshly. They played with the watch's chain as if mocking you.");
                            Console.WriteLine("\nPress ENTER to continue...");
                            Console.ReadLine();
                            Console.WriteLine("You nod slightly \"I'm truly sorry.. But somebody really close to me owned that watch, and I wish to find him.\"");
                            Console.WriteLine("Frozen, the stranger stared at you as if they didn't know what to say. Finally, they spoke, \"I found it near the old fishing bay.. Near Kamari Street.\"");
                            Console.WriteLine("\nPress ENTER to continue...");
                            Console.ReadLine();
                            Console.WriteLine("You smile at the stranger before you turn away, shouting: \"Thank you!\"");
                            Console.WriteLine("\nPress ENTER to continue...");
                            Console.ReadLine();
                            state = StoryState.FinalStageComplete;
                            break;

                        case DialogueChoice.Rude:
                            Console.WriteLine("\n\"Hey! Tell me where you got that watch!\"");
                            Console.WriteLine("\"Who are you?? Back off! This is my watch!\" the stranger snaps.");
                            Console.WriteLine("\nPress ENTER to continue...");
                            Console.ReadLine();
                            Console.WriteLine($"\"I need you to tell me where you found that watch at once, or else.\" You raise your {player.EquippedWeapon.Name}");
                            Console.WriteLine($"The stranger raised their weapon in response. \"Go away!!\" They exclaimed, lunging forward.");

                            Enemy enemy = new Enemy("Stranger");

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
                                Console.WriteLine($"\n{player.Name} has been defeated!");
                                Console.WriteLine("\nPress ENTER to continue...");
                                Console.ReadLine();
                                Console.WriteLine("\nGAME OVER\n");
                                Environment.Exit(0);
                                return;
                            }

                            if (enemy.IsDefeated())
                            {
                                Console.WriteLine($"\n{enemy.Name} was defeated!");
                                Console.WriteLine("\nPress ENTER to continue...");
                                Console.ReadLine();
                                Console.WriteLine("The stranger lay before you, lifeless. You take their watch, and examine it closely. With your act of violence, you had no chance in figuring out the true location of this watch. Your motivations were too relentless for your own good..");
                                Console.WriteLine("\nPress ENTER to continue...");
                                Console.ReadLine();
                                Console.Clear();
                                Console.WriteLine("\nTHE END: Aggresive Ending (1/4)\n");
                                Environment.Exit(0);
                            }

                            Console.ReadLine();
                            break;
                    }
                    break;

                // Player meets father
                case StoryState.FinalStageComplete:
                    Console.WriteLine("You run down the streets in a hurry, your mind racing at the thought of reuniting with your father. As you approach the fishing bay, you slow down, trying to remain calm to not attract the attention of unwanted visitors.");
                    Console.WriteLine("\nPress ENTER to continue...");
                    Console.ReadLine();
                    Console.WriteLine("You enter the bay, the floorboards creaking with each footstep. The bay was ruined.. Walls torn and fishing boats missing entirely.");
                    Console.WriteLine("You quickly come to a stop as something arose from the water in-between one of the boat slips.");
                    Console.WriteLine("\nIt was your father");
                    Console.WriteLine("\nPress ENTER to continue...");
                    Console.ReadLine();
                    Console.WriteLine("The creature.. Clothes torn, flesh bubbling, your father was a walking monster. The only thing that indicated form of life were his wide eyes as they stared right into your soul. Frozen in fear, you watch as your father crawls out of the water after you.");


                    FatherChoice fatherChoice = ChooseFatherAction();

                    static FatherChoice ChooseFatherAction()
                    {
                        while (true)
                        {
                            Console.WriteLine("\nWhat do you do?");
                            Console.WriteLine("1 - RUN AWAY!!");
                            Console.WriteLine("2 - Fight him");
                            Console.WriteLine("3 - Talk to him");
                            Console.Write("Choice: ");

                            switch (Console.ReadLine())
                            {
                                case "1":
                                case "runaway":
                                case "run away":
                                    return FatherChoice.Run;

                                case "2":
                                case "fighthim":
                                case "fight him":
                                    return FatherChoice.Fight;

                                case "3":
                                case "talktohim":
                                case "talk to him":
                                    return FatherChoice.Talk;

                                default:
                                    Console.WriteLine("Invalid choice.");
                                    break;
                            }
                        }
                    }

                    // Player makes decision to run, fight, or talk when confronted by their father
                    switch (fatherChoice)
                    {
                        case FatherChoice.Run:
                            Console.WriteLine("\nYou turn tail and flee as quickly as you could. Luckily, you could outrun him.");
                            Console.WriteLine("As you stop to catch your breath, the image of your father's grotesque face swarmed your thoughts.. An image that would haunt you forever.");
                            Console.WriteLine("\nPress ENTER to continue...");
                            Console.ReadLine();
                            Console.Clear();
                            Console.WriteLine("\nTHE END: Afraid Ending (2/4)\n");
                            Environment.Exit(0);
                            break;

                        case FatherChoice.Fight:
                            Console.WriteLine($"\nSwallowing your fear, you raise your {player.EquippedWeapon.Name}...");


                            Enemy enemy = new Enemy("Father");

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
                                Console.WriteLine($"\n{player.Name} has been defeated!");
                                Console.WriteLine("\nPress ENTER to continue...");
                                Console.ReadLine();
                                Console.WriteLine("\nGAME OVER\n");
                                Environment.Exit(0);
                                return;
                            }

                            if (enemy.IsDefeated())
                            {
                                Console.WriteLine($"\n{enemy.Name} was defeated!");
                                Console.WriteLine("\nPress ENTER to continue...");
                                Console.ReadLine();
                                Console.WriteLine("It's over. Through your relentless trials.. The hope you had built for years for this day to come only to discover.. He was gone a long time ago.");
                                Console.WriteLine("\nPress ENTER to continue...");
                                Console.ReadLine();
                                Console.Clear();
                                Console.WriteLine("\nTHE END: Relinquish Ending (3/4)\n");
                                Environment.Exit(0);
                                break;
                            }
                            break;

                        case FatherChoice.Talk:
                            Console.WriteLine("\nYou attempt to talk to him, desperately trying to reach him. As your father approached, he showed no sign of stopping.");
                            Console.WriteLine("\nYou back away, continuing to spill your heart out to him, but it was as if you were talking to a wall.");
                            Console.WriteLine("\nPress ENTER to continue...");
                            Console.ReadLine();
                            Console.WriteLine("Suddenly, you found yourself slipping and falling into one of the boat slips behind you. Your father leaps in after you as you are torn to shreds by him and fellow Seagents.");
                            Console.WriteLine("\nPress ENTER to continue...");
                            Console.ReadLine();
                            Console.Clear();
                            Console.WriteLine("\nTHE END: Endeavour Ending (4/4)\n");
                            Environment.Exit(0);
                            break;
                    }
                    break;
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
            Console.WriteLine("\n----------------------------------------");
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

// Structure requirment: groups stat data together for enemies and player for easier access
public struct CharacterStats
{
    public int Health;
    public int Attack;
    public int Defense;
}

// Labels for weapon damage
public enum DamageKind
{
    Physical,
    Special
}

// Union requirement: assigning if weapon is physical or special damage and making sure only one instance exists
[StructLayout(LayoutKind.Explicit)]
public struct DamageValue
{
    [FieldOffset(0)]
    public int Physical;

    [FieldOffset(0)]
    public int Special;

    [FieldOffset(4)]
    public DamageKind Kind;
}

// Labels for story progression
enum StoryState
{
    Intro,
    FirstExploration,
    FoundClue,
    FinalStage,
    FinalStageComplete
}

// Labels for polite or rude choices from player
enum DialogueChoice
{
    Polite,
    Rude
}

// Labels for players choice when confronted by their father
enum FatherChoice
{
    Run,
    Fight,
    Talk
}