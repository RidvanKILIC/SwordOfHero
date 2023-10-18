using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class useCardScript : MonoBehaviour
{
    #region Vaeiables
    [SerializeField]GameManager _gameManager;
    [SerializeField]DeckToPick _deckToPick;
    [SerializeField] gameTimer _gameTimer;
    #endregion
    private void Start()
    {
        Init();
    }
    /// <summary>
    /// Initializes variables & references
    /// </summary>
    void Init()
    {
        //_gameManager = GameObject.FindObjectOfType<GameManager>();
        //_panel = this.gameObject;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// Activates deactivates this gameObject & arranges close & keep buttons
    /// </summary>
    public void useCardDelayed()
    {
        Invoke("useCard", 0.1f);

    }
    /// <summary>
    /// Deactivates this gameObject if it is active
    /// </summary>
    public void adjustNewTrun()
    {
        if (_gameManager.getDealedDecksCount() <= 0)
        {
            if (_gameTimer.getTimeIsUp())
            {
                _gameManager.gameOver();
            }
            else
            {
                _deckToPick.getNewDeck();
            }
            //Debug.Log("Dealed Deck Count: " + _gameManager.getDealedDecksCount());
            //_gameManager.dealCards();
        }
        else
        {
            _gameManager.changeTurn();
        }
           
    }
    /// <summary>
    /// Calls Card's specials
    /// </summary>
    public void useCard()
    {
        //Debug.Log("Use Card Called");
        _gameManager.getCurrentCard().UseCard();
        //Invoke("delayedUseCard", 2f);
    }
     public void delayedUseCard()
     {
        _gameManager.getCurrentTurn().GetComponent<Player_Card>().clearCardPanel();
        _gameManager.clearCards();
        adjustNewTrun();
     }

}
