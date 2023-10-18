using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class timebarScript : MonoBehaviour
{
    #region Variables
    [Header("Timer Objets")]
    [SerializeField]TMPro.TMP_Text timeText;
    [SerializeField]UnityEngine.UI.Slider timerSlider;
    [SerializeField] GameObject timerObj;
    [SerializeField] GameObject lockObj;
    [Header("Timer Variables")]
    float maxTime;
    float currentTime;
    public bool timerControl;
    bool pauseTimer = false;
    [Header("References")]
    GameManager _gameManager;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
    /// <summary>
    /// Initiliazes objects, references & variables
    /// </summary>
    void Init()
    {
        _gameManager = GameObject.FindObjectOfType<GameManager>();
        //timerObj = gameObject.transform.GetComponentsInChildren<Transform>(true).FirstOrDefault(t => t.name.Equals("obj")).gameObject;
        //timerAnimator = timerObj.transform.GetComponentsInChildren<Transform>(true).FirstOrDefault(t => t.name.Equals("icon")).gameObject.GetComponent<Animator>();

        //Debug.Log(this.transform.parent.gameObject.name+" "+timerAnimator.gameObject.name);
        //transform.Find("icon").gameObject.GetComponent<Animator>();
        //timeText = transform.Find("text").GetComponent<TMPro.TMP_Text>();
        //timerSlider = GetComponent<UnityEngine.UI.Slider>();
        //activateDeactivateTimeBar(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (timerControl)
            timer();
    }
    /// <summary>
    /// Activates Deactivates this gameobject according to the given parameter
    /// </summary>
    /// <param name="_state"></param>
    public void activateDeactivateTimeBar(bool _state)
    {
        timerObj.SetActive(_state);
    }
    /// <summary>
    /// Assigns given string to timeText's text object
    /// </summary>
    /// <param name="_time"></param>
    void setTimeText(string _time)
    {
        timeText.text = _time;
    }
    public void updateMaxTime(float time)
    {
        maxTime = time;
    }
    /// <summary>
    /// Assign gicen float to maxTime variable then assign maxTime to current time after that call setTimer max value and sends the max value as a parameter  
    /// </summary>
    /// <param name="time"></param>
    public void setMaxTime(float time)
    {
        //Debug.Log("Max Time Set: "+time);
        maxTime = time;
        currentTime = maxTime;
        setTimerMaxValue(maxTime);
    }
    /// <summary>
    /// Increases current time as much as the given paramater
    /// </summary>
    /// <param name="time"></param>
    public void ýncreaseTime(float time)
    {
        currentTime += time;
    }
    public void decreaseMaxTime(float time)
    {
        maxTime -= time;
        setMaxTime(maxTime);
    }
    /// <summary>
    /// returns currentTime
    /// </summary>
    /// <returns></returns>
    public float getTime()
    {
        //Debug.Log(Mathf.Ceil(currentTime));
        return Mathf.Ceil(currentTime);
    }
    /// <summary>
    /// returns currentTime
    /// </summary>
    /// <returns></returns>
    public float getMaxTime()
    {
        return maxTime;
    }
    /// <summary>
    /// By assigning given parameter in to timerControl starts stops times true for start false for stop
    /// </summary>
    /// <param name="_state"></param>
    public void startStopTimer(bool _state)
    {
        lockObj.SetActive(false);
        activateDeactivateTimeBar(_state);
        timerControl = _state;
    }
    /// <summary>
    /// Resets timer variables
    /// </summary>
    public void resetTimer()
    {
        setTimerMaxValue(maxTime);
    }
    /// <summary>
    /// Decreases currentTime variable, calls setTimeText with the paramaeter currentTime and calls setTimerValue with the parameter currentValue
    /// </summary>
    void timer()
    {
        if (currentTime > 0 )
        {
            //Debug.Log("Current time: "+currentTime);
            if (!pauseTimer)
            {
                currentTime -= Time.deltaTime;
            }
            if(currentTime < 1 && currentTime > 0 /*&& _gameManager.getChoosenCardsCount() <= 1*/)
            {
                if (Input.GetMouseButton(0))
                {
                    SoundManager.SInstance.playSfx(cardSounds.SInstance.cardsNotMatchedSFX);
                    //cardAnims.cardAnimations.shakeAnim(lockObj.transform,0.005f,1f);
                    //cardAnims.cardAnimations.smallShakeAnim(lockObj.transform, 1f);
                }
                _gameManager.setPickingCardAvailable(false);
                lockObj.SetActive(true);
            }
            setTimeText((Mathf.RoundToInt(currentTime)).ToString());
            setTimerValue(currentTime);
        }
        else
        {
            _gameManager.clearCards();
            //Debug.Log("Turn ended");
            StartCoroutine(timeIsUpRoutine());
        }
    }
    IEnumerator timeIsUpRoutine()
    {
        startStopTimer(false);
        yield return new WaitForSeconds(1);
        _gameManager.timeUp();
    }
    /// <summary>
    /// Assing the given parameter to timerSlider's max value  and timerSlider's value
    /// </summary>
    /// <param name="_value"></param>
    void setTimerMaxValue(float _value)
    {
        timerSlider.maxValue = _value;
        timerSlider.value = _value;
        currentTime = maxTime;
        setTimeText(_value.ToString());
    }
    /// <summary>
    /// Assing the given parameter to timerSlider's value
    /// </summary>
    /// <param name="_value"></param>
    void setTimerValue(float _value)
    {
        timerSlider.value = _value;
    }
    public void pauseResumeTimer(bool _state)
    {
        pauseTimer = _state;
    }
}
