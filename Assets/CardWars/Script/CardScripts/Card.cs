using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public abstract class Card : ScriptableObject
{
    #region Variables
    [Header("Properties")]
    public new string name;
    public string description;
    public Sprite pattern;
    public Sprite cardSprite;
    public Sprite typeIcon;
    public Sprite charSprite;
    public int duration;
    public int attack;
    public int health;
    public Rarity rarity = new Rarity();
    public Type type = new Type();
    public GameObject fx;
    public GameObject trj;
    public GameObject gameObj;
    public SpacialType specialType;
    public AudioClip trjSFX;
    public AudioClip fxSFX;
    public AudioClip useSFX;
    public AudioClip spacialdemonstrationSFX;
    public AudioClip spacialReturnSFX;
    public AudioClip destroySFX;
    public callLocation Location;
    public startRunninTurn StartTime;
    public FunctionCallType FunctionType;
    #endregion
    public enum Rarity {Common,Rare,Epic,Legendary}
    public enum Type {Attack,Deffense,Special,Trap}
    public enum SpacialType
    {
        NonSpecial,
        Buff,
        Debuff
    }
    public enum callLocation
    {
        CardSelected,
        startOfTheTurn,
        BothCardSelection,
        BeforeScondCard,
        afterFirstCard,
        afterSecondCard,
        afterAttack,
        afterTakeDamage,
        beforeTakeDamage,
        AfterNonMatch,
        endOfTheTurn,
    }
    public enum startRunninTurn
    {
        sameTurn,
        nextTurn
    }
    public enum FunctionCallType
    {
        CallwithOriginalFunction,
        Replace
    }
    public virtual void Attack(GameObject enemmy) {/* Debug.Log("Attack to "+enemmy.name); */}
    public virtual void Heal(GameObject owner) {/*Debug.Log("Heal to "+owner.name);*/}
    public virtual void Special(GameObject target) {/* Debug.Log("Special"); */}
    public virtual void Initialize() {/* Debug.Log("Special"); */}
    public virtual bool isExpired() { return false; }
}
