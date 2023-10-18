using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quetzalcoatl", menuName = ("Card/Special/Quetzalcoatl"))]
public class QuetzalcoatlSpacialCard : Card
{
    int _duration;
    public override void Special(GameObject target)
    {
        if (!isExpired())
        {
            _duration--;
            base.Heal(target);
            var hit = target.GetComponent<IDamagable>();
            if (hit != null)
            {
                Debug.Log(this.health);
                hit.IHeal(this.health);
            }
             
            target.GetComponent<Player_Card>().removeSpacialTypes(Card.SpacialType.Debuff);
        }
    }
    public override bool isExpired()
    {
        if (_duration <= 0)
            return true;
        else
            return false;
    }
    public override void Initialize()
    {
        _duration = base.duration;
        //base.Initialize();
    }
}


//QuetzalcoatlSpacialCard
