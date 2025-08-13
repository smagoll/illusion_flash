using UnityEngine;

public class Stamina
{
    public int Current { get; private set; }
    public int Max { get; private set; }

    public event System.Action OnExhausted;
    public event System.Action OnRecovered;

    private bool isExhausted;
    private float regenDelayTimer;
    private float regenRate;
    private float regenDelay;

    public Stamina(int maxStamina, float regenRatePerSecond = 15f, float regenDelaySeconds = 1.5f)
    {
        Max = maxStamina;
        Current = maxStamina;

        regenRate = regenRatePerSecond;
        regenDelay = regenDelaySeconds;
    }

    public void Use(int amount)
    {
        if (amount <= 0) return;

        Current = Mathf.Max(0, Current - amount);
        regenDelayTimer = regenDelay;

        if (Current == 0 && !isExhausted)
        {
            isExhausted = true;
            OnExhausted?.Invoke();
        }
    }

    public void Restore(int amount)
    {
        if (amount <= 0) return;

        int prev = Current;
        Current = Mathf.Min(Max, Current + amount);

        if (Current == Max && isExhausted)
        {
            isExhausted = false;
            OnRecovered?.Invoke();
        }
    }

    public void Tick(float deltaTime)
    {
        if (regenDelayTimer > 0)
        {
            regenDelayTimer -= deltaTime;
            return;
        }

        if (Current < Max)
        {
            Restore(Mathf.RoundToInt(regenRate * deltaTime));
        }
    }
}