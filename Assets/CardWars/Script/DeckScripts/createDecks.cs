using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class createDecks : MonoBehaviour
{
    #region Variables
    [SerializeField] float rareCardChancePerc;
    [SerializeField] float epicCardChancePerc;
    [SerializeField] List<Card> SOH_Cards = new List<Card>();
    [SerializeField] List<Card> playableCards;
    [SerializeField] List<Card> trapCards;
    [SerializeField] List<Card> regularCardsList = new List<Card>();
    [SerializeField] List<Card> rareCardsList = new List<Card>();
    [SerializeField] List<Card> legendaryCardsList = new List<Card>();
    [SerializeField] List<Card> trapCardsList = new List<Card>();
    [SerializeField] List<Card> spacialCardsList = new List<Card>();
    [SerializeField] List<Card> choosenDeck = new List<Card>();
    [SerializeField] List<gameInfos.decks> gameModes = new List<gameInfos.decks>();
    [SerializeField] gameInfos.difficulty selectedDifficulty;
    public deckDens deckDensity;
    public enum deckDens {normal,spacial}
    //mainMenuController _menuController;
    GameManager _gameManager;
    bool deckCreated = false;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        //_menuController = GameObject.FindObjectOfType<mainMenuController>();
        _gameManager = GameObject.FindObjectOfType<GameManager>();
        //loadCards();
        createGameModes();
        setSelectedDifficulty(gameInfos.difficulty.EASY);
        //gameInfos.decks choosenDeck = getGameModesList().Find(x => x.gameDifficulty.Equals(getSelectedDifficulty()));
        //createRandonDeck(choosenDeck);
        createNewDeck();
        _gameManager.Init();
        // UnityEngine.Random.InitState(Convert.ToInt32(DateTime.Now.Ticks));
    }
    public void createNewDeck()
    {
        gameInfos.decks choosenDeck = getGameModesList().Find(x => x.gameDifficulty.Equals(getSelectedDifficulty()));
        createRandonDeck(choosenDeck);
    }
    void loadCards()
    {
        //Debug.Log("loading Cards");
        //playableCards = Resources.LoadAll<Card>("CardsObjects/NormalCards/");
        //trapCards = Resources.LoadAll<Card>("CardsObjects/TrapCards/");
        fillPlayableCardsLists();
    }
    void fillPlayableCardsLists()
    {
        //Debug.Log("Filling Card's List");
        for (int i = 0; i < playableCards.Count; i++)
        {
            if (playableCards[i].type.Equals(Card.Type.Special))
            {
                spacialCardsList.Add(playableCards[i]);
            }
            if (playableCards[i].rarity.Equals(Card.Rarity.Common))
            {
                regularCardsList.Add(playableCards[i]);
            }
            else if (playableCards[i].rarity.Equals(Card.Rarity.Rare))
            {
                rareCardsList.Add(playableCards[i]);
            }
            else if (playableCards[i].rarity.Equals(Card.Rarity.Legendary))
            {
                legendaryCardsList.Add(playableCards[i]);
            }
        }
        
        for(int i = 0; i < trapCards.Count; i++)
        {
            trapCardsList.Add(trapCards[i]);
        }

        regularCardsList= regularCardsList.OrderBy(x => System.Guid.NewGuid()).ToList();
        spacialCardsList = spacialCardsList.OrderBy(x => System.Guid.NewGuid()).ToList();
        rareCardsList = rareCardsList.OrderBy(x => System.Guid.NewGuid()).ToList();
        legendaryCardsList = legendaryCardsList.OrderBy(x => System.Guid.NewGuid()).ToList();
        trapCardsList = trapCardsList.OrderBy(x => System.Guid.NewGuid()).ToList();
    }
    public void createRandonDeck(gameInfos.decks _decks)
    {

        choosenDeck.Clear();
        var mixedCards = SOH_Cards.OrderBy(x => System.Guid.NewGuid()).ToList();
        int size = choosenDeck.Count;
        int count = 0;
        for (int i = size; i < _decks.deckSize; i++)
        {
            choosenDeck.Add(mixedCards[count]);
            count++;
        }
        //int nonTrapCardCount = _decks.deckSize;
        //int trapCardCount = _decks.trapCardCount;

        ////_menuController.activateDeactivateLoadingPanel(true);
        ////_menuController.stopStartIconAnim(true);
        //if (deckDensity.Equals(deckDens.normal))
        //{
        //    tryToAddLegendCard();
        //    tryToAddRareCards();
        //    fillListWithCommonCard(nonTrapCardCount - 4);
        //    int cout = 0;
        //    //Debug.Log("CurrentDeckCount: " + choosenDeck.Count + " Estimated Count: " + _decks.deckSize);
        //    if (choosenDeck.Count < _decks.deckSize)
        //    {
        //        List<Card> mixedCards = new List<Card>();
        //        foreach (var item in rareCardsList)
        //        {
        //            if (!choosenDeck.Contains(item))
        //                mixedCards.Add(item);
        //        }
        //        foreach (var item in legendaryCardsList)
        //        {
        //            if (!choosenDeck.Contains(item))
        //                mixedCards.Add(item);
        //        }
        //        mixedCards = mixedCards.OrderBy(x => System.Guid.NewGuid()).ToList();
        //        int size = choosenDeck.Count;
        //        int count = 0;
        //        for (int i = size; i < _decks.deckSize; i++)
        //        {
        //            choosenDeck.Add(mixedCards[count]);
        //            count++;
        //        }
        //        //Debug.Log(choosenDeck.Count);
        //    }

        //}
        //if (deckDensity.Equals(deckDens.spacial))
        //{
        //    for (int i = 0; i < _decks.deckSize; i++)
        //    {
        //        if(spacialCardsList.Count < i)
        //        {
        //            choosenDeck.Add(spacialCardsList[i]);
        //        }

        //    }
        //    if (choosenDeck.Count < _decks.deckSize)
        //    {
        //        fillListWithCommonCard((_decks.deckSize - choosenDeck.Count));
        //    }
        //}
        //addTrapCards(_decks);

        //if(gameInfos.choosenDeck.gameDeck != null)
        gameInfos.choosenDeck = new gameInfos.decks();
        gameInfos.decks newDeck = new gameInfos.decks();
        newDeck.deckSize = _decks.deckSize;
        newDeck.trapCardCount = _decks.trapCardCount;
        newDeck.gameDifficulty = _decks.gameDifficulty;
        newDeck.gameDeck = choosenDeck;
        gameInfos.choosenDeck = newDeck;

        deckCreated = true;
        //StartCoroutine(LoadAsyncScene());
        //_sceneManagment.sceneManager.loadStartScene();
    }

    //IEnumerator LoadAsyncScene()
    //{
    //    yield return new WaitUntil(()=>deckCreated);
    //    AsyncOperation asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("1");
    //    //asyncLoad.allowSceneActivation = false;
    //    // Eþzamansýz sahne tamamen yüklenene kadar bekleyin

    //        if (asyncLoad.progress >= 0.9f)
    //        {
    //        _menuController.stopStartIconAnim(false);
    //        //asyncLoad.allowSceneActivation = true;
    //        }      

    //}
    //void fillListWithNonCommonCard(gameInfos.decks _decks)
    //{

    //    tryToAddLegendCard();
    //    tryToAddRareCards();
    //    if (choosenDeck.Count < _decks.deckSize)
    //    {
    //        recursionCount++;
    //        fillListWithNonCommonCard(_decks);
    //    }
    //    if (recursionCount > 200)
    //    {
    //        Debug.LogError("infinite recursion");
    //    }
    //    Debug.Log("Filling With non connon");
    //}
    void addTrapCards(gameInfos.decks _decks)
    {
        for(int i = 0; i < _decks.trapCardCount; i++)
        {
          
            choosenDeck.Add(getRandomTrapCard());
        }
    }
    void tryToAddLegendCard()
    {
        if (getRandomValue() <= epicCardChancePerc)
        {
            //Add legendary
            choosenDeck.Add(getRamdomLegendaryCard());
        }
    }
    void tryToAddRareCards()
    {
        for (int i = 0; i < 2; i++)
        {
            if (getRandomValue() <= rareCardChancePerc)
            {

                //Add Rare
                var rareCard = getRamdomRareCard();
                while (!choosenDeck.Contains(rareCard))
                    rareCard = getRamdomRareCard();
                choosenDeck.Add(rareCard);
            }
        }
    }
    void fillListWithCommonCard(int listSize)
    {
        for(int i = 0; choosenDeck.Count < listSize; i++)
        {
            if (choosenDeck.Count < listSize)
            {
                choosenDeck.Add(regularCardsList.ElementAt(i));
                //Debug.Log(choosenDeck.Count);
            }

        }
    }
    double getRandomValue()
    {
        var random = new System.Random((int)DateTime.Now.Ticks);
        //Debug.Log(random.NextDouble());
        return random.NextDouble();
    }
    Card getRamdomLegendaryCard()
    {
        legendaryCardsList = legendaryCardsList.OrderBy(x => System.Guid.NewGuid()).ToList();
        return legendaryCardsList.ElementAt(0);
    }
    Card getRamdomRareCard()
    {
        rareCardsList = rareCardsList.OrderBy(x => System.Guid.NewGuid()).ToList();
        return rareCardsList.ElementAt(0);
    }
    Card getRandomTrapCard()
    {
        trapCardsList = trapCardsList.OrderBy(x => System.Guid.NewGuid()).ToList();
        return trapCardsList.ElementAt(0);
    }

    void createGameModes()
    {
        gameInfos.decks easyItem = new gameInfos.decks();
        easyItem.gameDifficulty = gameInfos.difficulty.EASY;
        easyItem.gameDeck = null;
        easyItem.deckSize = 8;
        easyItem.trapCardCount = 0;
        gameModes.Add(easyItem);
        gameInfos.decks normalItem = new gameInfos.decks();
        normalItem.gameDifficulty = gameInfos.difficulty.NORMAL;
        normalItem.gameDeck = null;
        normalItem.deckSize = 12;
        normalItem.trapCardCount = 1;
        gameModes.Add(normalItem);
        gameInfos.decks hardItem = new gameInfos.decks();
        hardItem.gameDifficulty = gameInfos.difficulty.HARD;
        hardItem.gameDeck = null;
        hardItem.deckSize = 16;
        hardItem.trapCardCount = 3;
        gameModes.Add(normalItem);
    }
    public List<gameInfos.decks> getGameModesList()
    {
        return gameModes;
    }
    public gameInfos.difficulty getSelectedDifficulty()
    {
        return selectedDifficulty;
    }

    void setSelectedDifficulty(gameInfos.difficulty _difficulty)
    {
        selectedDifficulty = _difficulty;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
