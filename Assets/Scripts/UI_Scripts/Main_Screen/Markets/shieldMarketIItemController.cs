using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class shieldMarketIItemController : MonoBehaviour
{
    [Header("Button Variables")]
    [SerializeField] GameObject BuyButton;
    [SerializeField] Image buttonImageSlot;
    [SerializeField] LocalizeStringEvent ButtonString;
    [Header("Description Variavles")]
    [SerializeField] setMarketItemsInfoTextsForString _nameText;
    [SerializeField] setMarketItemsInfoTextsForInt _defenseText;
    [SerializeField] setMarketItemsInfoTextsForInt _priceText;
    [Header("Icon Variables")]
    [SerializeField] Image iconImageSlot;
    [SerializeField] Image lockImage;
    [Header("Resources")]
    [SerializeField] Sprite buyImage;
    [SerializeField] Sprite equippedImage;
    [SerializeField] Sprite releaseImage;
    Shield shield;
    // Start is called before the first frame update
    void Awake()
    {
        //BuyButton.GetComponent<Button>().onClick.AddListener(() => BuyWeapon());
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void setShield(Shield _shield)
    {
        shield = null;
        shield = _shield;
        setObject();
    }
    public void setObject()
    {
        if (shield.isLock)
        {
            lockImage.gameObject.SetActive(true);
            BuyButton.GetComponent<Button>().interactable = false;
        }
        if (SaveLoad_Manager.gameData.playerInfos.BoughtshieldList.Contains(shield._name))
        {
            shield.status = shield.status = BaseItem.Status.Bought;
            if (SaveLoad_Manager.gameData.playerInfos.equippedShield == shield._name)
            {
                shield.isEquipped = true;
            }
        }
        string _key = shield._name;
        _nameText.setText("Shield_Names", _key + "_Text");
        _defenseText.setText(shield.defense);
        _priceText.setText(shield.Cost);
        iconImageSlot.overrideSprite = shield.sprite;
        if (shield.status.Equals(BaseItem.Status.Bought))
        {
            if (shield.isEquipped)
            {
                buttonImageSlot.sprite = releaseImage;
                ButtonString.StringReference.SetReference("marketItem_Texts", "itemButton_Text_Release");
                BuyButton.GetComponent<Button>().onClick.AddListener(() => releaseWeapon());
            }
            else
            {
                buttonImageSlot.sprite = equippedImage;
                ButtonString.StringReference.SetReference("marketItem_Texts", "itemButton_Text_Equip");
                BuyButton.GetComponent<Button>().onClick.AddListener(() => equipWeapon());
            }
        }
        else
        {
            buttonImageSlot.sprite = buyImage;
            ButtonString.StringReference.SetReference("marketItem_Texts", "itemButton_Text_Buy");
            BuyButton.GetComponent<Button>().onClick.AddListener(() => BuyWeapon());
        }
    }
    public void updateButtonText()
    {
        if (shield.status.Equals(BaseItem.Status.Bought))
        {
            if (shield.isEquipped)
            {
                buttonImageSlot.sprite = releaseImage;
                ButtonString.StringReference.SetReference("marketItem_Texts", "itemButton_Text_Release");
                BuyButton.GetComponent<Button>().onClick.AddListener(() => releaseWeapon());
            }
            else
            {
                buttonImageSlot.sprite = equippedImage;
                ButtonString.StringReference.SetReference("marketItem_Texts", "itemButton_Text_Equip");
                BuyButton.GetComponent<Button>().onClick.AddListener(() => equipWeapon());
            }
        }
        wearItem();
    }

    public void BuyWeapon()
    {
        SoundManager.SInstance.playSfx(SoundManager.SInstance.wearSFX);
        if (SaveLoad_Manager.gameData.playerInfos.playerGoldCount >= shield.Cost)
        {
            if (!string.IsNullOrEmpty(SaveLoad_Manager.gameData.playerInfos.equippedShield))
            {
                //Debug.Log("itemList count " + WeaponaryMarketController.marketShieldItemLists.Count);
                var exShield = GameObject.FindGameObjectWithTag("Managements").GetComponent<WeaponaryMarketController>().marketShieldItemLists.Find(x => x.GetComponent<shieldMarketIItemController>().shield._name == SaveLoad_Manager.gameData.playerInfos.equippedShield);
                if (exShield != null)
                {
                    //Debug.Log("Ex Shield" + exShield.name);
                    SaveLoad_Manager.gameData.playerInfos.playerBaseShield -= exShield.GetComponent<shieldMarketIItemController>().shield.defense;
                    exShield.GetComponent<shieldMarketIItemController>().shield.isEquipped = false;
                    exShield.GetComponent<shieldMarketIItemController>().updateButtonText();
                }

            }
            shield.status = BaseItem.Status.Bought;
            shield.isEquipped = true;
            SaveLoad_Manager.gameData.playerInfos.equippedShield = shield._name;
            if (!SaveLoad_Manager.gameData.playerInfos.BoughtswordList.Contains(shield._name))
            {
                SaveLoad_Manager.gameData.playerInfos.BoughtshieldList.Add(shield._name);
                SaveLoad_Manager.gameData.playerInfos.playerGoldCount -= shield.Cost;
                SaveLoad_Manager.gameData.playerInfos.playerBaseShield += shield.defense;
                GameObject.FindGameObjectWithTag("Managements").GetComponent<mainMenu_Controller>().updateShieldValue();
                GameObject.FindGameObjectWithTag("Managements").GetComponent<mainMenu_Controller>().updateGold();
                GameObject.FindGameObjectWithTag("Managements").GetComponent<WeaponaryMarketController>().updateGoldTexts();
            }
            if (!SaveLoad_Manager.gameData.isArmoryEquipped)
            {
                tutorialController.TInstance.deActivaeShieldArrow();
                tutorialController.TInstance.activateArmorArrow(GameObject.FindGameObjectWithTag("Managements").gameObject);
            }
            updateButtonText();
        }
        else
        {
            GameObject.FindGameObjectWithTag("Managements").GetComponent<mainMenu_Controller>().activateMArketWarningPanel();
        }
    }

    public void equipWeapon()
    {
        SoundManager.SInstance.playSfx(SoundManager.SInstance.wearSFX);

        if (SaveLoad_Manager.gameData.playerInfos.BoughtshieldList.Contains(shield._name))
        {
            if (!string.IsNullOrEmpty(SaveLoad_Manager.gameData.playerInfos.equippedShield))
            {
                //Debug.Log("itemList count " + WeaponaryMarketController.marketShieldItemLists.Count);
                var exSword = GameObject.FindGameObjectWithTag("Managements").GetComponent<WeaponaryMarketController>().marketShieldItemLists.Find(x => x.GetComponent<shieldMarketIItemController>().shield._name == SaveLoad_Manager.gameData.playerInfos.equippedShield);
                if (exSword != null)
                {
                    //Debug.Log("Ex Shield" + exSword.name);
                    SaveLoad_Manager.gameData.playerInfos.playerBaseShield -= exSword.GetComponent<shieldMarketIItemController>().shield.defense;
                    exSword.GetComponent<shieldMarketIItemController>().shield.isEquipped = false;
                    exSword.GetComponent<shieldMarketIItemController>().updateButtonText();
                }

            }
            SaveLoad_Manager.gameData.playerInfos.equippedShield = shield._name;
            SaveLoad_Manager.gameData.playerInfos.playerBaseShield += shield.defense;
            shield.isEquipped = true;
        }
        updateButtonText();
    }
    public void wearItem()
    {
        GameObject.FindGameObjectWithTag("Managements").GetComponent<mainMenu_Controller>().updatePlayer();
        GameObject.FindGameObjectWithTag("Managements").GetComponent<mainMenu_Controller>().updateShieldValue();
        SaveLoad_Manager.SInstance.saveJson();
    }
    public void releaseWeapon()
    {
        SoundManager.SInstance.playSfx(SoundManager.SInstance.wearSFX);
        if (SaveLoad_Manager.gameData.playerInfos.equippedShield == shield._name)
        {
            SaveLoad_Manager.gameData.playerInfos.playerBaseShield -= shield.defense;
            //Debug.Log(SaveLoad_Manager.gameData.playerInfos.equippedSword);
            SaveLoad_Manager.gameData.playerInfos.equippedShield = string.Empty;
        }
        shield.isEquipped = false;
        updateButtonText();
    }
}
