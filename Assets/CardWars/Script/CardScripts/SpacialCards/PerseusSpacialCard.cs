using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Perseus", menuName = ("Card/Special/Perseus"))]
public class PerseusSpacialCard : Card
{
    int _duration;
    public override void Special(GameObject target)
    {
        if (!isExpired())
        {
            _duration--;
            target.GetComponent<Player_Card>().hethBar.GetComponent<healthBarScript>().setShieldMAxValue(10f);
            float remainDamage = 0;
            float _damage = (float)target.GetComponent<Player_Card>()._gameManager.getCurrentCard().GetComponent<CardInfos>().card.attack;
            remainDamage = target.GetComponent<Player_Card>().hethBar.GetComponent<healthBarScript>().getShieldBarValue();
            float value = target.GetComponent<Player_Card>().hethBar.GetComponent<healthBarScript>().getShieldBarValue() - (float)target.GetComponent<Player_Card>()._gameManager.getCurrentCard().GetComponent<CardInfos>().card.attack;
            if (value < 0)
                value = 0;
            target.GetComponent<Player_Card>().hethBar.GetComponent<healthBarScript>().setShieldbarValue(value);
            _damage = _damage - remainDamage;

            cardAnims.cardAnimations.shakeAttaackAnim(target.GetComponent<Player>().GetComponent<Player_Card>()._gameManager.getCurrentTurn().GetComponent<Player_Card>()._avatar.transform, 10f, 1f);
            target.GetComponent<Player_Card>().GetComponent<Player_Card>()._gameManager.getCurrentTurn().GetComponent<Player_Card>().Damage(10);
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
//PerseusSpacialCard