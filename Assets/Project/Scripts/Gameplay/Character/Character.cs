using System;
using UnityEngine;
using Zenject;

public class Character : MonoBehaviour
{
    [SerializeField] private MovementController movementController;
    [SerializeField] private AnimationController animationController;
    [SerializeField] private AttackController attackController;
    [SerializeField] private WeaponController weaponController;
    [SerializeField] private CharacterView characterView;

    [Inject] private Blackboard globalBackboard;
    
    private ICharacterController _controller;
    private Inventory _inventory;
    
    public AttackController Attack => attackController;
    public MovementController Movement => movementController;
    public WeaponController WeaponController => weaponController;
    public LocalBlackboard Blackboard { get; private set; }
    
    //Tests
    [SerializeField] private GameObject swordPrefab;
    
    public void SetController(ICharacterController controller)
    {
        _inventory = new Inventory();
        
        attackController.Init(animationController);
        weaponController.Init(animationController);
        
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