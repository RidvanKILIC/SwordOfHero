using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Food", menuName = ("Food/New Food"))]
public class Food : BaseItem
{
    //public string name;
    //public string description;
    //public Sprite sprite;
    public int weaklyCost;
    public int plusHealth;
    public int plusEnergy;
}
