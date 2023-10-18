using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class gameInfos
{
    public static decks choosenDeck;
    public static difficulty choosenDifficulty;
    public enum difficulty { EASY, NORMAL, HARD }
    [System.Serializable]
    public struct gameDecks
    {
        public difficulty _difficulty;
        public List<Card> deckList;
        public GameObject PosList;
    }
    [System.Serializable]
    public struct decks
    {
        public difficulty gameDifficulty;
        public List<Card> gameDeck;
        public int deckSize;
        public int trapCardCount;
    }
    [System.Serializable]
    public struct pairedCads
    {
        public Card.Type _cardTyp;
        public GameObject firstCard;
        public GameObject secondCard;
    }
}
