using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using RK.GameDatas;
using Spine.Unity;
using RK.Skeleton;
public class PlayerHUD_Controller : MonoBehaviour
{
    [SerializeField] Image shieldImage;
    [SerializeField] Slider healthBar;
    [SerializeField] TMP_Text healthText;
    [SerializeField] Slider shieldBar;
    [SerializeField] TMP_Text shieldText;
    [SerializeField] Slider energyBar;
    [SerializeField] TMP_Text goldText;
    [SerializeField] GameObject Hair;
    [SerializeField] GameObject Beard;
    [SerializeField] GameObject BeardExtension;
    [SerializeField] GameObject Wound;
    [SerializeField] GameObject Mustache;
    // Start is called before the first frame update
    void Start()
    {
        //Init();
    }
    public void Init()
    {
        //healthBar.maxValue = SaveLoad_Manager.gameData.playerInfos.playerBaseHealth;
        updateHealth(SaveLoad_Manager.gameData.playerInfos.playerBaseHealth);
        //shieldBar.maxValue = SaveLoad_Manager.gameData.playerInfos.playerBaseShield;
        updateShield(SaveLoad_Manager.gameData.playerInfos.playerCurrentShield);
        goldText.text = SaveLoad_Manager.gameData.playerInfos.playerGoldCount.ToString();
        setSkin();
    }

    public void setSkin()
    {
        if (!string.IsNullOrEmpty(SaveLoad_Manager.gameData.playerInfos.equippedHair))
        {
            //Debug.Log("?");
            Hair choosenHair = ObjectsLists.hair.Find(x => x.name == SaveLoad_Manager.gameData.playerInfos.equippedHair);
            if (choosenHair != null)
            {
                if (!string.IsNullOrEmpty(choosenHair.hair))
                {
                    changeSkin.changeSkeletonSkin(Hair.GetComponent<SkeletonAnimation>(), "Hairs/" + choosenHair.hair);
                    //Hair.GetComponent<changeSkin>().changeSkeletonSkin("Hairs/" + choosenHair.hair);
                }
                else
                {
                    Hair.SetActive(false);
                }
                if (!string.IsNullOrEmpty(choosenHair.beard))
                {
                    changeSkin.changeSkeletonSkin(Beard.GetComponent<SkeletonAnimation>(), "Hairs/" + choosenHair.beard);
                    //Beard.GetComponent<changeSkin>().changeSkeletonSkin();
                }
                else
                {
                    Beard.SetActive(false);
                }
                if (!string.IsNullOrEmpty(choosenHair.beardExt))
                {
                    changeSkin.changeSkeletonSkin(BeardExtension.GetComponent<SkeletonAnimation>(), "Hairs/" + choosenHair.beardExt);
                    //BeardExtension.GetComponent<changeSkin>().changeSkeletonSkin("Hairs/" + choosenHair.beardExt);
                }
                else
                {
                    BeardExtension.SetActive(false);
                }
                if (!string.IsNullOrEmpty(choosenHair.mustache))
                {
                    changeSkin.changeSkeletonSkin(Mustache.GetComponent<SkeletonAnimation>(), "Hairs/" + choosenHair.mustache);
                    //Mustache.GetComponent<changeSkin>().changeSkeletonSkin("Hairs/" + choosenHair.mustache);
                }
                else
                {
                    Mustache.SetActive(false);
                }
                if (!string.IsNullOrEmpty(choosenHair.wound))
                {
                    changeSkin.changeSkeletonSkin(Wound.GetComponent<SkeletonAnimation>(), "Hairs/" + choosenHair.wound);
                    //Wound.GetComponent<changeSkin>().changeSkeletonSkin("Hairs/" + choosenHair.wound);
                }
                else
                {
                    Wound.SetActive(false);
                }
            }
        }
        else
        {
            Hair.SetActive(false);
            Beard.SetActive(false);
            BeardExtension.SetActive(false);
            Mustache.SetActive(false);
            Wound.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void updateHealth(float _value)
    {
        healthBar.maxValue = SaveLoad_Manager.gameData.playerInfos.playerBaseHealth;
        healthText.text = SaveLoad_Manager.gameData.playerInfos.playerBaseHealth.ToString() + "/" + SaveLoad_Manager.gameData.playerInfos.playerBaseHealth;
        healthBar.value = SaveLoad_Manager.gameData.playerInfos.playerBaseHealth;
    }
    public void updateShield(float _value)
    {
        shieldBar.maxValue = SaveLoad_Manager.gameData.playerInfos.playerBaseShield;
        shieldText.text = SaveLoad_Manager.gameData.playerInfos.playerBaseShield + "/" + SaveLoad_Manager.gameData.playerInfos.playerBaseShield;
        shieldBar.value = SaveLoad_Manager.gameData.playerInfos.playerBaseShield;
    }
    public void updateGold()
    {
        goldText.text = SaveLoad_Manager.gameData.playerInfos.playerGoldCount.ToString();
    }
    public void updateGoldText(int _value)
    {
        goldText.text = _value.ToString();
    }
}
