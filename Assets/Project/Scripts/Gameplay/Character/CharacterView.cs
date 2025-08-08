using UnityEngine;

public class CharacterView : MonoBehaviour, IDamageable, ITargetable
{
    private CharacterModel _model;
    private AnimationController _animationController;
    private MovementController _movementController;

    public void Init(Character character)
    {
        _model = character.Model;
        _animationController = character.AnimationController;
        _movementController = character.MovementController;

        _model.Health.OnDeath += OnDeath;
    }

    public void TakeDamage(int amount)
    {
        _model.TakeDamage(amount);
    }

    private void OnDeath()
    {
        _animationController.Death();
        _movementController.StopMove();
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public bool CanTarget => !_model.IsPlayer && !_model.IsDeath;
}