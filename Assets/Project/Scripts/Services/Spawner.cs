using NodeCanvas.BehaviourTrees;
using UnityEngine;
using Zenject;

public class Spawner : MonoBehaviour
{
    private IFactory<Character> _factory;
    private PlayerController _playerController;
    private ICameraService _cameraService;
    
    [Inject]
    private void Construct(IFactory<Character> characterFactory, PlayerController playerController, ICameraService cameraService)
    {
        _factory = characterFactory;
        _playerController = playerController;
        _cameraService = cameraService;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        SpawnPlayer();
        SpawnEnemy();
    }

    private void SpawnPlayer()
    {
        var player = _factory.Create();
        
        player.transform.position = new Vector3(0, 0, 0);
        
        player.SetController(_playerController);
        
        _cameraService.SetTarget(player.transform);
    }

    private void SpawnEnemy()
    {
        var enemy = _factory.Create();
        
        enemy.transform.position = new Vector3(0, 0, 10);
        
        enemy.SetController(new AIController());
    }
}