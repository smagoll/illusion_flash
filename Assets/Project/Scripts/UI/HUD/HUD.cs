using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private ProgressBar hpBar;
    [SerializeField] private ProgressBar staminaBar;
    
    private CharacterModel character;
    
    public void Init(CharacterModel model)
    {
        character = model;

        hpBar.SetMaxValue(character.Health.Max);
        staminaBar.SetMaxValue(character.Stamina.Max);
    }

    private void Update()
    {
        if (character == null) return;

        hpBar.SetValue(character.Health.Current);
        staminaBar.SetValue(character.Stamina.Current);
    }
}