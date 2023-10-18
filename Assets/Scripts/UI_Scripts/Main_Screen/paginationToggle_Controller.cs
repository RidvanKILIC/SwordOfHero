using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;
public class paginationToggle_Controller : MonoBehaviour
{
    Toggle _toggle;
    [SerializeField] LocalizeStringEvent marketTitle;
    [SerializeField] GameObject textObject;
    [SerializeField] GameObject BG;
    // Start is called before the first frame update
    void Start()
    {
        _toggle = GetComponent<Toggle>();
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void onValueChanged()
    {
        SoundManager.SInstance.playSfx(SoundManager.SInstance.swipeSFX);
        //Debug.Log("ValueChanged");
        if (_toggle.isOn)
        {
            if (this.gameObject.name == "Housing" || this.gameObject.name == "Armory")
            {
                GameObject.FindGameObjectWithTag("Managements").gameObject.GetComponent<scroolBarStartPos>().setScroolReactVerticalPos();
            }
            textObject.SetActive(true);//this.gameObject.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            if (this.gameObject.name=="Armory" && !SaveLoad_Manager.gameData.isArmoryEquipped)
            {
                tutorialController.TInstance.endArmoryTutorial();
            }
            else if(this.gameObject.name == "Home" && SaveLoad_Manager.gameData.isArmoryEquipped && !SaveLoad_Manager.gameData.isClashedOnce)
            {
                tutorialController.TInstance.mainMenuFirstClashTutorial(GameObject.FindGameObjectWithTag("Managements").gameObject);
            }
            else if (this.gameObject.name == "Home" && SaveLoad_Manager.gameData.isClashedOnce && SaveLoad_Manager.gameData.isHousingBought && SaveLoad_Manager.gameData.isArmoryEquipped
                     && SaveLoad_Manager.gameData.isArmoryEquipped && SaveLoad_Manager.gameData.isClashedOnce && !SaveLoad_Manager.gameData.isCardGameActivated)
            {
                if (!SaveLoad_Manager.gameData.isHousingTutorialEnd)
                {
                    SaveLoad_Manager.gameData.isHousingTutorialEnd = true;
                    tutorialController.TInstance.endmainMenuHousingTutorial(GameObject.FindGameObjectWithTag("Managements").gameObject);
                }
                
                tutorialController.TInstance.mainMenuActivateCardTutorial(GameObject.FindGameObjectWithTag("Managements").gameObject);
            }
        }
        else
        {
            textObject.SetActive(false);//this.gameObject.transform.localScale = Vector3.one;
        }
            
    }
    public void onValueChangedScale()
    {
        SoundManager.SInstance.playSfx(SoundManager.SInstance.swipeSFX);
        //Debug.Log("ValueChanged");
        if (_toggle.isOn)
        {
            this.gameObject.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            BG.GetComponent<Image>().color = new Color32(255,255,255, 255);
        }
        else
        {
            BG.GetComponent<Image>().color = new Color32(162, 162, 162, 255);
            this.gameObject.transform.localScale = Vector3.one;
        }
           
    }
    public void changeTitle(string _key)
    {
        if (_toggle.isOn)
            marketTitle.StringReference.SetReference("UI_Texts_Main_Screen", _key);
    }
}
