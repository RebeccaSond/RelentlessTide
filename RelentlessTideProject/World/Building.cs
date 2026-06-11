// Abstract requirment: a grouped structure for all building options
public abstract class Building
{
    public string Name { get; }

    protected Building(string name)
    {
        Name = name;
    }

    public abstract BuildingResult Enter(Player player);
}

public enum BuildingResult
{
    Leave,
    FoundClue,
    PlayerDied
}