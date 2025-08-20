using System.Collections;
using UnityEngine;

public class ParrySystem
{
    private Character _character;
    private bool _isParryActive;
    private bool _onCooldown;
    private float _parryWindow = 0.5f;
    private float _parryCooldown = 0.6f;
    private Coroutine _parryCoroutine;
    
    public bool IsParryActive => _isParryActive;

    public ParrySystem(Character character)
    {
        _character = character;
    }

    public void TryParry()
    {
        if (_onCooldown) return;

        if (_parryCoroutine != null)
            _character.StopCoroutine(_parryCoroutine);

        _parryCoroutine = _character.StartCoroutine(ParryWindowCoroutine());
    }

    private IEnumerator ParryWindowCoroutine()
    {
        _onCooldown = true;
        _isParryActive = true;

        yield return new WaitForSeconds(_parryWindow);
        _isParryActive = false;

        yield return new WaitForSeconds(_parryCooldown);
        _onCooldown = false;
    }
}