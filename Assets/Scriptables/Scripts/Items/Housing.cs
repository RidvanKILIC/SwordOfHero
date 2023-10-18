using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Housing", menuName = ("Housing/New Housing"))]
public class Housing : BaseItem
{
    //public string _name;
    //public string description;
    //public Sprite sprite;
    public Sprite BG;
    public int weaklyCost;
    public int plusHealth;
    public int plusEnergy;
    public Fame fame;

    public enum Fame
    {
        Peasant,
        Noble,
        Duke,
        King
    }
}
