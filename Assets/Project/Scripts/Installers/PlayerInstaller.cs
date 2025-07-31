using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private Player.Player player;

    public override void InstallBindings()
    {
        Container.Bind<Player.Player>().FromInstance(player).AsSingle();
        
        Container.BindInterfacesAndSelfTo<PlayerController>().AsSingle();
    }
}