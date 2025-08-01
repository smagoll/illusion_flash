using Input;
using UnityEngine;
using Zenject;

public class ServiceInstaller : MonoInstaller
{
    [SerializeField] private InputService _inputService;
    [SerializeField] private CameraService _cameraService;

    public override void InstallBindings()
    {
        Container.Bind<IInputService>().FromInstance(_inputService).AsSingle();
        Container.Bind<ICameraService>().FromInstance(_cameraService).AsSingle();

        var blackboard = new Blackboard();
        
        Container.Bind<Blackboard>().FromInstance(blackboard).AsSingle();
    }
}