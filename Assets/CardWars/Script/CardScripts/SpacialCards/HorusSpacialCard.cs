using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Horus", menuName = ("Card/Special/Horus"))]
public class HorusSpacialCard : Card
{
    int _duration;
    bool decreasedOnce = false;

    public override void Special(GameObject target)
    {
        if (!isExpired())
        {
            if (!decreasedOnce)
            {
                _duration--;
                Debug.Log(target.GetComponent<Player_Card>()._gameManager.gameObject.name);
                target.GetComponent<Player_Card>()._gameManager.getCurrentTurn().GetComponent<Player_Card>().getTimer().gameObject.GetComponent<timebarScript>().decreaseMaxTime(5f);
                decreasedOnce = true;
            }
            if (_duration <= 0)
            {
                target.GetComponent<Player_Card>()._gameManager.getCurrentTurn().GetComponent<Player_Card>().getTimer().gameObject.GetComponent<timebarScript>().updateMaxTime(target.GetComponent<Player_Card>()._gameManager.getCurrentTurn().GetComponent<Player_Card>().getPlayerTurnTime());
            }
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
        decreasedOnce = false;
        _duration = base.duration;
        //base.Initialize();
    }
}

