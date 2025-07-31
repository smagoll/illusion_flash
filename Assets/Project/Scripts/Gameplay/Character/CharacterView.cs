using UnityEngine;

public class CharacterView : MonoBehaviour, IDamageable
{
    private CharacterModel _model;

    public void Init(CharacterModel model)
    {
        _model = model;
    }

    public void TakeDamage(int amount)
    {
        _model.TakeDamage(amount);
    }
}