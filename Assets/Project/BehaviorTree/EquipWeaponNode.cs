public class EquipWeaponNode : CharacterNodeBase
{
    protected override void OnExecute()
    {
        Character.WeaponController.DrawWeapon();

        if (!Character.WeaponController.IsWeaponDrawn)
        {
            return;
        }

        EndAction(true);
    }
}