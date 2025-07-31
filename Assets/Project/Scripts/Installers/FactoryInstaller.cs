using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FactoryInstaller : MonoInstaller
{
    [SerializeField] private Character characterPrefab;

    public override void InstallBindings()
    {
        Container.BindIFactory<Character>()
            .FromComponentInNewPrefab(characterPrefab);
    }
}