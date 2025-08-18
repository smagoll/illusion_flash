public class BlockSystem
{
    public bool IsBlocked { get; private set; }
    
    public void Block(bool isActive)
    {
        IsBlocked = isActive;
    }

    public int ReduceDamage(int damage)
    {
        return damage;
    }
}