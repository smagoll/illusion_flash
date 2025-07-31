using System;
using UnityEngine;

public class Mana
{
    public int Current { get; private set; }
    public int Max { get; private set; }

    public event Action<int> OnChanged;

    public Mana(int max)
    {
        Max = max;
        Current = max;
    }

    public void Spend(int amount)
    {
        Current = Mathf.Max(Current - amount, 0);
        OnChanged?.Invoke(Current);
    }

    public void Restore(int amount)
    {
        Current = Mathf.Min(Current + amount, Max);
        OnChanged?.Invoke(Current);
    }
}