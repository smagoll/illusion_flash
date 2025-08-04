using System;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class Character : MonoBehaviour
{
    [Header("Controllers")]
    [SerializeField] private MovementController movementController;
    [SerializeField] private AnimationController animationController;
    [FormerlySerializedAs("attackController")] [SerializeField] private AttackController attackControllerController;
    [SerializeField] private CharacterView characterView;
    
    [Header("Model")]
    [SerializeField] private ModelFacade modelFacade;

    [Inject] private Blackboard globalBackboard;
    
    private ICharacterController _controller;
    private Inventory _inventory;
    
    private WeaponController weaponController;
    
    public AttackController AttackController => attackControllerController;
    public MovementController Movement => movementController;
    public WeaponController WeaponController => weaponController;
    public LocalBlackboard Blackboard { get; private set; }
    
    //Tests
    [SerializeField] private GameObject swordPrefab;
    
    public void SetController(ICharacterController controller)
    {
        _inventory = new Inventory();
        
        animationController.Init(modelFacade.animator);
        attackControllerController.Init(animationController);
        weaponController = new WeaponController(animationController, modelFacade.socketHolder);
        
        var sword = new Weapon("Меч", swordPrefab, 25);
        _inventory.AddItem(sword);

        weaponController.SetWeapon(_inventory.EquippedWeapon);
        
        Blackboard = new LocalBlackboard(globalBackboard);
        
        _controller = controller;
        _controller.Init(this);

        var model = new CharacterModel(100, 100);
        characterView.Init(model);
        
        Debug.Log("Character Initialized");
    }

    private void Update()
    {
        _controller?.Tick();
    }

    public void ToggleWeapon()
    {
        weaponController.ToggleWeapon();
    }
}