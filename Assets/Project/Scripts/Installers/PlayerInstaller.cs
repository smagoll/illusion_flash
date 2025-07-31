using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private Character character;

    public override void InstallBindings()
    {
        Container.Bind<Character>().FromInstance(character).AsSingle();
        
        Container.BindInterfacesAndSelfTo<PlayerController>()
            .AsSingle()
            .OnInstantiated<PlayerController>((ctx, controller) =>
            {
                character.SetController(controller);
            });
    }
}