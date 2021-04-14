using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Items/Weapon", order = 2)]
public class WeaponScriptable : EquipScriptable
{
    public WeaponStats weaponStats;

    public override void UseItem(PlayerController controller)
    {
        if (Equipped)
        {
            controller.WeaponHolder.UnequipWeapon();
        }
        else
        {
            controller.WeaponHolder.EquipWeapon(this);
        }

        base.UseItem(controller);
    }
}