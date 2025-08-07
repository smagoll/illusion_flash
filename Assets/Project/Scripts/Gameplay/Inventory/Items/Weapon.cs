using Unity.VisualScripting;
using UnityEngine;

public class Weapon : Item
{
    public WeaponView Prefab { get; private set; }
    public int Damage { get; private set; }
    public float Range { get; private set; }

    public Weapon(string name, WeaponView prefab, int damage, float range) : base(name)
    {
        Prefab = prefab;
        Damage = damage;
        Range = range;
    }
}