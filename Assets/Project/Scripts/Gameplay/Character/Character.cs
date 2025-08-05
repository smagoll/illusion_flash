using System;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class Character : MonoBehaviour
{
    [Header("Controllers")]
    [SerializeField] private MovementController movementController;
    [SerializeField] private AnimationController animationController;
    [SerializeField] private CharacterView characterView;
    
    [Header("Model")]
    [SerializeField] private ModelFacade modelFacade;

    [Inject] private Blackboard globalBackboard;
    
    private ICharacterController _controller;
    private Inventory _inventory;
    
    // Controllers
    private WeaponController weaponController;
    private AbilityController abilityController;
    
    public MovementController MovementController => movementController;
    public WeaponController WeaponController => weaponController;
    public AbilityController AbilityController => abilityController;
    public AnimationController AnimationController => animationController;
    
    
    public LocalBlackboard Blackboard { get; private set; }
    
    //Tests
    [SerializeField] private WeaponView swordPrefab;
    [SerializeField] private CharacterConfig characterConfig;
    
    public void SetController(ICharacterController controller)
    {
        _inventory = new Inventory();
        
        animationController.Init(modelFacade.animator, modelFacade.eventsHandler);
        movementController.Init(animationController);
        weaponController = new WeaponController(animationController, modelFacade.socketHolder);
        abilityController = new AbilityController(this, characterConfig.abilities);
        
        var sword = new Weapon("Меч", swordPrefab, 25);
        _inventory.AddItem(sword);
        weaponController.SetWeapon(_inventory.EquippedWeapon);
        
        Blackboard = new LocalBlackboard(globalBackboard);
        
        _controller = controller;
        _controller.Init(this);

        var model = new CharacterModel(characterConfig.hp, characterConfig.mp);
        characterView.Init(model);
        
        Debug.Log("Character Initialized");
    }

    private void Update()
    {
        _controller?.Tick();
    }
}