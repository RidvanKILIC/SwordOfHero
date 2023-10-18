using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using cardAnims;

public class GameManager : MonoBehaviour
{
    #region Variables

    [Header("Panels and Layouts")]
    [SerializeField]public GameObject Canvas;
    //[SerializeField] GameObject PosList4x4;
    //[SerializeField] GameObject PosList5x5;
    //[SerializeField] GameObject PosList5x7;
    [SerializeField] GameObject scaleUpPanel;
    [SerializeField] GameObject scaleUpCardSlot;
    [SerializeField] useCardScript _useCardScript;
    [SerializeField] GameObject battleLockPanel;
    [SerializeField] GameObject demonstrationPanel;
    [SerializeField] GameObject warningPanel;
    [SerializeField] public GameObject specialCardPosLeft, specialCardPosRight;
    [Header("Text Objects")]
    [SerializeField] TMPro.TMP_Text warningText;
    [Header("Control Variables")]
    bool isCardsMatched = false;
    [SerializeField] bool pickingCardAvailable = true;
    [SerializeField] bool matchCardPanelIsActive=false;
    [SerializeField] bool timeIsUp = false;
    [Header("Card Variables")]
    [SerializeField] List<gameInfos.gameDecks> gameModes = new List<gameInfos.gameDecks>();
    //[SerializeField] gameInfos.gameDecks easyGameDeck=new gameInfos.gameDecks();
    //[SerializeField] gameInfos.gameDecks normalGameDeck = new gameInfos.gameDecks();
    //[SerializeField] gameInfos.gameDecks hardGameDeck = new gameInfos.gameDecks();
    [SerializeField] List<GameObject> dealedDeck = new List<GameObject>();
    GameObject cardToScaleUp;
    CardInfos currentCard;
    GameObject cardToAddHand;
    [Header("Picked Card References")]
    [SerializeField]List<GameObject> choosenCards = new List<GameObject>();
    GameObject choosenPosList;
    DeckToPick _deckToPick;
    [Header("Turn Objects & References")]
    [SerializeField]public List<GameObject> playerList;
    [SerializeField]GameObject currentTurn=null;
    [SerializeField] GameObject pastTurn = null;
    [SerializeField] GameObject scoreBoard;
    [SerializeField]gameTimer _gameTimer;
    int indexOfCurrentPlayer = 0;
    public bool firstCardSelected = false;
    public bool firtsCardsMatched = false;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        //Init();
    }
    /// <summary>
    /// Initializes objects and references
    /// </summary>
    public void Init()
    {

        Time.timeScale = 1;
        //SoundManager.SInstance.adjustTheme();
        if (!SaveLoad_Manager.gameData.isCardGamePlayedOnce)
        {
            indexOfCurrentPlayer = 1;
        }
        else
        {
            indexOfCurrentPlayer = UnityEngine.Random.Range(0, playerList.Count);
        }
        //gameInfos.choosenDeck.deckList = choosenDeck.deckList;
        //_useCardPanel = GameObject.FindObjectOfType<useCardPanel>();
        //_gameOver = GameObject.FindObjectOfType<gameOverScript>();
        //_gameOverScript.openCloseGameOverPanel(false);
        _deckToPick = GameObject.FindObjectOfType<DeckToPick>();
        setDeck();
        //closeScaleUpPanel();
        lockUnlockBattlePanel(false);    
        //if (_deckToPick.isCardsDealed())
            
    }
    /// <summary>
    /// Assigns Decs properties
    /// </summary>
    public void setDeck()
    {
        gameInfos.gameDecks choosenMode;
        choosenMode = gameModes.Find(x => x._difficulty.Equals(gameInfos.choosenDeck.gameDifficulty));
        if (choosenMode.PosList != null)
        {
            choosenPosList = choosenMode.PosList;
            choosenMode.deckList = gameInfos.choosenDeck.gameDeck;
        }

        //choosenDeck.deckList = gameInfos.choosenDeck.deckList;
        //choosenDeck.gameDifficulty = gameInfos.choosenDifficulty;
        //switch (choosenDeck.gameDifficulty)
        //{
        //    case gameInfos.difficulty.EASY:
        //        choosenPosList = PosList4x4;
        //        break;
        //    case gameInfos.difficulty.NORMAL:
        //        choosenPosList = PosList5x5;
        //        break;
        //    case gameInfos.difficulty.HARD:
        //        choosenPosList = PosList5x7;
        //        break;
        //    default:
        //        choosenPosList = PosList4x4;
        //        break;
        //}
        //choosenDeck.PosList = getPositions(choosenPosList);

        activateDeactivateLayoutGroups(true);
        _deckToPick.setDecks(choosenMode.deckList,getPositions(choosenMode.PosList));
    }
    /// <summary>
    /// Activates deactivates current card layout's layoutGroup component
    /// </summary>
    /// <param name="state"></param>
    public void activateDeactivateLayoutGroups(bool state)
    {
       choosenPosList.GetComponent<FlexibleLayoutGroup>().enabled=state;
    }
    /// <summary>
    /// Returns parameter childs as a list of objects
    /// </summary>
    /// <param name="layout"></param>
    /// <returns></returns>
    List<GameObject> getPositions(GameObject layout)
    {
        List<GameObject> objs = new List<GameObject>();
        foreach(Transform child in layout.transform)
        {
            objs.Add(child.gameObject);
        }
        return objs;

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    #region Discard
    public void OpenClosePlayerCardPickPanel(bool _state)
    {
        //PlayerCardPickPanel.GetComponent<Animator>().SetBool("OpenClosePickPanel", _state);
        //if (_state)
        //    PlayerCardPickPanel.transform.localScale=Vector3.one;
        //else
        //    PlayerCardPickPanel.transform.localScale = Vector3.zero;
    }
    public void OpenCloseEnemyCardPickPanel(bool _state)
    {
        //EnemyCardPickPanel.GetComponent<Animator>().SetBool("OpenClosePickPanel", _state);
        //if (_state)
        //    EnemyCardPickPanel.transform.localScale = Vector3.one;
        //else
        //    EnemyCardPickPanel.transform.localScale = Vector3.zero;
    }
    #endregion

    public void delayedChangeTurnCall(float delay)
    {
        Invoke("changeTurn", delay);
    }

    /// <summary>
    /// Changes the turn
    /// </summary>
    public void changeTurn()
    {
        if (!SaveLoad_Manager.gameData.isCardGamePlayedOnce)
        {
            if (!firstCardSelected)
            {
                tutorialController.TInstance.cardGameTutorial(1, false, GameObject.FindGameObjectWithTag("Managements").gameObject);
            }
            else
            {
                SaveLoad_Manager.gameData.isCardGamePlayedOnce = true;
                SaveLoad_Manager.SInstance.saveJson();
                if(firtsCardsMatched)
                    tutorialController.TInstance.cardGameTutorial(2, true, GameObject.FindGameObjectWithTag("Managements").gameObject);
                else
                    tutorialController.TInstance.cardGameTutorial(2, false, GameObject.FindGameObjectWithTag("Managements").gameObject);

            }
                
        }
        else
        {
            if (getCurrentTurn() != null)
            {
                getCurrentTurn().GetComponent<Player_Card>().handleUsedSpacial();
            }

            if (_gameTimer.getTimeIsUp())
            {
                gameOver();
            }
            else
            {
                StartCoroutine(changeTurnDelayed());
            }
        }

    }
    public void repeatTurn() 
    {
        //getCurrentTurn().GetComponent<Player>().handleUsedSpacial();
        //StartCoroutine(repeatTurnDelayed());
        Debug.Log("Current Turn" + getCurrentTurn().name);
        getCurrentTurn().GetComponent<Player_Card>().activateDeactivateTurn(true);
        if (getCurrentTurn().GetComponent<Player_Card>().getSpacialList().Count > 0)
        {
            List<GameObject> specList = getCurrentTurn().GetComponent<Player_Card>().getSpacialList();

            for (int i = 0; i < specList.Count; i++)
            {
                if (specList[i] != null)
                {
                    if (specList[i].GetComponent<CardInfos>().card.Location.Equals(Card.callLocation.BothCardSelection) && specList[i].GetComponent<CardInfos>().card.FunctionType.Equals(Card.FunctionCallType.Replace))
                    {
                        showAndUseSpecialCard(specList[i], getCurrentTurn(), false, getCurrentTurn());
                    }
                }
            }


            //foreach (var item in specList)
            //{
            //    if (item.GetComponent<CardInfos>().card.Location.Equals(Card.callLocation.BothCardSelection) && item.GetComponent<CardInfos>().card.FunctionType.Equals(Card.FunctionCallType.Replace))
            //    {
            //        showAndUseSpecialCard(item, getCurrentTurn(), false,item.GetComponent<CardInfos>().owner);
            //    }
            //}
        }
        //if (getCurrentTurn().GetComponent<Player>().getSpacialList().Count > 0)
        //{
        //    List<GameObject> specList = getCurrentTurn().GetComponent<Player>().getSpacialList();
        //    foreach (var item in specList)
        //    {
        //        if (item.GetComponent<CardInfos>().card.Location.Equals(Card.callLocation.startOfTheTurn))
        //        {
        //            showAndUseSpecialCard(item, getCurrentTurn(), true);
        //        }
        //        if (item.GetComponent<CardInfos>().card.Location.Equals(Card.callLocation.BothCardSelection))
        //        {
        //            showAndUseSpecialCard(item, getCurrentTurn(), false);
        //        }
        //    }
        //}

        setTimers(pastTurn, currentTurn);
    }
    #region Discarded repeatTurnDelayed
    //IEnumerator repeatTurnDelayed()
    //{
    //    Debug.Log("Current Turn" + getCurrentTurn().name);
    //    getCurrentTurn().GetComponent<Player>().activateDeactivateTurn(true);

    //    if (getCurrentTurn().GetComponent<Player>().getSpacialList().Count > 0)
    //    {
    //        List<GameObject> specList = getCurrentTurn().GetComponent<Player>().getSpacialList();
    //        foreach (var item in specList)
    //        {
    //            if (item.GetComponent<CardInfos>().card.Location.Equals(Card.callLocation.BothCardSelection) && item.GetComponent<CardInfos>().card.FunctionType.Equals(Card.FunctionCallType.Replace))
    //            {
    //                showAndUseSpecialCard(item, getCurrentTurn(), null);
    //            }
    //        }
    //    }
    //    yield return new WaitForSeconds(1f);
    //    if (currentTurn.gameObject.name == "Enemy")
    //    {
    //        currentTurn.GetComponent<enemyAI>().pickCards();
    //    }


    //    if (getCurrentTurn().GetComponent<Player>().getSpacialList().Count > 0)
    //    {
    //        List<GameObject> specList = getCurrentTurn().GetComponent<Player>().getSpacialList();
    //        foreach (var item in specList)
    //        {
    //            if (item.GetComponent<CardInfos>().card.Location.Equals(Card.callLocation.startOfTheTurn))
    //            {
    //                showAndUseSpecialCard(item, getCurrentTurn(), () => item.GetComponent<CardInfos>().card.Special(getCurrentTurn()));
    //            }
    //        }
    //    }

    //    setTimers(pastTurn, currentTurn);
    //}
    #endregion
    IEnumerator changeTurnDelayed()
    {


        yield return new WaitForSeconds(1);
        //lockUnlocPlayersHands(false);
        pastTurn = null;
        if (currentTurn != null)
        {
            pastTurn = currentTurn;
            pastTurn.GetComponent<Player_Card>().activateDeactivateTurn(false);
            pastTurn.GetComponent<Player_Card>().clearSpacialList();

        }
        currentTurn = playerList.ElementAt(indexOfCurrentPlayer);
        getCurrentTurn().GetComponent<Player_Card>().handleUsedSpacial();
        currentTurn.GetComponent<Player_Card>().activateDeactivateTurn(true);
        //setTimers(pastTurn, currentTurn);
        indexOfCurrentPlayer++;
        if (indexOfCurrentPlayer >= playerList.Count)
        {
            indexOfCurrentPlayer = 0;
        }
        //if (getCurrentTurn().GetComponent<Player>().getSpacialList().Count > 0)
        //{
        //    List<GameObject> specList = getCurrentTurn().GetComponent<Player>().getSpacialList();
        //    foreach (var item in specList)
        //    {
        //        if (item.GetComponent<CardInfos>().card.Location.Equals(Card.callLocation.BothCardSelection) && item.GetComponent<CardInfos>().card.FunctionType.Equals(Card.FunctionCallType.Replace))
        //        {
        //            showAndUseSpecialCard(item, getCurrentTurn(), null);
        //        }
        //    }
        //}
        yield return new WaitForSeconds(1);
       
        setTimers(pastTurn, currentTurn);
    }
    /// <summary>
    /// Sets timers foreach player for pastturn & current turn
    /// </summary>
    /// <param name="pastTurn"></param>
    /// <param name="currentTurn"></param>
    public void setTimers(GameObject pastTurn,GameObject currentTurn)
    {
        if (pastTurn != null)
        {
            //pastTurn.GetComponent<Player>().getTimer().GetComponent<timebarScript>().activateDeactivateTimeBar(false);
            pastTurn.GetComponent<Player_Card>().getTimer().GetComponent<timebarScript>().resetTimer();
            pastTurn.GetComponent<Player_Card>().getTimer().GetComponent<timebarScript>().pauseResumeTimer(false);
            pastTurn.GetComponent<Player_Card>().getTimer().GetComponent<timebarScript>().startStopTimer(false);
           
        }
        currentTurn.GetComponent<Player_Card>().getTimer().GetComponent<timebarScript>().resetTimer();
        currentTurn.GetComponent<Player_Card>().getTimer().GetComponent<timebarScript>().pauseResumeTimer(false);
        //currentTurn.GetComponent<Player>().getTimer().GetComponent<timebarScript>().activateDeactivateTimeBar(true);
        currentTurn.GetComponent<Player_Card>().getTimer().GetComponent<timebarScript>().startStopTimer(false);
        if (_deckToPick.isCardsDealed())
            startTimer();
    }
    /// <summary>
    /// Starts current turns timer
    /// </summary>
    public void startTimer()
    {
        bool callFunction = true;
        if (getCurrentTurn().GetComponent<Player_Card>().getSpacialList().Count > 0)
        {
            List<GameObject> specList = getCurrentTurn().GetComponent<Player_Card>().getSpacialList();
            for(int i = 0; i < specList.Count; i++)
            {
                if (specList[i] != null)
                {
                    if(specList[i].GetComponent<CardInfos>().card.Location.Equals(Card.callLocation.startOfTheTurn))
                    {
                        if (specList[i].GetComponent<CardInfos>().card.FunctionType.Equals(Card.FunctionCallType.Replace))
                        {
                            callFunction = false;
                        }
                        showAndUseSpecialCard(specList[i], getCurrentTurn(), true, getCurrentTurn());
                    }
                    if (specList[i].GetComponent<CardInfos>().card.Location.Equals(Card.callLocation.BothCardSelection))
                    {
                        showAndUseSpecialCard(specList[i], getCurrentTurn(), false, null);
                    }
                }
            }
        }
        if (callFunction)
        {
            if (currentTurn.gameObject.name == "Enemy")
            {
                currentTurn.GetComponent<enemyAI>().startPickingCards(1);
            }
            timeIsUp = false;
            currentTurn.GetComponent<Player_Card>().getTimer().GetComponent<timebarScript>().startStopTimer(true);
            setPickingCardAvailable(true);
        }
    }
    /// <summary>
    /// Stops current turns timer
    /// </summary>
    public void stopTimer()
    {
        //timeIsUp = false;
        currentTurn.GetComponent<Player_Card>().getTimer().GetComponent<timebarScript>().startStopTimer(false);
    }
    public void pauseResumeTimer(bool _state)
    {
        currentTurn.GetComponent<Player_Card>().getTimer().GetComponent<timebarScript>().pauseResumeTimer(_state);
    }
    /// <summary>
    /// Calls given players lockUnlockPlayerHand function with the given _state parameter
    /// </summary>
    /// <param name="_state"></param>
    /// <param name="player"></param>
    //public void lockUnlocPlayersHands( bool _state, GameObject player = null)
    //{
    //    if (player != null)
    //    {
    //        //Debug.Log("Player null");
    //        player.GetComponent<Player>().hand.lockUnlockPlayerHand(_state);
    //    }
    //    else
    //    {
    //        //Debug.Log("Player not null");
    //        foreach (var item in playerList)
    //        {
    //            item.GetComponent<Player>().hand.lockUnlockPlayerHand(_state);
    //        }
    //    }

    //}
    /// <summary>
    /// Assings given parameter into pickingCardAvailable variable
    /// </summary>
    /// <param name="_state"></param>
    public void setPickingCardAvailable(bool _state)
    {
        pickingCardAvailable = _state;
    }
    /// <summary>
    /// Returns pickingCardAvailable variable
    /// </summary>
    /// <returns></returns>
    public bool getPickingCardAvailable()
    {
        return pickingCardAvailable;
    }
    /// <summary>
    /// Assings given parameter into matchCardPanelIsActive variable
    /// </summary>
    /// <param name="_state"></param>
    public void setMatchCardPanelIsActive(bool _state)
    {
        matchCardPanelIsActive = _state;
    }
    /// <summary>
    /// Returns matchCardPanelIsActive variable
    /// </summary>
    /// <returns></returns>
    public bool getMatchCardPanelIsActive()
    {
        return matchCardPanelIsActive;
    }
    GameObject getOwner()
    {
        return currentTurn;
    }
    GameObject getEnemy()
    {
        return playerList[indexOfCurrentPlayer];
    }
    public void showAndUseSpecialCard(GameObject specialCard,GameObject _owner, bool callFunction,GameObject target)
    {
        _owner.GetComponent<Player_Card>().demonstrateSpecial(specialCard,callFunction,target);
    }
    #region Discarded
    IEnumerator specialCardMovementRoutine(GameObject specialCard, GameObject _owner,Action callback)
    {
        specialCard.SetActive(true);
        GameObject card = Instantiate(specialCard.gameObject, specialCardPosLeft.transform.position, Quaternion.identity);
        card.SetActive(true);
        clearObjectName(card);
        card.GetComponent<CardMovements>().setRotatable(false);
        card.GetComponent<CardMovements>().activateDeactivateCardBack(false);
        card.GetComponent<CardMovements>().activateDeactivateCardFront(true);
        card.transform.position = specialCardPosLeft.transform.position;
        card.transform.SetParent(demonstrationPanel.transform.GetChild(0).transform.GetChild(0).transform);
        card.transform.GetChild(1).gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, 1);
        card.GetComponent<RectTransform>().sizeDelta = specialCardPosLeft.GetComponent<RectTransform>().sizeDelta;
        card.transform.rotation = new Quaternion(0, 0, 0, 0);
        card.transform.localScale = Vector3.one;
        cardAnims.cardAnimations.cardkMovePosAnim(card.transform, 1f, true, cardSounds.SInstance.cardMoveToDemonstrationFX);
        cardAnims.cardAnimations.changeCardSizeAnim(card.GetComponent<RectTransform>(), demonstrationPanel.transform.GetChild(0).transform.GetChild(0).GetComponent<RectTransform>().sizeDelta, 1f);
        yield return new WaitForSeconds(1);
        card.transform.SetParent(specialCardPosRight.transform);
        cardAnims.cardAnimations.cardkMovePosAnim(card.transform, 1f, true, cardSounds.SInstance.cardMoveToDemonstrationFX);
        cardAnims.cardAnimations.changeCardSizeAnim(card.GetComponent<RectTransform>(), specialCardPosRight.GetComponent<RectTransform>().sizeDelta, 1f);
        card.SetActive(false);
        yield return new WaitForSeconds(1f);
        if (callback != null)
            callback();
        Destroy(card, 4f);
    }
    #endregion
    ///// <summary>
    ///// Calls setCardToScaleUp  function with the given parameter then activates scaleUpPanel
    ///// </summary>
    ///// <param name="_card"></param>
    //public void openScaleUpPanel(GameObject _card)
    //{
    //    if ((!scaleUpPanel.activeInHierarchy && _card.GetComponent<CardMovements>().cardFrontIsActive) && pickingCardAvailable)
    //    {
    //        Debug.Log("Opening Panel");
    //        setcardToScaleUp(_card);
    //        scaleUpPanel.SetActive(true);
    //    }
    //}
    /// <summary>
    /// Add cards to demonstration panel and activates panel
    /// </summary>
    /// <param name="_card"></param>
    public void openDemonstrationPanelwithCard(GameObject _card, bool isSecondCard, bool isMatched)
    {
        
        demonstrationPanel.SetActive(true);
        GameObject card = Instantiate(_card.gameObject,_card.transform.position,Quaternion.identity);
        clearObjectName(card);
        card.GetComponent<CardMovements>().setRotatable(false);
        card.GetComponent<CardMovements>().activateDeactivateCardBack(false);
        card.GetComponent<CardMovements>().activateDeactivateCardFront(true);
        card.transform.position = _card.transform.position;
        card.transform.SetParent(demonstrationPanel.transform.GetChild(0).transform.GetChild(0).transform);
        card.transform.GetChild(1).gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, 1);
        card.transform.rotation = new Quaternion(0, 0, 0, 0);
        card.transform.localScale = Vector3.one;
        cardAnims.cardAnimations.cardkMovePosAnim(card.transform, 0.75f, true, cardSounds.SInstance.cardMoveToDemonstrationFX);
        cardAnims.cardAnimations.changeCardSizeAnim(card.GetComponent<RectTransform>(), demonstrationPanel.transform.GetChild(0).transform.GetChild(0).GetComponent<RectTransform>().sizeDelta, 0.75f);
       
        cardToAddHand = card;
        if (isSecondCard)
        {
            if (isMatched)
            {
                StartCoroutine(cardsMatchedRoutine(card));
            }
            else if (!isMatched)
            {
                StartCoroutine(cardsNotMatchCouroutine());
                //Invoke("closeDemonstrartionPanelRemoveCard", 1);
            }
        }
        else
        {
            Invoke("closeDemonstrartionPanelRemoveCard", 2);
        }
    }
    IEnumerator cardsNotMatchCouroutine()
    {
        
        //_cardAnimations.fadeAndDestroy(demonstrationPanel.transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).gameObject.GetComponent<UnityEngine.UI.Image>(), 1f);


        yield return new WaitForSeconds(1.5f);
        SoundManager.SInstance.playSfx(cardSounds.SInstance.cardsNotMatchedSFX);
        closeDemonstrartionPanelRemoveCard();
        clearCards();
        //yield return new WaitForSeconds(2);
        //closeDemonstrartionPanelRemoveCard();
        bool CallFunction = true;
        if (getCurrentTurn() != null)
        {
            List<GameObject> specList = getCurrentTurn().GetComponent<Player_Card>().getSpacialList();
            for (int i = 0; i < specList.Count; i++)
            {
                if (specList[i] != null)
                {
                    if (specList[i].GetComponent<CardInfos>().card.Location.Equals(Card.callLocation.AfterNonMatch))
                    {
                        if (specList[i].GetComponent<CardInfos>().card.FunctionType.Equals(Card.FunctionCallType.Replace))
                        {
                            CallFunction = false;
                        }
                        else
                        {
                            CallFunction = true;
                        }
                        Debug.Log("Should Call");
                        showAndUseSpecialCard(specList[i], getCurrentTurn(), true, getCurrentTurn());
                    }
                }
            }
            //    if (currentTurn.GetComponent<Player>().getSpacialList().Count > 0)
            //{

            //    foreach (var item in currentTurn.GetComponent<Player>().getSpacialList())
            //    {
            //        if (item.GetComponent<CardInfos>().card.Location.Equals(Card.callLocation.AfterNonMatch))
            //        {
            //            if (item.GetComponent<CardInfos>().card.FunctionType.Equals(Card.FunctionCallType.Replace))
            //            {
            //                CallFunction = false;
            //            }
            //            else
            //            {
            //                CallFunction = true;
            //            }
            //            Debug.Log("Should Call");
            //            showAndUseSpecialCard(item, getCurrentTurn(),true, item.GetComponent<CardInfos>().owner);
            //        }
            //    }
            //}
        }
        //closeDemonstrartionPanelRemoveCard();
        if (CallFunction)
        {
            firstCardSelected = true;
            firtsCardsMatched = false;
            changeTurn();
        }
        
    }
    IEnumerator cardsMatchedRoutine(GameObject _card)
    {
        yield return new WaitForSeconds(2f);

        if (!_card.GetComponent<CardInfos>().getCardType().Equals(Card.Type.Trap))
        {
            cardAnims.cardAnimations.setTRjfx(_card.GetComponent<CardInfos>().card.trj);
            cardAnims.cardAnimations.setfx(_card.GetComponent<CardInfos>().card.fx);
            cardAnims.cardAnimations.createTrj(getCurrentTurn().GetComponent<Player_Card>().currentCardPanel.transform.GetChild(0).transform.position);
        }
        if(!_card.GetComponent<CardInfos>().getCardType().Equals(Card.Type.Special) && !_card.GetComponent<CardInfos>().getCardType().Equals(Card.Type.Trap))
        {

            if (_card.GetComponent<CardInfos>().getCardType().Equals(Card.Type.Attack))
            {
                cardAnims.cardAnimations.setTarget(getEnemy().GetComponent<Player_Card>()._avatar.transform);
            }
            else if (_card.GetComponent<CardInfos>().getCardType().Equals(Card.Type.Deffense))
            {
                cardAnims.cardAnimations.setTarget(getOwner().GetComponent<Player_Card>()._avatar.transform);
            }
        }
        else if (_card.GetComponent<CardInfos>().getCardType().Equals(Card.Type.Special))
        {

            if (_card.GetComponent<CardInfos>().getSpecialType().Equals(Card.SpacialType.Buff))
            {
                cardAnims.cardAnimations.setTarget(getOwner().GetComponent<Player_Card>()._avatar.transform);
            }
            if (_card.GetComponent<CardInfos>().getSpecialType().Equals(Card.SpacialType.Debuff))
            {
                cardAnims.cardAnimations.setTarget(getEnemy().GetComponent<Player_Card>()._avatar.transform);
            }
        }
        closeDemonstrartionPanelRemoveCard();

        //cardToAddHand.gameObject.GetComponent<CardInfos>().activateLightnig(Vector3.zero, currentTurn.GetComponent<Player_Card>().currentCardPanel.transform.GetChild(0).gameObject);
        cardToAddHand.transform.SetParent(currentTurn.GetComponent<Player_Card>().currentCardPanel.transform.GetChild(0).transform/*demonstrationPanel.transform.GetChild(0).transform*/);
        cardAnims.cardAnimations.changeCardSizeAnim(cardToAddHand.GetComponent<RectTransform>(),Vector2.zero /*currentTurn.GetComponent<Player_Card>().currentCardPanel.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta*/, 0.3f);
        cardAnims.cardAnimations.fadeCardAnim(cardToAddHand.transform.GetChild(1).GetComponent<UnityEngine.UI.Image>(), 0f, 0.5f); //fadeAndDestroy(cardToAddHand.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>(), 1f);
        cardAnims.cardAnimations.cardkMovePosAnimElastic(cardToAddHand.transform, 0.5f,false, cardSounds.SInstance.cardsMatchedSFX);
 

        cardToAddHand.transform.SetAsFirstSibling();
        int choosenCardsCount = choosenCards.Count - 1;
        yield return new WaitForSeconds(0.5f);
        if (/*!_card.GetComponent<CardInfos>().getCardType().Equals(Card.Type.Special) &&*/ !_card.GetComponent<CardInfos>().getCardType().Equals(Card.Type.Trap))
        {
            cardAnims.cardAnimations.chargeTrj();
        }
   
        for (int i = choosenCardsCount; i >= 0; i--)
        {

            choosenCards.ElementAt(i).transform.SetParent(currentTurn.GetComponent<Player_Card>().currentCardPanel.transform.GetChild(0).transform/*demonstrationPanel.transform.GetChild(0).transform*/);
            cardAnims.cardAnimations.changeCardSizeAnim(choosenCards.ElementAt(i).transform.GetChild(1).GetComponent<RectTransform>(),Vector2.zero /*currentTurn.GetComponent<Player_Card>().currentCardPanel.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta*/, 0.3f);
            cardAnims.cardAnimations.cardkMovePosAnimElastic(choosenCards.ElementAt(i).transform, 0.5f,false, cardSounds.SInstance.cardsMatchedSFX);
            cardAnims.cardAnimations.fadeCardAnim(choosenCards.ElementAt(i).transform.GetChild(1).GetComponent<UnityEngine.UI.Image>(), 0f, 0.5f);//fadeAndDestroy(item.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>(), 1f);
            choosenCards.ElementAt(i).transform.SetAsFirstSibling();
            //choosenCards.ElementAt(i).gameObject.GetComponent<CardInfos>().activateLightnig(Vector3.zero, currentTurn.GetComponent<Player_Card>().currentCardPanel.transform.GetChild(0).gameObject);
            //soundManager.SInstance.playSfx(soundManager.SInstance.matchedSFX);

            yield return new WaitForSeconds(0.5f);
            if (/*!_card.GetComponent<CardInfos>().getCardType().Equals(Card.Type.Special) &&*/ !_card.GetComponent<CardInfos>().getCardType().Equals(Card.Type.Trap))
            {
                cardAnims.cardAnimations.chargeTrj();
            }
        }

        //yield return new WaitForSeconds(0.5f);

        firstCardSelected = true;
        firtsCardsMatched = true;
        cardsMatched(_card);


    }
    /// <summary>
    /// Removes card from demonstration Panel then closes panel
    /// </summary>
    public void closeDemonstrartionPanelRemoveCard()
    {
        
        if (choosenCards.Count == 1) 
        {
            currentTurn.GetComponent<Player_Card>().addCardToCardPanel(cardToAddHand, 0);
            List<GameObject> specList = new List<GameObject>();
            specList = getCurrentTurn().GetComponent<Player_Card>().getSpacialList();
            if (specList.Count > 0)
            {
                for (int i = 0; i < specList.Count; i++)
                {
                    if (specList[i] != null)
                    {
                        if (specList[i].GetComponent<CardInfos>().card.Location.Equals(Card.callLocation.afterFirstCard))
                        {
                            showAndUseSpecialCard(specList[i], getCurrentTurn(), true, getCurrentTurn());
                        }
                    }
                }
            }
            //}

            if (currentTurn.gameObject.name == "Enemy")
            {
                currentTurn.GetComponent<enemyAI>().startPickingCards(2);
            }
            cleatDemonstrationPanel();
            demonstrationPanel.SetActive(false);
            pauseResumeTimer(false);
            setPickingCardAvailable(true);
        }
        else if(choosenCards.Count >= 2)
        {
            StartCoroutine(delayedCloseDemonPanel());
        }

        //if (choosenCards.Count < 2)
        //{
        //    //if (choosenCards.Count == 1)
        //    //{

        //    //}
        //    demonstrationPanel.SetActive(false);
        //    pauseResumeTimer(false);
        //    setPickingCardAvailable(true);
        //    if (currentTurn.gameObject.name == "Enemy")
        //    {
        //        //Debug.Log("Picking Second Card0");
        //        currentTurn.GetComponent<enemyAI>().startPickingCards(2);
        //    }
        //}
        //else
        //{
        //    StartCoroutine(delayedCloseDemonPanel());
        //}

    }
    void cleatDemonstrationPanel()
    {
        foreach (Transform item in demonstrationPanel.transform.GetChild(0).transform.GetChild(0).transform)
        {
            if (choosenCards.Count > 1)
            {
                if (!isCardsMatched)
                {
                    item.gameObject.GetComponent<Animator>().Play("wrongAnimation");
                    cardAnims.cardAnimations.fadeAndDestroy(item.transform.GetChild(1).gameObject.GetComponent<UnityEngine.UI.Image>(), 1f);

                }

            }
            else
            {
                Destroy(item.gameObject);
            }

        }
    }
    IEnumerator delayedCloseDemonPanel()
    {
        cleatDemonstrationPanel();
        yield return new WaitForSeconds(1);
        demonstrationPanel.SetActive(false);
        pauseResumeTimer(false);
        setPickingCardAvailable(true);


    }
    ///// <summary>
    ///// Closes scaleUpPanel and Removes scaleUpPanles card object 
    ///// </summary>
    //public void closeScaleUpPanel()
    //{
    //    if (scaleUpPanel.activeInHierarchy)
    //    {
    //        //Debug.Log("Closing Panel");
    //        scaleUpPanel.SetActive(false);
    //        removeCardToScaleUp();
    //    }

    //} 
    ///// <summary>
    ///// Initiliaze the given parameter to the cardToScaleUp variable
    ///// </summary>
    ///// <param name="_card"></param>
    //public void setcardToScaleUp(GameObject _card)
    //{
    //    Debug.Log("card set!");
    //    cardToScaleUp = GameObject.Instantiate(_card);
    //    clearObjectName(cardToScaleUp);
    //    cardToScaleUp.transform.position = scaleUpCardSlot.transform.position;
    //    cardToScaleUp.transform.SetParent(scaleUpCardSlot.transform);
    //    cardToScaleUp.transform.localScale = Vector3.one;
    //    //cardToScaleUp.GetComponent<CardMovements>().activateDeactivateCardDescText(false);
    //    cardToScaleUp.GetComponent<CardMovements>().setRotatable(false);
    //    cardToScaleUp.GetComponent<RectTransform>().sizeDelta = scaleUpCardSlot.GetComponent<RectTransform>().sizeDelta;
    //}
    ///// <summary>
    ///// Destroys cardToScaleYp varşables' game object then set carToScaleUp to null
    ///// </summary>
    //public void removeCardToScaleUp()
    //{
    //    if (cardToScaleUp != null)
    //    {
    //        Destroy(cardToScaleUp.gameObject);
    //        cardToScaleUp = null;
    //    }

    //}
    /// <summary>
    /// When a card picked called and add them into choosenCard list if 2 card picked checks them
    /// </summary>
    /// <param name="card"></param>
    public void setChoosenCard(GameObject card)
    {
        pauseResumeTimer(true);
        //Debug.Log("Called Set Choosen Card"+card.name);
        if (choosenCards.Count < 2 && choosenCards.Count >= 0)
        {
            setPickingCardAvailable(false);
            //if (choosenCards.Count == 0)
            //{
            //    card.GetComponent<CardMovements>().activateDeactivateCardDescText(true);
            //}
            //else
            //{
            //    card.GetComponent<CardMovements>().activateDeactivateCardDescText(false);
            //}
            //Debug.Log("Added");
            choosenCards.Add(card);
            card.GetComponent<CardInfos>().setRevealed(true);
            setPickingCardAvailable(false);
            if (choosenCards.Count == 1)
            {
                //setPickingCardAvailable(false);
                //openDemonstrationPanelwithCard(card,false, false);
                StartCoroutine(delayedDemonstrationCall(card, false, false));
            }
            else if (choosenCards.Count == 2)
            {
               
                stopTimer();
                //setPickingCardAvailable(false);
                if (!timeIsUp)
                {
                    //currentTurn.GetComponent<Player>().addCardToCardPanel(card, 1);
                    Invoke("checkCards", 0f);
                }
                else
                {
                    clearCards();
                }
                
                //checkCards();
            }
        }
        else 
        {
            //Debug.Log("call clear");
            clearCards();
        }
    }
    IEnumerator delayedDemonstrationCall(GameObject _card,bool isSecondCard, bool isMatched)
    {
        cardAnims.cardAnimations.fadeCardAnim(_card.transform.GetChild(1).GetComponent<UnityEngine.UI.Image>(), 0.6f,1f);
        yield return new WaitForSeconds(0.7f);
        openDemonstrationPanelwithCard(_card, isSecondCard, isMatched);

    }
    /// <summary>
    /// Checks 2 card from the cardlist if they don't match call clearCard method if they match call cardsMatched  method
    /// </summary>
    void checkCards()
    {
        var firstCard = choosenCards[0];
        var secondCard = choosenCards[1];
        cardAnims.cardAnimations.fadeCardAnim(secondCard.transform.GetChild(1).GetComponent<UnityEngine.UI.Image>(), 0.6f, 1f);
        //Debug.Log(firstCard.name);
        //Debug.Log(secondCard.name);
        if (firstCard.name == secondCard.name)
        {
            //openDemonstrationPanelwithCard(secondCard, true, true);
            setIsCardMatched(true);
            //isCardsMatched = true;
            StartCoroutine(delayedDemonstrationCall(secondCard, true, true));
            //Debug.Log("Congrats");
            //foreach(var item in choosenCards)
            //{
            //    Debug.Log("choosen card: "+item.name);
            //}
            //Debug.Log(firstCard.name);

        }
        else
        {
            //openDemonstrationPanelwithCard(secondCard, true, false);
            setIsCardMatched(false);
            //isCardsMatched = false;
            StartCoroutine(delayedDemonstrationCall(secondCard, true, false));
            //Debug.Log("unfortunate");

        }
    }
    /// <summary>
    /// When the cards matched arranges matched cards paramaters and  UseCard panel then activates use card panel
    /// </summary>
    public void cardsMatched(GameObject _card)
    {
        //Debug.Log(_card.name);
        
        GameObject matchedCard =_card;
        //Debug.Log(matchedCard.name);
        clearObjectName(matchedCard);
        setCurrentCard(matchedCard.GetComponent<CardInfos>());
        matchedCard.GetComponent<CardInfos>().owner = getOwner();
        matchedCard.GetComponent<CardInfos>().enemy = getEnemy();
        removeCardToDealedDeck(matchedCard.name);

        useMatchedCard(); 
    }
    /// <summary>
    /// Activates Deactivates battleLockPanel According to the given parameter
    /// </summary>
    /// <param name="_state"></param>
    public void lockUnlockBattlePanel(bool _state)
    {
        battleLockPanel.SetActive(_state);
    }
    /// <summary>
    /// According to the parameter activates deactivates cardMathPanel
    /// </summary>
    /// <param name="_state"></param>
    void useMatchedCard()
    {
        _useCardScript.useCardDelayed();
    }
    /// <summary>
    /// Opens up use card panel , called when a card clicked in player's  hand
    /// </summary>
    /// <param name="_card"></param>
    //public void useCardFromHand(GameObject _card)
    //{
    //    GameObject matchedCard = GameObject.Instantiate(_card);
    //    //clearObjectName(matchedCard);
    //    currentCard = _card;
    //    matchedCard.transform.position = cardMatchCardSlot.transform.position;
    //    matchedCard.transform.SetParent(cardMatchCardSlot.transform);
    //    matchedCard.transform.localScale = Vector3.one;
    //    matchedCard.GetComponent<CardInfos>().owner = getOwner();
    //    matchedCard.GetComponent<CardInfos>().enemy = getEnemy();
    //    //matchedCard.GetComponent<CardMovements>().activateDeactivateCardDescText(false);
    //    matchedCard.GetComponent<CardMovements>().setRotatable(false);
    //    matchedCard.GetComponent<RectTransform>().sizeDelta = cardMatchCardSlot.GetComponent<RectTransform>().sizeDelta;
    //    _useCardPanel.openAsUseCardPanel();
    //}
    public GameObject getFirstCard()
    {
        if(choosenCards[0] != null)
            return choosenCards[0].gameObject;
        else
            return null;
    }
    /// <summary>
    /// Returns currentCard variable
    /// </summary>
    /// <returns></returns>
    public CardInfos getCurrentCard()
    {
        //if (currentCard != null)
        //{
        //    Debug.Log(currentCard.gameObject.name);
        //}
        //else
        //{
        //    Debug.Log("card is null");
        //}
        return currentCard;
    }
    /// <summary>
    /// Assign given paramater in to currentCard variable
    /// </summary>
    /// <param name="_card"></param>
    public void setCurrentCard(CardInfos _card)
    {
        //GameObject card = new GameObject();
        //card = Instantiate(_card);
        //card.transform.localScale = Vector3.zero;
        currentCard = _card;
    }
    /// <summary>
    /// Return currentTurnVariable
    /// </summary>
    /// <returns></returns>
    public GameObject getCurrentTurn()
    {
        return currentTurn;
    }
    /// <summary>
    /// Assign given paramaeter into currentTurnVaqriable
    /// </summary>
    /// <param name="_player"></param>
    public void setCurrentTurn(GameObject _player)
    {
        currentTurn = _player;
    }
    /// <summary>
    /// Add given paramöeter to dealedDeck List if not already in the deck
    /// </summary>
    /// <param name="_card"></param>
    public void addCardToDealedDeck(GameObject _card)
    {
        if(!dealedDeck.Contains(_card))
            dealedDeck.Add(_card);
    }
    /// <summary>
    /// returns dealedDeck
    /// </summary>
    /// <returns></returns>
    public List<GameObject> getDealedDeck()
    {
        List<GameObject> listToRetun = new List<GameObject>();
        listToRetun.AddRange(dealedDeck);
        return listToRetun;
    }
    /// <summary>
    /// Remove given paramöeter to dealedDeck List if already in the deck
    /// </summary>
    /// <param name="_card"></param>
    public void removeCardToDealedDeck(String _cardName)
    {
        //Debug.Log(_card.gameObject.name);
        //var item = dealedDeck.Find(x => x.gameObject.name == _card.gameObject.name);
        //Debug.Log(item.name);
        //if (item!=null)
        //Debug.Log(_cardName);
        if (dealedDeck.Count > 0)
        {
            dealedDeck.RemoveAll(x => x.name == _cardName);
            //foreach (var item in dealedDeck)
            //{
            //    Debug.Log(item.name + "  " + _cardName);

            //    if (item.name == _cardName)
            //    {
            //        Debug.Log(item.name + "  " + _cardName);
            //        dealedDeck.Remove(item);
            //    }
            //}
        }

            //dealedDeck.RemoveAll(x=>x.gameObject.name == _card.gameObject.name);
    }
    /// <summary>
    /// Clears dealedDeck List
    /// </summary>
    public void clearDealedDeckList()
    {
        dealedDeck.Clear();
    }
    /// <summary>
    /// Returns dealedDeck list's count
    /// </summary>
    /// <returns></returns>
    public int getDealedDecksCount()
    {
        return dealedDeck.Count;
    }
    /// <summary>
    /// Assign given parameter in to isCardMatched variable
    /// </summary>
    /// <param name="state"></param>
    public void setIsCardMatched(bool state)
    {
        isCardsMatched = state;
    }
    /// <summary>
    /// return isCardMatched variable
    /// </summary>
    /// <returns></returns>
    public bool getIsCardMatched()
    {
        return isCardsMatched;
    }
    /// <summary>
    /// Calls DeckToPick's dealCards function
    /// </summary>
    public void dealCards()
    {
        _deckToPick.dealCards();
    }
    /// <summary>
    /// Removes given parameter from cardlist if exist
    /// </summary>
    /// <param name="card"></param>
    public void removeCard(GameObject card)
    {
        choosenCards.Remove(choosenCards.Find(x => x == card)); 
    }
    /// <summary>
    /// Activates time is up panel, calls clearCards then calls changeTurn
    /// </summary>
    public void timeUp()
    {
        timeIsUp = true;
        changeTurn();
    }
    /// <summary>
    /// Flip back card in the cardList  then remove them from list
    /// </summary>
    public void clearCards()
    {
        //Debug.Log("Clear Cards");
        if(choosenCards.Count>1 && isCardsMatched)
        {
            //if (choosenCards.Count > 0)
            //{
                foreach (var item in choosenCards)
                {
                if(item.transform.GetChild(0).transform.GetChild(0).GetComponent<UnityEngine.UI.Image>() != null)
                    cardAnims.cardAnimations.fadeAndDestroy(item.transform.GetChild(0).transform.GetChild(0).GetComponent<UnityEngine.UI.Image>(), 2f);
                else
                    Destroy(item.gameObject);
                }
                choosenCards.Clear();
            //}
            currentTurn.GetComponent<Player_Card>().clearCardPanelExceptFirstElement();
        }
        else
        {
            foreach (var _card in choosenCards)
            {
                //Debug.Log(_card.name + "closing..");
                _card.GetComponent<CardInfos>().activateDeactivateHighLight(false);
                _card.transform.GetChild(1).GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, 1);
                _card.GetComponent<CardMovements>().flip();
            }
            currentTurn.GetComponent<Player_Card>().clearCardPanel();
        }
        foreach(var item in dealedDeck)
        {
            item.GetComponent<CardInfos>().activateDeactivateHighLight(false);
            if (item.GetComponent<CardMovements>().cardFrontIsActive)
                item.GetComponent<CardMovements>().flipRoutine();
        }   
        //Debug.Log("trying to clear");
        setIsCardMatched(false);
        choosenCards.Clear();
        StartCoroutine(delayedCall(true,setPickingCardAvailable));
        //changeTurn();
    }
    public int getChoosenCardsCount()
    {
        return choosenCards.Count;
    }
    /// <summary>
    /// Remeoves (clone string fron given object's name)
    /// </summary>
    /// <param name="_obj"></param>
    public void clearObjectName(GameObject _obj)
    {
        //var stackTrace = new System.Diagnostics.StackTrace();
        //var caller = stackTrace.GetFrame(1).GetMethod().Name;
        //UnityEngine.Debug.Log("clear names " + "called by " + caller);
        _obj.name = _obj.name.Replace("(Clone)", "");
        if (_obj.name.Contains("Clone)"))
            clearObjectName(_obj);
    }
    IEnumerator changeTurndelayedCall(bool state, Action<bool> callback)
    {
        yield return new WaitForSeconds(2);
        callback(state);
    }
    IEnumerator delayedCall(bool state,Action<bool> callback)
    {
        yield return new WaitForSeconds(2);
        callback(state);
    }
    IEnumerator warningPanelRoutine(string _text/*, GameObject pastTurn=null, GameObject currentTurn=null*/, Action<GameObject,GameObject> callback=null)
    {
        yield return new WaitForSeconds(1.5f);
        warningText.text = "";
        warningText.text = _text;
        warningPanel.SetActive(true);
        yield return new WaitForSeconds(1);
        warningPanel.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        if (callback != null)
            callback(pastTurn,currentTurn);
    }
    public void gameOver()
    {
        Invoke("setScoreBoard", 2);
        //StartCoroutine(gameOverRoutine());

    }
    public void setScoreBoard()
    {
        int earnedGold =0;
        int earnedXp = 0;
        int starCount = 0;
        Time.timeScale = 0;
        if (playerList[0].GetComponent<Player_Card>().getCurrentHealth() > playerList[1].GetComponent<Player_Card>().getCurrentHealth())
        {
            earnedGold = UnityEngine.Random.Range(65, 75);
            earnedXp = UnityEngine.Random.Range(6, 8);
            //Debug.Log("You Won!");
            starCount = 1;
            //You Won!
            //_gameOverScript.setText(playerList[0].gameObject.name);
        }
        else if (playerList[0].GetComponent<Player_Card>().getCurrentHealth() < playerList[1].GetComponent<Player_Card>().getCurrentHealth())
        {
            earnedGold = UnityEngine.Random.Range(55, 60);
            earnedXp = UnityEngine.Random.Range(1, 3);
            starCount = 0;
            //Debug.Log("Enemy Won!");
            //Enemy Won!
            //_gameOverScript.setText(playerList[1].gameObject.name);
        }
        else if (playerList[0].GetComponent<Player_Card>().getCurrentHealth() == playerList[1].GetComponent<Player_Card>().getCurrentHealth())
        {
            earnedGold = UnityEngine.Random.Range(40, 50);
            earnedXp = UnityEngine.Random.Range(2, 5);
            starCount= - 1;
            //Debug.Log("Tie!");
            //Draw!
            //_gameOverScript.setText("Nobody is ");
        }
        //SaveLoad_Manager.gameData.playerInfos.playerGoldCount += earnedGold;
        scoreBoard.SetActive(true);
        scoreBoard.GetComponent<ScoreBoard>().gameOver(starCount, earnedGold, earnedXp);
    }
    //IEnumerator gameOverRoutine()
    //{
    //    yield return new WaitForSeconds(2f);
       
    //    //_gameOverScript.openCloseGameOverPanel(true);
        
    //}
    public void keepGoing()
    {
        if (getCurrentTurn() != null)
        {
            getCurrentTurn().GetComponent<Player_Card>().handleUsedSpacial();
        }

        if (_gameTimer.getTimeIsUp())
        {
            gameOver();
        }
        else
        {
            StartCoroutine(changeTurnDelayed());
        }
    }

}
