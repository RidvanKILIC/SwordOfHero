using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Vehicle", menuName = ("Vehichle/New Vehicle"))]
public class Vehicle : BaseItem
{
    public Food foodNeeds;
    public int plusAttack;
    public int plusShield;
    public int weaklyCost;
    public int endurance;
    //public transportType _transportType;
    //public Spacialty spacialty;

    public enum transportType
    {
        Land,
        Sea,
        Air
    }
    public enum Spacialty
    {
        Long_Distance,
        Short_Distance,
    }
}
