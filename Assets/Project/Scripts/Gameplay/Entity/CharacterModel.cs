using UnityEngine;

public class CharacterModel
{
    public Health Health { get; }
    public Mana Mana { get; }
    public Stamina Stamina { get; }
    
    public bool IsDeath { get; private set; }
    public bool IsPlayer { get; set; }

    public CharacterModel(int maxHealth, int maxMana, int maxStamina)
    {
        Health = new Health(maxHealth);
        Mana = new Mana(maxMana);
        Stamina = new Stamina(maxStamina);

        Health.OnDeath += Death;
    }
    
    public void Tick(float deltaTime)
    {
        Stamina.Tick(deltaTime);
    }
    
    public void TakeDamage(int amount)
    {
        Health.TakeDamage(amount);
        Debug.Log("Take damage");
    }

    public void RestoreMana(int amount)
    {
        Mana.Restore(amount);
    }
    
    public void UseStamina(float amount)
    {
        Stamina.Use(amount);
    }
    
    public void RestoreStamina(int amount)
    {
        Stamina.Restore(amount);
    }

    private void Death()
    {
        IsDeath = true;
    }
}