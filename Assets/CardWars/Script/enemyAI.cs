using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class enemyAI : MonoBehaviour
{
    #region Variables
    [Header("References")]
    [SerializeField]GameManager _gameManager;
    [Header("Card Variables")]
    [SerializeField]List<GameObject> currentDeckList = new List<GameObject>();
    [SerializeField] List<GameObject> tmpCards = new List<GameObject>();
    [SerializeField]GameObject firstCard;
    int currentCardIndex;
    int randomRevealedIndex;
    float firstDelay = 0;
    float secondDelay = 0;
    [SerializeField] List<gameInfos.pairedCads> revealedPairs = new List<gameInfos.pairedCads>();
    bool isThereRevealedPair = false;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        clearObjects();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    bool checkRevealedPairs(GameObject cardToCheck)
    {
        bool resultToReturn = false;
        foreach(var item in revealedPairs)
        {
            if (item.firstCard == cardToCheck)
                resultToReturn = true;
            else if (item.secondCard == cardToCheck)
                resultToReturn = true;
        }
        return resultToReturn;
    }
    void checkRevealedPairs(int whichCard)
    {
        if (whichCard == 1)
        {
            //Debug.Log(whichCard + "cheking revealed pairs");
            revealedPairs.Clear();
            int tmpCardCount = tmpCards.Count - 1;
            for (int i = tmpCardCount; i >= 0; i--)
            {
                //Debug.Log(tmpCards.ElementAt(i).name);
                if (tmpCards.ElementAt(i).GetComponent<CardInfos>().getRevealed())
                {
                    GameObject currentCard = tmpCards.ElementAt(i);
                    GameObject pairCard = tmpCards.Find(x => x.name == currentCard.name && x != currentCard);
                    //Debug.Log(pairCard.name);
                    if (pairCard.gameObject.GetComponent<CardInfos>().getRevealed() && !checkRevealedPairs(pairCard))//------------------------------------------------------------------------->
                    {
                        gameInfos.pairedCads newPair = new gameInfos.pairedCads();
                        isThereRevealedPair = true;
                        newPair.firstCard = currentCard;
                        newPair.secondCard = pairCard;
                        newPair._cardTyp = currentCard.GetComponent<CardInfos>().getCardType();
                        revealedPairs.Add(newPair);
                    }
                }
            }
            delayedFirstCardPick();
        }
        else if (whichCard == 2)
        {
            delayedSecondCardPick();
           
        }
    }
    public void getCurrentCardsList(int whichCard)
    {
        
        //Debug.Log("current Deck List Called");
        currentDeckList = _gameManager.getDealedDeck();
        foreach(var item in currentDeckList)
        {
            tmpCards.Add(item);
        }
        checkRevealedPairs(whichCard);
        //Debug.Log(currentDeckList.Count + " " + tmpCards.Count);
        //tmpCards = _gameManager.getDealedDeck();

            
    }
    void pickFirstCard()
    {
        if (isThereRevealedPair)
        {
            randomRevealedIndex = Random.Range(0, revealedPairs.Count);
            currentCardIndex = currentDeckList.FindIndex(x => x.name == revealedPairs.ElementAt(randomRevealedIndex).firstCard.name);
            firstCard = currentDeckList[currentCardIndex];
        }
        else
        {
            firstCard = pickCard();
        }
        //Debug.Log("First Card:" + firstCard.name);
        currentDeckList.Remove(firstCard);
        firstCard.GetComponent<CardMovements>().flip();
    }
    void pickSecondCard()
    {
        //Debug.Log("First Card:" + firstCard.name);
        GameObject secondCard = getSecondCard(firstCard);
        //Debug.Log("Second Card:" + secondCard.name);
        secondCard.GetComponent<CardMovements>().flip();
        clearObjects();
    }
    GameObject pickCard()
    {

        //else
        //{
            currentCardIndex = Random.Range(0, currentDeckList.Count);
            //Debug.Log("Getting Random card");
        //}

        return currentDeckList.ElementAt(currentCardIndex);
    }
    GameObject getSecondCard(GameObject firstCard)
    {
        currentDeckList.Remove(firstCard);
        if (isThereRevealedPair)
        {
            //Debug.Log("There is revealed pair");
            GameObject cardToReturn = currentDeckList.Find(x => x.name == revealedPairs.ElementAt(randomRevealedIndex).secondCard.name && x != firstCard);
            return cardToReturn;
        }
        else
        {
            GameObject pair = currentDeckList.Find(x => x.name == firstCard.name && x != firstCard);
            if (pair.GetComponent<CardInfos>().getRevealed())
            {
                return pair;
            }
            else
            {
                return pickCard();
            }
        }

        
    }
    void clearObjects()
    {
        firstCard = null;
        isThereRevealedPair = false;
        currentDeckList.Clear();
        tmpCards.Clear();
    }
    public void startPickingCards(int whichCard)
    {
        //Debug.Log(whichCard + " picking");
        getCurrentCardsList(whichCard);

    }
    public void delayedFirstCardPick()
    {
        //Debug.Log("Picking First Card");
        firstDelay = Random.Range(0, 2);
        Invoke("pickFirstCard", firstDelay);
    }
    public void delayedSecondCardPick()
    {
        //Debug.Log("Picking Second Card with delay :" + (_gameManager.getCurrentTurn().GetComponent<Player>().getTimer().GetComponent<timebarScript>().getTime() - 4));
        float secondDelay = Random.Range(0, _gameManager.getCurrentTurn().GetComponent<Player_Card>().getTimer().GetComponent<timebarScript>().getMaxTime() - 3/*(firstDelay + 4), (firstDelay + 4) + _gameManager.getCurrentTurn().GetComponent<Player>().getTimer().GetComponent<timebarScript>().getTime() - 2*/);
        Invoke("pickSecondCard", secondDelay);
    }
}
