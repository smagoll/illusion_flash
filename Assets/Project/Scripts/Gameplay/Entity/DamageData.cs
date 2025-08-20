using UnityEngine;

public class DamageData
{
    private int _damage;

    public int Damage => GetDamage();
    public Vector3 HitPoint { get; private set; }
    public Vector3 HitNormal { get; private set; }
    public Character Attacker { get; private set; }

    public DamageData(int damage, Character attacker)
    {
        _damage = damage;
        Attacker = attacker;
    }

    private int GetDamage()
    {
        return _damage;
    }

    public void SetHit(Vector3 point, Vector3 normal)
    {
        HitPoint = point;
        HitNormal = normal;
    }
}