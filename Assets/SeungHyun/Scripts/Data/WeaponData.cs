using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Data/Weapon")]
public class WeaponData : ItemData
{
    public WeaponType equipmentType;

    private void Reset()
    {
        type = ItemType.Weapon;
    }
    
}
