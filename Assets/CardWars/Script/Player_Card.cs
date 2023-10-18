using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cardAnims;
using System.Linq;

public class Player_Card : MonoBehaviour, CIDamagable
{
    #region Variables
    [Header("Variables")]
    [SerializeField] int health;
    [Header("Objects")]
    [SerializeField] public GameObject currentCardPanel;
    [SerializeField] public GameObject _avatar;
    [SerializeField] public GameObject hethBar;
    [SerializeField] healthBarScript _healthBarScript;
    [SerializeField] GameObject specialCardObj;
    [SerializeField] TMPro.TMP_Text specialCardCount;
    [SerializeField] float playerTurnTime;
    [SerializeField] GameObject timer;
    [SerializeField] GameObject highLight;
    [SerializeField] List<GameObject> spacialCardsList = new List<GameObject>();
    [SerializeField] List<GameObject> cardToDestroy = new List<GameObject>();
    [SerializeField] PlayerHUD_Controller playerBadge;
    public int Health { get; set; }
    //public float _Shield { get; set; }

    public GameManager _gameManager;
    [SerializeField]specialCardsContainer _specialContainer;
    #endregion
    private void Awake()
    {
        if (playerBadge != null)
            playerBadge.setSkin();
    }
    // Start is called before the first frame update
    void Start()
    {
        Health = health;
       // Debug.Log(playerBadge.name);
        
        //var skin = playerBadge.gameObject.GetComponent<PlayerHUD_Controller>();
        //if (skin != null)
        //{
        //    playerBadge.setSkin();
        //}
        //else
        //{
        //    Debug.Log("skin is null");
        //}
        //_specialContainer = GameObject.FindObjectOfType<specialCardsContainer>();
        _healthBarScript = hethBar.GetComponent<healthBarScript>();
        _gameManager = GameObject.FindObjectOfType<GameManager>();
        timer.GetComponent<timebarScript>().setMaxTime(playerTurnTime);
        _healthBarScript.setMaxHealth(Health);
        //_healthBarScript.setHealth(Health);
    }
    public float getPlayerTurnTime()
    {
        return playerTurnTime;
    }
    // Update is called once per frame
    void Update()
    {

    }
    public GameObject getTimer()
    {
        return timer;
    }
    public void addCardToCardPanel(GameObject _card,int slot)
    {
        GameObject card = Instantiate(_card, _card.transform.position, Quaternion.identity);
        _gameManager.clearObjectName(card);
        card.GetComponent<RectTransform>().sizeDelta = _card.GetComponent<RectTransform>().sizeDelta;
        card.GetComponent<CardMovements>().setRotatable(false);
        card.GetComponent<CardMovements>().activateDeactivateCardBack(false);
        card.GetComponent<CardMovements>().activateDeactivateCardFront(true);      
        card.transform.SetParent(currentCardPanel.transform.GetChild(slot).transform);
        card.transform.localScale = Vector3.one;
        bool control = _gameManager.getIsCardMatched();
        //Debug.Log(!control);
        cardAnims.cardAnimations.cardkMoveReturnPosAnim(card.transform,1f,!control);
       
        cardAnims.cardAnimations.changeCardSizeAnim(card.GetComponent<RectTransform>(), currentCardPanel.transform.GetChild(slot).GetComponent<RectTransform>().sizeDelta,1f);
        //card.GetComponent<RectTransform>().sizeDelta = currentCardPanel.transform.GetChild(slot).GetComponent<RectTransform>().sizeDelta;
    }
    public void clearCardPanel()
    {
        //Debug.Log("Clear All");
        int zerothChildCount = currentCardPanel.transform.GetChild(0).childCount-1;
        if (zerothChildCount >= 0)
        {
            for (int i = zerothChildCount; i >= 0; i--)
            {
                //Debug.Log(i);
                if(currentCardPanel.transform.GetChild(0).transform.GetChild(i).transform.GetChild(1).GetComponent<UnityEngine.UI.Image>()!=null)
                    cardAnims.cardAnimations.fadeAndDestroy(currentCardPanel.transform.GetChild(0).transform.GetChild(i).transform.GetChild(1).GetComponent<UnityEngine.UI.Image>(), 2f);
                else
                    Destroy(currentCardPanel.transform.GetChild(0).transform.GetChild(i).gameObject);
            }
        }

        int firsthChildCount = currentCardPanel.transform.GetChild(1).childCount-1;
        if (firsthChildCount >= 0)
        {
            for (int i = firsthChildCount; i >= 0; i--)
            {
                if (currentCardPanel.transform.GetChild(0).transform.GetChild(1).transform.GetChild(1).GetComponent<UnityEngine.UI.Image>() != null)
                    cardAnims.cardAnimations.fadeAndDestroy(currentCardPanel.transform.GetChild(1).transform.GetChild(i).transform.GetChild(1).GetComponent<UnityEngine.UI.Image>(), 2f);
                else
                    Destroy(currentCardPanel.transform.GetChild(1).transform.GetChild(i).gameObject);
            }
        }

    }
    public void demonstrateSpecial(GameObject _card,bool callFunction,GameObject target)
    {
        _specialContainer.demonstrateCard(_card, spacialCardsList.FindIndex(x => x.name == _card.name),callFunction,target);
    }
    public void clearCardPanelExceptFirstElement()
    {
        //Debug.Log("Last Element Should Remain");
        int zerothChildCount = currentCardPanel.transform.GetChild(0).childCount - 1;
        if (zerothChildCount >= 0)
        {
            for (int i = zerothChildCount; i > 0; i--)
            {
                //Debug.Log(i);
                cardAnims.cardAnimations.fadeAndDestroy(currentCardPanel.transform.GetChild(0).transform.GetChild(i).transform.GetChild(1).GetComponent<UnityEngine.UI.Image>(), 2f);
                //Destroy(currentCardPanel.transform.GetChild(0).transform.GetChild(i).gameObject);
            }
        }

        int firsthChildCount = currentCardPanel.transform.GetChild(1).childCount - 1;
        if (firsthChildCount >= 0)
        {
            for (int i = firsthChildCount; i > 0; i--)
            {
                cardAnims.cardAnimations.fadeAndDestroy(currentCardPanel.transform.GetChild(1).transform.GetChild(i).transform.GetChild(1).GetComponent<UnityEngine.UI.Image>(), 2f);
                //Destroy(currentCardPanel.transform.GetChild(1).transform.GetChild(i).gameObject);
            }
        }
    }
    /// <summary>
    /// Substract given parameter from Health variable
    /// </summary>
    /// <param name="_damageAmount"></param>
    public void Damage(int _damageAmount)
    {
        Health -= _damageAmount;
        Mathf.Clamp(Health, 0, 100);
        ///PersiusSpacial
        ///
        _healthBarScript.decraseHealth(_damageAmount);
        if (Health <= 0)
        {
            _gameManager.gameOver();
        }
        
    }
    public void IAttack()
    {

    }
    /// <summary>
    /// Add given parameter to Health Variable
    /// </summary>
    /// <param name="_healthAmount"></param>
    public void Heal(int _healthAmount)
    {
        Health += _healthAmount;
        Mathf.Clamp(Health, 0f, 100f);
        //Debug.Log("Healed" + _healthAmount);
        _healthBarScript.increaseHealth(_healthAmount);
        if (Health <= 0)
        {
            Health = 0;
            _gameManager.gameOver();
        }
    }
    public int getCurrentHealth()
    {
        return Health;
    }
    public void activateDeactivateTurn(bool _state)
    {
        highLight.SetActive(_state);
        //Debug.Log("Called with:"+_state);
        if (_state)
            cardAnims.cardAnimations.changeScaleAnim(_avatar.transform, new Vector3(1.1f, 1.1f, 1.1f),1f);
        else
            cardAnims.cardAnimations.changeScaleAnim(_avatar.transform, Vector3.one, 1f);
    }
    public void handleUsedSpacial()
    {
        if (_specialContainer.cardShoowing)
        {
            int size = _specialContainer.currentSpecials.Count - 1;
            if(size >= 0)
            {
                for (int i = size; i >= 0; i--)
                {

                    if (_specialContainer.currentSpecials[i].GetComponent<CardInfos>().card.isExpired())
                    {
                        removeSpacial(_specialContainer.currentSpecials[i]);
                    }
                    else
                    {
                        _specialContainer.retunCardToSlot(_specialContainer.currentSpecials[i], spacialCardsList.FindIndex(x => x.name == _specialContainer.currentSpecials[i].name));
                    }
                }
            }

        }

    }
    public void AddSpacial(GameObject _spacial)
    {
        if (spacialCardsList.Count < 5)
        {
            GameObject spacial = Instantiate(_spacial.gameObject/*, _gameManager.specialCardPosLeft.transform.position, Quaternion.identity*/);
            spacial.gameObject.GetComponent<CardInfos>().card.gameObj = spacial.gameObject;
            spacial.GetComponent<CardInfos>().card.Initialize();
            spacial.transform.GetChild(1).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 1f, 1f, 1f);
            _gameManager.clearObjectName(spacial);
            _spacial.transform.transform.localScale = Vector3.one;
            spacialCardsList.Add(spacial);
            if (spacialCardsList.Count == 1)
            {
                _specialContainer._animator.Play("specialCardsPanelOpen");
                _specialContainer.setIsPanelActive(true);

            }
            //_specialContainer.addCardToSlot(_spacial,spacialCardsList.FindIndex(x=>x.name == _spacial.name),isFirst);
            //Debug.Log(_specialContainer.posList.ElementAt(spacialCardsList.FindIndex(x => x.name == _spacial.name)).gameObject.name);
            //spacial.transform.position = _specialContainer.posList.ElementAt(spacialCardsList.FindIndex(x => x.name == _spacial.name)).transform.position;
            spacial.transform.position = _specialContainer.posList.ElementAt(spacialCardsList.FindIndex(x => x.name == _spacial.name)).transform.position;
            spacial.transform.SetParent(_specialContainer.posList.ElementAt(spacialCardsList.FindIndex(x => x.name == _spacial.name)).transform);
            spacial.transform.rotation = Quaternion.Euler(80, 0, 0);
            spacial.GetComponent<RectTransform>().sizeDelta = _specialContainer.posList.ElementAt(spacialCardsList.FindIndex(x => x.gameObject.name == _spacial.gameObject.name)).transform.GetComponent<RectTransform>().sizeDelta;
            spacial.GetComponent<CardMovements>().setRotatable(false);
            spacial.transform.localScale = Vector3.one;
            cardAnims.cardAnimations.cardkMoveReturnPosAnim(spacial.transform, 1f, false);
            //spacial.transform.position =new Vector3(0,0,0);

            setSpecCount();
            activeDeactiveSpecialCardObject(true);
        }

    }
    public void removeSpacial(GameObject _spacial)
    {
        spacialCardsList.RemoveAt(spacialCardsList.FindIndex(x => x.gameObject.name.Equals(_spacial.gameObject.name)));
        bool isLast = false;
        if (spacialCardsList.Count <= 0)
            isLast = true;
        _specialContainer.destroyCard(_specialContainer.currentSpecials.Find(x=>x ==_spacial),isLast);
        if (spacialCardsList.Count <= 0)
        {
            spacialCardsList.Clear();
            activeDeactiveSpecialCardObject(false);
        }
        else
        {
            setSpecCount();
        }
        clearSpacialList();
    }
    public void removeSpacialTypes(Card.SpacialType _type)
    {
        int size = spacialCardsList.Count - 1;
        if (spacialCardsList.Count > 0)
        {
            for (int i = size; i >= 0; i--)
            {
                if (spacialCardsList[i].GetComponent<CardInfos>().card.specialType.Equals(_type))
                {
                    spacialCardsList.RemoveAt(i);   
                }
            }
        }

    }
    public void clearSpacialList()
    {
        int size = spacialCardsList.Count - 1;
        if (spacialCardsList.Count > 0)
        {
            for (int i = size; i >= 0; i--)
            {
                if (spacialCardsList[i].GetComponent<CardInfos>().card.duration<=0)
                {
                    spacialCardsList.RemoveAt(i);
                }
            }
        }
        int sizeRomove = cardToDestroy.Count - 1;
        bool isLast = false;

        if (spacialCardsList.Count <= 0)
        {
            if(_specialContainer.getIsPanelActive())
                _specialContainer._animator.Play("specialCardsPanelClose");
        }
            


    }
    public List<GameObject> getSpacialList()
   {
        return spacialCardsList;
   }
    public void setSpecCount()
   {
        specialCardCount.text = spacialCardsList.Count.ToString();
   }
    public void activeDeactiveSpecialCardObject(bool _state)
    {
        specialCardObj.SetActive(_state);
    }
    //public void IDead()
    //{
    //    throw new System.NotImplementedException();
    //}
}

