using UnityEngine;

public class Weapon : Item
{
    public GameObject Prefab { get; private set; }
    public int Damage { get; private set; }

    public Weapon(string name, GameObject prefab, int damage) : base(name)
    {
        Prefab = prefab;
        Damage = damage;
    }
}