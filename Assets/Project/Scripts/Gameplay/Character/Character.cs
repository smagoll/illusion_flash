using System;
using NodeCanvas.BehaviourTrees;
using NodeCanvas.Framework;
using UnityEngine;
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

    [Inject] private IGlobalBlackboard globalBlackboard;

    private ICharacterController _controller;
    private Inventory _inventory;

    // Controllers
    private WeaponController weaponController;
    private AbilityController abilityController;
    private LockOnTargetSystem lockOnTargetSystem;
    private CharacterStateMachine stateMachine;
    private StatusEffectSystem statusEffectSystem;

    // Public properties
    public MovementController MovementController => movementController;
    public WeaponController WeaponController => weaponController;
    public AbilityController AbilityController => abilityController;
    public AnimationController AnimationController => animationController;
    public LockOnTargetSystem LockOnTargetSystem => lockOnTargetSystem;
    public StatusEffectSystem StatusEffectSystem => statusEffectSystem;
    public CharacterStateMachine StateMachine => stateMachine;
    public CombatSystem CombatSystem { get; private set; }

    public IBlackboard Blackboard => behaviourTreeOwner.blackboard;
    public IGlobalBlackboard GlobalBlackboard => globalBlackboard;
    public CharacterModel Model { get; private set; }
    public VFXHandler VFXHandler { get; private set; }

    // Test / Config
    [SerializeField] private WeaponView swordPrefab;
    [SerializeField] private CharacterConfig characterConfig;

    public void SetController(ICharacterController controller)
    {
        InitializeInventory();
        InitializeCombatSystem();
        InitializeControllers();
        InitializeLockOnSystem();
        InitializeWeapons();
        InitializeBlackboard();
        InitializeModelAndView();
        InitializeStatusEffectSystem();
        InitializeSystems();

        _controller = controller;
        _controller.Init(this);

        stateMachine = new CharacterStateMachine(this);

        Debug.Log("Character Initialized");
    }

    private void Update()
    {
        _controller?.Tick();
        Model.Tick(Time.deltaTime);
        stateMachine.Update();
    }

    private void InitializeInventory()
    {
        _inventory = new Inventory();
    }
    
    private void InitializeSystems()
    {
        VFXHandler = new VFXHandler(this);
    }

    private void InitializeControllers()
    {
        animationController.Init(modelFacade.animator, modelFacade.eventsHandler);
        movementController.Init(animationController);

        weaponController = new WeaponController(animationController, modelFacade.socketHolder, this);
        abilityController = new AbilityController(this, characterConfig.abilities);
    }

    private void InitializeStatusEffectSystem()
    {
        statusEffectSystem = new StatusEffectSystem(this);
    }

    private void InitializeCombatSystem()
    {
        CombatSystem = new CombatSystem(this);
    }
    
    private void InitializeLockOnSystem()
    {
        var targetLayer = LayerMask.GetMask("Character");
        lockOnTargetSystem = new LockOnTargetSystem(targetLayer, movementController);
    }

    private void InitializeWeapons()
    {
        var sword = new Weapon("Меч", swordPrefab, 10, 2f, new WeaponCombo()
        {
            Attacks = new []
            {
                new AttackData("Attack1", 5),
                new AttackData("Attack2", 5),
                new AttackData("Attack3", 5)
            }
        });
        _inventory.AddItem(sword);
        weaponController.SetWeapon(_inventory.EquippedWeapon);
    }

    private void InitializeBlackboard()
    {
        Blackboard.AddVariable(BBKeys.GlobalBlackboard, globalBlackboard);
        Blackboard.AddVariable(BBKeys.PlayerCharacter, this);
    }

    private void InitializeModelAndView()
    {
        Model = new CharacterModel(characterConfig.hp, characterConfig.mp, characterConfig.stamina);
        characterView.Init(this);
    }

    public void Block(bool isActive)
    {
        if (isActive)
        {
            stateMachine.TrySetState<CharacterBlockState>();
        }
        
        CombatSystem.ActivateBlock(isActive);
    }

    public void Stun(float duration)
    {
        statusEffectSystem.AddEffect(new StunEffect(duration));
    }

    private void OnDestroy()
    {
        VFXHandler.Dispose();
    }
}
