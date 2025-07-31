using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CharacterFactory : IFactory<Character>
{
    private readonly DiContainer _container;
    private readonly Character _characterPrefab;

    public CharacterFactory(DiContainer container, Character characterPrefab)
    {
        _container = container;
        _characterPrefab = characterPrefab;
    }

    public Character Create()
    {
        var character = _container.InstantiatePrefabForComponent<Character>(_characterPrefab);
        
        var root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                
            })
        });
        
        _container.Bind<ICharacterController>()
            .To<AIController>()
            .AsTransient()
            .WithArguments(root)
            .WhenInjectedInto<Character>();

        return character;
    }
}