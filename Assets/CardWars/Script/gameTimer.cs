using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameTimer : MonoBehaviour
{
    public float timeRemaining;
    bool timerIsRunning = false;
    bool timeIsUp = false;
    [SerializeField]TMPro.TMP_Text timeTxt;
    [SerializeField] UnityEngine.UI.Slider timerSlider;
    GameManager _gameManager;
    Animator _anim;
    private void Start()
    {
        setTimerSliderMaxValue(timeRemaining);
        _gameManager = GameObject.FindObjectOfType<GameManager>();
        _anim = GetComponent<Animator>();
        // Starts the timer automatically
        timerIsRunning = true;

    }
    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                if (timeRemaining <= 3)
                {
                    _anim.Play("generalTimeF");
                }
                timeRemaining -= Time.deltaTime;
                if (timeRemaining > 0)
                    DisplayTime(timeRemaining);
                else
                    DisplayTime(0);
            }
            else
            {
                //Debug.Log("Time has run out!");
                timeRemaining = 0;
                DisplayTime(timeRemaining);
                setTimeIsRunning(false);
                setTimeIsUp(true);
               //_gameManager.gameOver();
            }
        }
        //if(timeRemaining<=0 && _gameManager.getPickingCardAvailable())
        //{
        //    _gameManager.gameOver();
        //}
    }
    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        setTimerSliderValue(timeRemaining);
    }
    void setTimerSliderMaxValue(float value)
    {
        timerSlider.maxValue = value;
        timerSlider.value = value;
    }
    void setTimerSliderValue(float value)
    {
        timerSlider.value = value;
    }
    public float getCurrentTime()
    {
        return timeRemaining;
    }
    public void setTimeIsRunning(bool state)
    {
        timerIsRunning=state;
    }
    public void setTimeIsUp(bool state)
    {
        timeIsUp = state;
    }
    public bool getTimeIsRunning()
    {
        return timerIsRunning;
    }
    public bool getTimeIsUp()
    {
        return timeIsUp;
    }
}
