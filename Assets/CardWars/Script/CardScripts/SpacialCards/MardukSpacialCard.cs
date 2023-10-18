using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Marduk", menuName = ("Card/Special/Marduk"))]
public class MardukSpacialCard : Card
{
    int _duration;
    public override void Special(GameObject target)
    {
        if (!isExpired())
        {
            //Debug.Log(isExpired());
            _duration--;
            //Debug.Log("Marduk Spacial");
            target.GetComponent<Player_Card>()._gameManager.delayedChangeTurnCall(3);
        }
        Debug.Log(isExpired());
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
//MardukSpacialCard