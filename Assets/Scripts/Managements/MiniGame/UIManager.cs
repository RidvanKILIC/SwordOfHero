using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Slider progressSlider;
    [SerializeField] Image progressSliderBossImage;
    [SerializeField] Slider playerSpacialSlider;
    [SerializeField] GameObject playerSpacialButton;
    [SerializeField] PlayerHUD_Controller playerHud;
    private static UIManager instance;
    public static UIManager UInstance
    {
        get
        {
            if (instance == null)
            {
                //Debug.LogError("Save Load Manager's instance is null");
            }
            return instance;
        }
    }
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setProgressSliderMax(int Value)
    {
        progressSlider.maxValue = Value;
        setProgressSliderValue(0);
    }
    public void setProgressSliderValue(int Value)
    {
        progressSlider.value = Value;
    }

    public void updatePlayerHealth(float _value)
    {
        playerHud.updateHealth(_value);
    }
    public void updatePlayerShield(float _value)
    {
        playerHud.updateShield(_value);
    }
}
