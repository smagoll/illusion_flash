using System;
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
        _model.Health.OnDeath += () => OnTargetLost?.Invoke();
    }

    public void TakeDamage(int amount)
    {
        _model.TakeDamage(amount);
        _character.StatusEffectSystem.AddEffect(new StunEffect(0.5f));
    }

    private void OnDeath()
    {
        _character.StateMachine.TrySetState<CharacterDeathState>();
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public bool CanTarget => !_model.IsPlayer && !_model.IsDeath;
    public Transform LockOnPoint => lockOnPoint;
    public event Action OnTargetLost;
}