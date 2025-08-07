using Input;
using NodeCanvas.Framework;
using UnityEngine;
using Zenject;

public class ServiceInstaller : MonoInstaller
{
    [SerializeField] private InputService _inputService;
    [SerializeField] private CameraService _cameraService;
    [SerializeField] private AssetBlackboard _globalBlackboard;

    public override void InstallBindings()
    {
        Container.Bind<IInputService>().FromInstance(_inputService).AsSingle();
        Container.Bind<ICameraService>().FromInstance(_cameraService).AsSingle();
        
        Container.Bind<IGlobalBlackboard>().FromInstance(_globalBlackboard).AsSingle();
    }
}