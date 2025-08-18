using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParrySystem
{
    private Character _character;
    private bool _isParryActive;
    private float _parryWindow = 0.5f;
    private Coroutine _parryCoroutine;
    
    public bool IsParryActive => _isParryActive;

    public ParrySystem(Character character)
    {
        _character = character;
    }

    public void TryParry()
    {
        if (_parryCoroutine != null)
            _character.StopCoroutine(_parryCoroutine);

        _parryCoroutine = _character.StartCoroutine(ParryWindowCoroutine());
    }

    private IEnumerator ParryWindowCoroutine()
    {
        _isParryActive = true;
        yield return new WaitForSeconds(_parryWindow);
        _isParryActive = false;
    }
}