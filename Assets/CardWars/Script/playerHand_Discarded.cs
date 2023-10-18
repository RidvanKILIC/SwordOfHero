using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHand_Discarded : MonoBehaviour
{
    #region Variables
    [Header("Layouts & GameObjects")]
    [SerializeField] GameObject handLayout;
    [SerializeField] GameObject lockObject;
    List<GameObject> cardsInHand=new List<GameObject>();
    [Header("References")]
    GameManager _gameManager;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.FindObjectOfType<GameManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// Instatiate given parameter and add it to hand 
    /// </summary>
    /// <param name="_card"></param>
    public void addCardToHand(GameObject _card)
    {
        GameObject cardToAdd = Instantiate(_card);
        _gameManager.clearObjectName(cardToAdd);
        //cardToAdd.GetComponent<CardInfos>().setInHand(true);
        cardToAdd.transform.parent = handLayout.transform;
        cardToAdd.transform.localScale = Vector3.one;
        cardsInHand.Add(_card);
    }
    /// <summary>
    /// Removes given paramaeter from list and destroys its gmaeObject
    /// </summary>
    /// <param name="_cardToRemove"></param>
    public void removeCardFromHand(GameObject _cardToRemove)
    {
        GameObject _card = null;
        //foreach(var item in cardsInHand)
        //{
        //    Debug.Log(item.name);
        //    Debug.Log(_cardToRemove.name);
        //}
        _card = cardsInHand.Find(x => x.name.Equals(_cardToRemove.name));
        if (cardsInHand.Count>0 && _card != null)
        {
            cardsInHand.Remove(_cardToRemove);
            Destroy(_cardToRemove.gameObject);
        }

    }
    /// <summary>
    /// Returns count of cardsInHand List
    /// </summary>
    /// <returns></returns>
    public int numberOfCardsInHand()
    {
        return cardsInHand.Count;
    }
    /// <summary>
    /// Assigns given parameter to lockObjest variable
    /// </summary>
    /// <param name="_state"></param>
    public void lockUnlockPlayerHand(bool _state)
    {
        lockObject.SetActive(_state);
    }
}
