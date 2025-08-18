public class DamageData
{
    private int _damage;

    public int Damage => GetDamage();
    public Character Character { get; private set; }

    public DamageData(int damage, Character character)
    {
        _damage = damage;
        Character = character;
    }

    private int GetDamage()
    {
        return _damage;
    }
}