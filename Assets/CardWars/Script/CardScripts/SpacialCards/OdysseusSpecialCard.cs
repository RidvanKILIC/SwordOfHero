using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Odysseus", menuName = ("Card/Special/Odysseus"))]
public class OdysseusSpecialCard : Card
{
    int _duration;
    List<GameObject> currentDeck = new List<GameObject>();
    public override void Special(GameObject target)
    {

        if (!isExpired())
        {
            Debug.Log(this.name + "spacial");
            _duration--;
            currentDeck = target.GetComponent<Player_Card>()._gameManager.getDealedDeck();
            GameObject firstCard = target.GetComponent<Player_Card>()._gameManager.getFirstCard();
            ///HighLight Second Card
            GameObject secondCard = currentDeck.Find(x => x.name == firstCard.name && x != firstCard);
            secondCard.GetComponent<CardInfos>().setRevealed(true);
            secondCard.GetComponent<CardInfos>().activateDeactivateHighLight(true);

            //if (target.GetComponent<Player>()._gameManager.getCurrentTurn().name == "Enemy")
            //{
            //    target.GetComponent<Player>()._gameManager.getCurrentTurn().GetComponent<enemyAI>().startPickingCards(2);
            //}
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
        currentDeck = new List<GameObject>();
        //base.Initialize();
    }
}
//OdysseusSpecialCard
