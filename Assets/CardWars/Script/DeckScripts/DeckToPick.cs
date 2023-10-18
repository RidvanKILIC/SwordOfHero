using System;
using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using System.Linq;
using UnityEngine;
using cardAnims;
public class DeckToPick : MonoBehaviour
{
    #region Variables
    [Header("Deck Objects & Variables")]
    [SerializeField] Animator deckAnimator;
    [SerializeField] List<GameObject> dummyCardList = new List<GameObject>();
    [SerializeField]public List<Card> DeckList = new List<Card>();
    [SerializeField]public  List<GameObject> PosList = new List<GameObject>();
    [SerializeField]public  GameObject cardBack;
    [SerializeField]public GameObject cardPrefab;
    [SerializeField]TMPro.TMP_Text cardCountText;
    [SerializeField] TMPro.TMP_Text roundCountText;
    int totalCard ;
    [SerializeField]int totalRound;
    bool cardsDealed = false;
    [Header("References")]
    [SerializeField]GameManager _gameManager;
    [SerializeField]createDecks _createDeck;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
    /// <summary>
    /// Initiliazes variables & references
    /// </summary>
    void Init()
    {
        //_gameManager = GetComponent<GameManager>();
        cardBack.SetActive(true);
    }
    public void getNewDeck()
    {
        _createDeck.createNewDeck();
        _gameManager.setDeck();
    }
    void duplicateDeackList()
    {
        int length = (DeckList.Count - 1);


        for (int i =length ; i >= 0; i--)
        {
            //UnityEngine.Debug.Log(DeckList.ElementAt(i).name);
            if(!DeckList.ElementAt(i).type.Equals(Card.Type.Trap))
                DeckList.Add(DeckList[i]);
        }
        DeckList = DeckList.OrderBy(x => Guid.NewGuid()).ToList();
        //UnityEngine.Debug.Log("Decklist count after duplicate: "+DeckList.Count);
    }
    /// <summary>
    /// Initiliazes deck variables
    /// </summary>
    void initDeck(List<Card> _cardList, List<GameObject> _posList)
    {
        DeckList.Clear();

        DeckList = _cardList;
        PosList = _posList;
        duplicateDeackList();
        totalCard = DeckList.Count;
        setCardCountText(totalCard.ToString());
    }
    /// <summary>
    /// Assigns given decks and shuffles it then calls deal function
    /// </summary>
    /// <param name="choosenDec"></param>
    public void setDecks(List<Card> _cardList,List<GameObject> _posList)
    {

        initDeck(_cardList,_posList);
        //var stackTrace = new StackTrace();
        //var caller = stackTrace.GetFrame(1).GetMethod().Name;
        //UnityEngine.Debug.Log("setDecks " + "called by " + caller);
        //cardBack = playerInfos.choosenDeck.deckBack;Later

        if (!SaveLoad_Manager.gameData.isCardGamePlayedOnce)
        {
            tutorialController.TInstance.cardGameStartTutorial(GameObject.FindGameObjectWithTag("Managements").gameObject);
        }
        else
        {
            dealCards();
        }
    }

    /// <summary>
    /// return cardsDealed variable
    /// </summary>
    /// <returns></returns>
    public bool isCardsDealed()
    {
        return cardsDealed;
    }
    /// <summary>
    /// Assign given parameter into cardsDealed variable
    /// </summary>
    /// <returns></returns>
    public void setCardsDealed(bool _state)
    {
        cardsDealed = _state;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// Assign given parameter cardCountText's text properties
    /// </summary>
    /// <param name="_count"></param>
    void setCardCountText(string _count)
    {
        cardCountText.text = _count;
    }
    /// <summary>
    /// Assign given parameter roundCountText's text properties
    /// </summary>
    /// <param name="_count"></param>
    void setRoundCountText(string _count)
    {
        roundCountText.text ="X"+ _count;
    }
    /// <summary>
    /// Starts deal coroutine
    /// </summary>
    public void dealCards()
    {
        //var stackTrace = new StackTrace();
        //var caller = stackTrace.GetFrame(1).GetMethod().Name;
        //UnityEngine.Debug.Log("dealCards " + "called by " + caller);
        _gameManager.setPickingCardAvailable(false);
        setCardsDealed(false);
        StartCoroutine(deal());
    }
    ///// <summary>
    ///// Takes Card list and turns it to cardPrefabs list and return it
    ///// </summary>
    ///// <param name="cardList"></param>
    ///// <returns></returns>
    //List<GameObject> createCardObjects(List<Card> cardList)
    //{
    //    List<GameObject> deck = new List<GameObject>();
    //    for (int i = 0; i < cardList.Count; i++)
    //    {

    //        GameObject card = attachCardToPrefab(cardList.ElementAt(i));
    //        deck.Add(card);
    //        card.SetActive(false);

    //    }
    //    return deck;
    //}
    /// <summary>
    /// Attacch given Card paramaeter to Card prefab then return it 
    /// </summary>
    /// <param name="_card"></param>
    /// <returns></returns>
    GameObject attachCardToPrefab(Card _card)
    {
        GameObject cardObj = GameObject.Instantiate(cardPrefab);
        cardObj.GetComponent<CardInfos>().card = _card;
        cardObj.GetComponent<CardInfos>().Init();
        cardObj.gameObject.name = cardObj.GetComponent<CardInfos>().card.name;
        //UnityEngine.Debug.Log("Dealing... " + card.name);
        return cardObj;
    }
    IEnumerator deal()
    {
        foreach(var item in dummyCardList)
        {
            item.SetActive(true);
        }
        setRoundCountText(totalRound.ToString());
        yield return new WaitForSeconds(0.5f);
        DeckList = DeckList.OrderBy(x => Guid.NewGuid()).ToList();
        //UnityEngine.Debug.Log("Deck List Cont: "+DeckList.Count);
        if (totalRound > 0)
        {
            
            deckAnimator.SetBool("openCloseDoor", true);
            deckAnimator.SetTrigger("start");
            yield return new WaitForSeconds(1);
            for (int i = 0; i < (PosList.Count); i++)
            {
                //Debug.Log("Total Count: "+(PosList.Count / 2));
                //Debug.Log("First Loop: "+i);
                if(DeckList[i] != null)
                {
                    GameObject card = attachCardToPrefab(DeckList[i]); /*Instantiate(DeckList[i], transform.position, Quaternion.identity);*/
                    _gameManager.addCardToDealedDeck(card);
                    //if (card.name.Equals("cardPrefab(Clone)"))
                    //{
                    //    UnityEngine.Debug.LogError("Null");
                    //}
                        
                    //UnityEngine.Debug.Log("Dealing... "+DeckList[i].name);
                    _gameManager.clearObjectName(card);
                    card.transform.position = dummyCardList.ElementAt(i).transform.position;/*cardBack.transform.position*/;
                    dummyCardList.ElementAt(i).gameObject.SetActive(false);
                    card.transform.SetParent(PosList[(i)].transform);
                    card.transform.localScale = Vector3.one;
                    //card.GetComponent<RectTransform>().sizeDelta = PosList[(i)].GetComponent<RectTransform>().sizeDelta;
                    //card.transform.localScale= new Vector3(0.3f, 0.3f, 0.3f);
                    card.transform.eulerAngles = new Vector3(-80, 0,20 /*UnityEngine.Random.Range(10f, 60f)*/);  //Quaternion.Euler(0f, 0f,UnityEngine.Random.Range(10f,60f));
                    card.GetComponent<CardMovements>().currentParent = PosList[(i)];
                    card.gameObject.SetActive(true);
                    cardAnims.cardAnimations.cardRotateAnim(card.transform, 0.5f,Vector3.zero);
                    cardAnims.cardAnimations.changeCardSizeAnim(card.GetComponent<RectTransform>(), PosList[(i)].GetComponent<RectTransform>().sizeDelta,0.5f);
                    //_cadAnimations.cardScaleUpAnim(card.transform, 1f);
                    cardAnims.cardAnimations.cardkMovePosAnim(card.transform, 0.5f,false,cardSounds.SInstance.cardDealingSFX);
                    //card.GetComponent<CardMovements>().moveCard(PosList[(i)].transform.position);
                    totalCard--;
                    setCardCountText(totalCard.ToString());
                    //soundManager.SInstance.playSfx(soundManager.SInstance.cardDealingSFX);
                    yield return new WaitForSeconds(0.5f);
                    //for(int j = 0; j < 2; j++)
                    //{
                    //    Debug.Log("Second Loop: " + (i+j));
                    //    GameObject card = Instantiate(DeckList[i], transform.position, Quaternion.identity);
                    //    gameManager.clearObjectName(card);
                    //    card.transform.SetParent(PosList[(i+j)].transform);
                    //    card.transform.localScale = Vector3.one;
                    //    card.GetComponent<RectTransform>().sizeDelta = PosList[(i+j)].GetComponent<RectTransform>().sizeDelta;
                    //    card.GetComponent<CardMovements>().currentParent = PosList[(i+j)];
                    //    card.GetComponent<CardMovements>().moveCard(PosList[(i+j)].transform.position);
                    //    totalCard--;
                    //    setCardCountText(totalCard.ToString());
                    //    yield return new WaitForSeconds(0.5f);
                    //}
                }
                else
                {
                    UnityEngine.Debug.Log("Card is null");
                }

            }
            yield return new WaitForSeconds(1);
            deckAnimator.SetBool("openCloseDoor", false);
            _gameManager.activateDeactivateLayoutGroups(false);
            yield return new WaitForSeconds(1f);
            _gameManager.OpenClosePlayerCardPickPanel(true);
            setCardsDealed(true);
            totalRound--;
            _gameManager.changeTurn();
        }
        else
        {
            _gameManager.gameOver();
            /// End Game
            cardBack.SetActive(false);
        }

        //_gameManager.changeTurn();
        //_gameManager.startTimer();

        //_gameManager.setPickingCardAvailable(true);

        //for(int i=0;i<PlayerDeckList.Count;i++)
        //{
        //    GameObject card = Instantiate(PlayerDeckList[i], transform.position, Quaternion.identity);
        //    card.transform.SetParent(playerPosList[i].transform);
        //    card.transform.localScale = Vector3.one;
        //    card.GetComponent<RectTransform>().sizeDelta = playerPosList[i].GetComponent<RectTransform>().sizeDelta;
        //    card.GetComponent<CardMovements>().moveCard(playerPosList[i].transform.position);
        //    totalCard--;
        //    setCardCountText(totalCard.ToString());
        //    yield return new WaitForSeconds(0.5f);
        //}
        //for (int i = 0; i < playerPickPosList.Count; i++)
        //{
        //    GameObject card = Instantiate(PlayerDeckList[i], transform.position, Quaternion.identity);
        //    card.transform.SetParent(playerPickPosList[i].transform);
        //    card.transform.localScale = Vector3.one;
        //    card.transform.position = Vector3.zero;
        //    card.GetComponent<RectTransform>().sizeDelta = playerPickPosList[i].GetComponent<RectTransform>().sizeDelta;
        //    card.GetComponent<CardMovements>().moveCard(playerPickPosList[i].transform.position);
        //}

        //yield return new WaitForSeconds(1f);
        //for (int i = 0; i < EnemyDeckList.Count; i++)
        //{
        //    GameObject card = Instantiate(EnemyDeckList[i], transform.position, Quaternion.identity);
        //    card.transform.SetParent(EnemyPosList[i].transform);
        //    card.transform.localScale = Vector3.one;
        //    card.GetComponent<RectTransform>().sizeDelta = EnemyPosList[i].GetComponent<RectTransform>().sizeDelta;
        //    card.GetComponent<CardMovements>().moveCard(EnemyPosList[i].transform.position);

        //    totalCard--;
        //    setCardCountText(totalCard.ToString());
        //    yield return new WaitForSeconds(0.5f);
        //}
        //for (int i = 0; i < EnemyPickPosList.Count; i++)
        //{
        //    GameObject card = Instantiate(EnemyDeckList[i], transform.position, Quaternion.identity);
        //    card.transform.SetParent(EnemyPickPosList[i].transform);
        //    card.transform.localScale = Vector3.one;
        //    card.transform.position = Vector3.zero;
        //    card.GetComponent<RectTransform>().sizeDelta = EnemyPickPosList[i].GetComponent<RectTransform>().sizeDelta;
        //    card.GetComponent<CardMovements>().moveCard(EnemyPickPosList[i].transform.position);
        //}

    }

}
