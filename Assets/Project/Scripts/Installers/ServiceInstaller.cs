using Input;
using UnityEngine;
using Zenject;

public class ServiceInstaller : MonoInstaller
{
    [SerializeField] private InputService _inputService;

    public override void InstallBindings()
    {
        Container.Bind<IInputService>().FromInstance(_inputService).AsSingle();
    }
}
