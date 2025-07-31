using System;
using UnityEngine;
using Zenject;

public class Spawner : MonoBehaviour
{
    private IFactory<Character> _factory;
    
    [Inject]
    private void Construct(IFactory<Character> characterFactory)
    {
        _factory = characterFactory;
    }

    private void Start()
    {
        var enemy = _factory.Create();
        
        enemy.transform.position = new Vector3(0, 0, 10);
    }
}