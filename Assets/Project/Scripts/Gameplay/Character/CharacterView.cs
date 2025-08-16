using TMPro;
using UnityEngine;

public class CharacterView : MonoBehaviour, IDamageable, ITargetable
{
    [SerializeField] private Transform lockOnPoint;
    
    private CharacterModel _model;
    
    private Character _character;

    public void Init(Character character)
    {
        _character = character;
        _model = character.Model;

        _model.Health.OnDeath += OnDeath;
    }

    public void TakeDamage(int amount)
    {
        _model.TakeDamage(amount);
        _character.StatusEffectSystem.AddEffect(new StunEffect(0.3f));
    }

    private void OnDeath()
    {
        _character.StateMachine.SetState<CharacterDeathState>();
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public bool CanTarget => !_model.IsPlayer && !_model.IsDeath;
    public Transform LockOnPoint => lockOnPoint;
}