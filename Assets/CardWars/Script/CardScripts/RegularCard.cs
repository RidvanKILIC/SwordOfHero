using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Regular Card", menuName = ("Card/Regular"))]
public class RegularCard : Card
{
    public override void Attack(GameObject enemy)
    {
        //base.Attack(enemy);
        var hit = enemy.GetComponent<CIDamagable>();
        if (hit != null)
            hit.Damage(attack); 
    }
    public override void Heal(GameObject owner)
    {
        //base.Heal(owner);
        var hit = owner.GetComponent<CIDamagable>();
        if (hit != null)
            hit.Heal(health);
    }

}
