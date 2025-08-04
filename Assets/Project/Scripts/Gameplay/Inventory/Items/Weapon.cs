using UnityEngine;

public class Weapon : Item
{
    public WeaponView Prefab { get; private set; }
    public int Damage { get; private set; }

    public Weapon(string name, WeaponView prefab, int damage) : base(name)
    {
        Prefab = prefab;
        Damage = damage;
    }
}