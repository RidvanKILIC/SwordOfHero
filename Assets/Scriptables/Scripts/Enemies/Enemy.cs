using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : ScriptableObject
{
    public string _name;
    public Sprite avatarSprite;
    public int health;
    public int dropTopGold;
    public int dropBottomGold;
    public int dropTopGem;
    public int dropBottomGem;
    public int attack;
    public int dropTopExp;
    public int dropBottomExp;
    public int shield;
    public bool isDead;
    public float arenaEnterDureation;
    public GameObject Attackfx;
    public GameObject DamageFx;
    public EnemyType enemyType;
    public GameObject target;
    public AudioClip attackSFX;
    public AudioClip hitSFX;
    public AudioClip stepsSFX;
    public AudioClip deathSFX;
    public virtual void enterArena(GameObject obj, Transform pos) { /*Debug.Log("entered Arena");*/ }
    public virtual void Damage(float _damageAmount) {/* Debug.Log("Damage");*/ }
    public virtual void Attack(float _damageAmount) { /*Debug.Log("Attack");*/ }

}
public enum EnemyType
{
    minion,
    Boss,
}
