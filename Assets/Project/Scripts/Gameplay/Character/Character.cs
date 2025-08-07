using System;
using NodeCanvas.BehaviourTrees;
using NodeCanvas.Framework;
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
    
    [Header("Blackboard")]
    [SerializeField] private BehaviourTreeOwner behaviourTreeOwner;

    [Inject] private IGlobalBlackboard globalBackboard;
    
    private ICharacterController _controller;
    private Inventory _inventory;
    
    // Controllers
    private WeaponController weaponController;
    private AbilityController abilityController;
    
    public MovementController MovementController => movementController;
    public WeaponController WeaponController => weaponController;
    public AbilityController AbilityController => abilityController;
    public AnimationController AnimationController => animationController;


    public BehaviourTreeOwner BehaviourTreeOwner => behaviourTreeOwner;
    public IBlackboard Blackboard => behaviourTreeOwner.blackboard;
    public IGlobalBlackboard GlobalBlackboard => globalBackboard;
    public CharacterModel Model { get; private set; }
    
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
        
        var sword = new Weapon("Меч", swordPrefab, 200, 2f);
        _inventory.AddItem(sword);
        weaponController.SetWeapon(_inventory.EquippedWeapon);

        Blackboard.AddVariable(BBKeys.GlobalBlackboard, globalBackboard);
        Blackboard.AddVariable(BBKeys.PlayerCharacter, this);
        
        _controller = controller;
        _controller.Init(this);

        Model = new CharacterModel(characterConfig.hp, characterConfig.mp);
        characterView.Init(this);
        
        Debug.Log("Character Initialized");
    }

    private void Update()
    {
        _controller?.Tick();
    }
}