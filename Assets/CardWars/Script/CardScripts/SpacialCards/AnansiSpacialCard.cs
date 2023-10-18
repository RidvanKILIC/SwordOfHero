using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Anansi", menuName = ("Card/Special/Anansi"))]
public class AnansiSpacialCard : Card
{
    int _duration;
    List<GameObject> currentDeck = new List<GameObject>();
    GameObject firstCard = null;
    GameObject secondCard = null;
    public void getRandomCard(GameObject target)
    {
        if (firstCard != null)
        {
            Debug.Log("SecondCard "+firstCard.name);
            _duration--;
            secondCard = currentDeck.Find(x => x.name != firstCard.name);
            //target.GetComponent<Player>()._gameManager.setChoosenCard(secondCard);
            secondCard.GetComponent<CardMovements>().flipRoutine();

        }
        else
        {
            
            Debug.Log("Picking FirstCard");
            int randCard = Random.Range(0, currentDeck.Count);
            firstCard = currentDeck[randCard].gameObject;
            //target.GetComponent<Player>()._gameManager.setChoosenCard(firstCard);
            firstCard.GetComponent<CardMovements>().flipRoutine();
        }
    }
    public override void Special(GameObject target)
    {
        if (!isExpired())
        {
            Debug.Log(isExpired());
            currentDeck.Clear();
            currentDeck = target.GetComponent<Player_Card>()._gameManager.getDealedDeck();
            getRandomCard(target);
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
        firstCard = null;
        secondCard = null;
        _duration = base.duration;
        //base.Initialize();
    }
}
