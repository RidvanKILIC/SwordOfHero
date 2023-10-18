using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sword", menuName = ("Weaponary/Sword/New Sword"))]
public class Weapon : BaseItem
{
    public int attack;
    public weaponType _weaponType;
    public enum weaponType
    {
        Axes,
        Blades,
        DS_Axes,
        FantasticKnifes,
        Hammers,
        Magic_hit,
        Spears,
        Staffs,
        Swords,
        Woods,
    }

}
