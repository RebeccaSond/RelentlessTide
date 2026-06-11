public abstract class Building
{
    public string Name { get; }

    protected Building(string name)
    {
        Name = name;
    }

    public abstract void Enter(Player player);
}