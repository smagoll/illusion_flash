using System.Collections.Generic;
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
        
        ConditionNode playerInRange = new ConditionNode(character =>
        {
            var player = character.transform;
            if (player == null) return false;
            float distance = Vector3.Distance(character.transform.position, player.position);
            return distance <= 10f;
        });

        ActionNode moveToPlayer = new ActionNode(character =>
        {
            var player = character.transform;
            if (player == null) return NodeState.Failure;

            Vector3 direction = player.position - character.transform.position;
            float distance = direction.magnitude;

            if (distance <= 0.5f)
            {
                character.Movement.Move(Vector2.zero);
                return NodeState.Success;
            }

            direction.y = 0;
            direction.Normalize();

            Vector3 forward = character.transform.forward;
            Vector3 right = character.transform.right;

            float inputX = Vector3.Dot(direction, right);
            float inputY = Vector3.Dot(direction, forward);

            Vector2 input = new Vector2(inputX, inputY);
            character.Movement.Move(input);

            return NodeState.Running;
        });
        
        var root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                playerInRange,
                moveToPlayer
            })
        });
        
        enemy.SetController(new AIController(root));
    }
}