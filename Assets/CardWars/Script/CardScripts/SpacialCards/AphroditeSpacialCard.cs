using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Aphrodite", menuName = ("Card/Special/Aphrodite"))]
public class AphroditeSpacialCard : Card
{
    public int _duration;
    public override void Special(GameObject target)
    {
        if (!isExpired())
        {
            //Debug.Log(this.name + "spacial");
            _duration--;
            target.GetComponent<Player_Card>()._gameManager.repeatTurn();
            
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
///AphroditeSpacialCard
