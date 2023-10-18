using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerSpacial_Controller : MonoBehaviour
{
    [SerializeField] Slider spacialSlider;
    [SerializeField] Button spacialButton;
    [SerializeField] GameObject fxSpacialActiveFx;
    [SerializeField] int spacialNeedGemCount;
    [SerializeField] int currentGemCount=0;
    [SerializeField] int normalDamge;
    [SerializeField] int SpacialDamage;
    bool spacialUsable = true;
    MiniGame_Manager _gameManager;
    private void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("Managements").gameObject.GetComponent<MiniGame_Manager>();
        setSpacialUsed();
    }
    public Button getSpacialButton()
    {
        return spacialButton;
    }
    public void activeDeactiveButton(bool state)
    {
        spacialButton.interactable = state;
    }
    public void activeDeactiveFx(bool state)
    {
        
        if (state)
        {
            //if (!fxSpacialActiveFx.activeInHierarchy)
            //{
            //    fxSpacialActiveFx.SetActive(state);
            //}
          
                LeanTween.scale(spacialSlider.transform.parent.gameObject, new Vector3(1.1f, 1.1f, 1.1f), 1f).setEase(LeanTweenType.easeInSine);
            
        }
        else
        {
            //fxSpacialActiveFx.SetActive(state);
            LeanTween.scale(spacialSlider.transform.parent.gameObject, Vector3.one, 1f).setEase(LeanTweenType.easeOutSine);
        }
    }
    public void setSpacialNeedGemCount(int value)
    {
        spacialNeedGemCount = value;
        setSpacialSliderMax();
    }
    public void resetSpacialCurrentGemCount()
    {
        currentGemCount = 0;
    }
    public void setSpacialSliderMax()
    {
        //spacialSlider.maxValue=spacialNeedGemCount;
    }
    public void setSlidersValue(int value)
    {
        if(!_gameManager.currentPlayer.GetComponent<Player>().spacialUsing)
            currentGemCount += value;
        //Debug.Log("Current GEm Count:" + currentGemCount);
        //spacialSlider.value = currentGemCount;
        if (currentGemCount >= spacialNeedGemCount && !spacialUsable)
        {
            spacialUsable = true;
            activeDeactiveSpacial(true);
        }

    }
    public void activeDeactiveSpacial(bool state)
    {
        if (state)
        {
            if (!SaveLoad_Manager.gameData.isClashedOnce)
            {
                Debug.Log("CAlled");
                tutorialController.TInstance.miniGameTutorial(GameObject.FindGameObjectWithTag("Managements").gameObject, false);
            }
            else
            {
                activeDeactiveButton(state);
                activeDeactiveFx(state);
            }
        }
        else
        {
            activeDeactiveButton(state);
            activeDeactiveFx(state);
        }
       
        
    }
    public void setSpacialUsed()
    {
        spacialUsable = false;
        currentGemCount = 0;
        setSlidersValue(0);
        activeDeactiveSpacial(false);
    }
    public void keepGoing()
    {
        activeDeactiveButton(true);
        activeDeactiveFx(true);
    }
}
