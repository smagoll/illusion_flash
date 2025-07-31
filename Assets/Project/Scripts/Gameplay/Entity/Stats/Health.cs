using System;
using UnityEngine;

public class Health
{
    public int Current { get; private set; }
    public int Max { get; private set; }

    public event Action OnDeath;
    public event Action<int> OnChanged;

    public Health(int max)
    {
        Max = max;
        Current = max;
    }
    
    public void TakeDamage(int amount)
    {
        Current = Mathf.Max(Current - amount, 0);
        OnChanged?.Invoke(Current);

        if (Current == 0)
            OnDeath?.Invoke();
    }
}