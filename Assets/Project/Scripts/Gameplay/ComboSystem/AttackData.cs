public class WeaponCombo
{
    public AttackData[] Attacks;
}

public class AttackData
{
    public string AnimationName { get;private set; }
    public float StaminaCost{ get;private set; }

    public AttackData(string animationName, float staminaCost)
    {
        AnimationName = animationName;
        StaminaCost = staminaCost;
    }
}