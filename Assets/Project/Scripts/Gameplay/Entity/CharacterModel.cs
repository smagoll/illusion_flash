using UnityEngine;

public class CharacterModel
{
    public Health Health { get; }
    public Mana Mana { get; }

    public CharacterModel(int maxHealth, int maxMana)
    {
        Health = new Health(maxHealth);
        Mana = new Mana(maxMana);
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
}