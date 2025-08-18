using UnityEngine;

public class ComboSystem
{
    private WeaponCombo _combo;
    private int _currentStep = 0;

    private bool _canContinue;

    public bool CanContinue => _canContinue;

    public void SetCombo(WeaponCombo combo)
    {
        _combo = combo;
        ResetCombo();
    }

    public AttackData GetCurrentAttack()
    {
        if (_combo == null || _currentStep >= _combo.Attacks.Length)
            return null;
        return _combo.Attacks[_currentStep];
    }

    public void OnAttack()
    {
        _canContinue = false;
    }

    public void NextStep()
    {
        _currentStep++;
        if (_combo == null || _currentStep >= _combo.Attacks.Length)
            ResetCombo();
    }

    public void AllowNext() => _canContinue = true;

    public void ResetCombo()
    {
        _currentStep = 0;
        _canContinue = false;
    }
}