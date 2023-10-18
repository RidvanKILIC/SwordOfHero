using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class BaseItem : ScriptableObject
{
    public string _name;
    public string description;
    public Sprite sprite;
    public Status status;
    public int Cost;
    public Type type;
    public bool isEquipped;
    public bool isLock;
    public enum Status
    {
        Not_Bought,
        Bought
       
    }
    public enum Type
    {
        Common,
        rare,
        Epic,
        Legendary
    }
}
