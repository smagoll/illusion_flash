using System.Collections.Generic;

public class Inventory
{
    private List<Item> _items = new();

    public Weapon EquippedWeapon { get; private set; }

    public void AddItem(Item item)
    {
        _items.Add(item);
        if (EquippedWeapon == null && item is Weapon weapon)
            EquippedWeapon = weapon;
    }
}
