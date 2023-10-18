using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardMovements : MonoBehaviour
{
    #region Variables

    #region discarded
    //[SerializeField] float x;
    //[SerializeField] float y;
    //[SerializeField] float z;
    #endregion
    [Header("Card's Objects")]
    [SerializeField] GameObject cardFront;
    [SerializeField] GameObject cardBack;
    [Header("Card Rotate Variables")]
    [SerializeField] public bool cardFrontIsActive;
    public bool rotateCompleted;
    bool rotatable = true;
    public bool movedToDest=false;
    public GameObject currentParent;
    [Header("References & Other Variables")]
    [SerializeField] LayerMask cardLayer;
    GameManager _gameManager;
    timebarScript _timeBarScript;
    gameTimer _gameTimer;
    CardInfos _cardInfos;
    [SerializeField] int timer;
    #endregion
    // Start is called before the first frame update
    void Awake()
    {
        Init();
    }
    /// <summary>
    /// Initializes Objects and References
    /// </summary>
    void Init()
    {
        _gameManager = GameObject.FindObjectOfType<GameManager>();
        _timeBarScript = GameObject.FindObjectOfType<timebarScript>();
        _gameTimer = GameObject.FindObjectOfType<gameTimer>();
        _cardInfos = GetComponent<CardInfos>();
        //setInHand(false);
        cardFrontIsActive = false;
        rotateCompleted = true;
        //setTriggersFunctions();-----------------------------------------> scaleUpFunction
    }
    ///// <summary>
    ///// Set triggers Events
    ///// </summary>
    //void setTriggersFunctions()
    //{

    //    EventTrigger trigger = GetComponent<EventTrigger>();
    //    EventTrigger.Entry entry = new EventTrigger.Entry();
    //    entry.eventID = EventTriggerType.PointerDown;
    //    entry.callback.AddListener((eventData) => { _gameManager.openScaleUpPanel(this.gameObject); });

    //    EventTrigger.Entry entry2 = new EventTrigger.Entry();
    //    entry2.eventID = EventTriggerType.PointerUp;
    //    entry2.callback.AddListener((eventData) => { _gameManager.closeScaleUpPanel(); });

    //    trigger.triggers.Add(entry);
    //    trigger.triggers.Add(entry2);
    //}

    // Update is called once per frame
    void Update()
    {
        
        //if (/*(Input.GetMouseButton(0) && IsPointerOverUIObject()) &&*/ _gameManager.getPickingCardAvailable() && rotatable &&_gameManager.getCurrentTurn().name.Equals("Player") && _timeBarScript.getTime() >= 1)
        //{
        //    //rotateCompleted = false;
        //    if (rotateCompleted && !cardFrontIsActive/* && !_cardInfos.getInHand()*/)
        //    {
        //        //rotateCompleted = false;
        //        //Debug.Log("rotateCompleted " + rotateCompleted);
        //        //Debug.Log("cardFrontIsActive " + cardFrontIsActive);
        //        //Debug.Log("getPickingCardAvailable " + _gameManager.getPickingCardAvailable());

        //            flip();
        //    }
        //    //else
        //    //{
        //    //    don
        //    //}
        //    //else if (_cardInfos.getInHand())
        //    //{
        //    //    Debug.Log("Clicked in hand:"+ this.gameObject.name);
        //    //    _gameManager.useCardFromHand(this.gameObject);
        //    //}
        //    //else
        //    //{
        //    //    Debug.Log("Something is wrong");
        //    //}
        //}
    }
    public void cardButton()
    {
        if (/*(Input.GetMouseButton(0) && IsPointerOverUIObject()) &&*/ _gameManager.getPickingCardAvailable() && rotatable && _gameManager.getCurrentTurn().name.Equals("Player") && _timeBarScript.getTime() >= 1)
        {
            //rotateCompleted = false;
            if (rotateCompleted && !cardFrontIsActive/* && !_cardInfos.getInHand()*/)
            {
                //rotateCompleted = false;
                //Debug.Log("rotateCompleted " + rotateCompleted);
                //Debug.Log("cardFrontIsActive " + cardFrontIsActive);
                //Debug.Log("getPickingCardAvailable " + _gameManager.getPickingCardAvailable());

                flip();
            }
            //else
            //{
            //    don
            //}
            //else if (_cardInfos.getInHand())
            //{
            //    Debug.Log("Clicked in hand:"+ this.gameObject.name);
            //    _gameManager.useCardFromHand(this.gameObject);
            //}
            //else
            //{
            //    Debug.Log("Something is wrong");
            //}
        }
    }
    /// <summary>
    /// Checks if pointer on this object or not
    /// </summary>
    /// <returns></returns>
    public bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        foreach (RaycastResult r in results)
        {
            //if (r.gameObject.layer.Equals(cardLayer))
            //{
                if (r.gameObject.GetComponent<RectTransform>() != null)
                {

                Debug.Log("Game Object:"+r.gameObject.name);
                Debug.Log("Partent:" + r.gameObject.transform.parent.gameObject.name);
                    if (r.gameObject.transform.parent.gameObject.Equals(this.gameObject))
                    {
                        return true;
                    }

                }
            //}

        }


        return false;
    }
    /// <summary>
    /// set animators FlipCard parameter to !cardFrontIsActive value
    /// </summary>
    public void flip()
    {
        bool callFunction = true;
        if (_gameManager.getCurrentTurn().GetComponent<Player_Card>().getSpacialList().Count > 0)
        {
            List<GameObject> specList = _gameManager.getCurrentTurn().GetComponent<Player_Card>().getSpacialList();
            foreach (var item in specList)
            {
                if (item.GetComponent<CardInfos>().card.Location.Equals(Card.callLocation.BothCardSelection) && item.GetComponent<CardInfos>().card.FunctionType.Equals(Card.FunctionCallType.Replace))
                {
                    callFunction = false;
                    item.GetComponent<CardInfos>().card.Special(_gameManager.getCurrentTurn());
                    Debug.Log(item.name + "Called");
                }
            }
        }
        if (callFunction)
        {
            flipRoutine();
        }


    }
    public void flipRoutine()
    {
        _cardInfos.activateDeactivateHighLight(false);
        if (!cardFrontIsActive)
            SoundManager.SInstance.playSfx(cardSounds.SInstance.cardsTurningSFX);

        rotateCompleted = false;
        transform.SetParent(GameObject.Find("Canvas").transform);
        this.GetComponent<Animator>().SetBool("FlipCard", !cardFrontIsActive);
    }
    /// <summary>
    /// Activates deactivates Cart front and back object according to the cardFrontIs active variable's value
    /// </summary>
    public void flipCard()
    {
        if (rotatable)
        {
            //setRotatable(false);
            if (!cardFrontIsActive)
            {
                _gameManager.setChoosenCard(this.gameObject);
                //Debug.Log("Card Turned");
                activateDeactivateCardFront(true);
                activateDeactivateCardBack(false);
                cardFrontIsActive = true;
            }
            else
            {
                //Debug.Log("Card Turnedback");
                activateDeactivateCardFront(false);
                activateDeactivateCardBack(true);
                cardFrontIsActive = false;
            }
        }

    }
    public void activateDeactivateCardBack(bool _state)
    {
        cardBack.SetActive(_state);
    }
    public void activateDeactivateCardFront(bool _state)
    {
        //var stackTrace = new StackTrace();
        //var caller = stackTrace.GetFrame(1).GetMethod().Name;
        //UnityEngine.Debug.Log("dealCards " + "called by " + caller);
        cardFront.SetActive(_state);
    }
    /// <summary>
    /// Set rotate completed variable to true
    /// </summary>
    public void finishRotating()
    {
        //Debug.Log("ChangingPanrent");
        transform.SetParent(currentParent.transform);
        rotateCompleted = true;
    }
    /// <summary>
    /// Calls MovePos coroutine with the given parameter
    /// </summary>
    /// <param name="dest"></param>
    public void moveCard(Vector3 dest)
    {
        StartCoroutine(MovePos(dest));
    }
    #region discarded
    ///// <summary>
    ///// According to the given parameter activate deactivates cardDescText gameObject
    ///// </summary>
    ///// <param name="_state"></param>
    //public void activateDeactivateCardDescText(bool _state)
    //{
    //    cardDescText.gameObject.SetActive(_state);
    //}
    #endregion
    /// <summary>
    /// Assigns given parameter to rotatable variable
    /// </summary>
    /// <param name="_state"></param>
    public void setRotatable(bool _state)
    {
        rotatable = _state;
    }


    #region Discarded
    /// <summary>
    /// Flips this gameObject
    /// </summary>
    /// <returns></returns>
    //IEnumerator cardFlipRountine()
    //{
    //    rotateCompleted = false;
    //    yield return null;
    //    for(int i = 0; i < 180; i++)
    //    {
    //        transform.Rotate(new Vector3(x, y, z));
    //        timer++;
    //        if(timer == 90 || timer == -90)
    //        {
    //            flipCard();
    //        }
    //    }
    //    rotateCompleted = true;
    //    timer = 0;
    //}
    #endregion

    /// <summary>
    /// Moves this object to the given position
    /// </summary>
    IEnumerator MovePos(Vector3 dest)
    {
        float inTime = 0.8f;

        Vector3 fromPos = transform.position;
        Vector3 Endpos = dest;

        for (float t = 0f; t <= 1; t += Time.deltaTime / inTime)
        {

            transform.position = Vector3.Lerp(fromPos, Endpos, t);
            if (Vector3.Distance(transform.position,Endpos)==0)
            {
                movedToDest = true;
                this.transform.localPosition = Vector3.zero;
                //Debug.Log("Position set!");
            }
           
            yield return null;
        }

    }
}
