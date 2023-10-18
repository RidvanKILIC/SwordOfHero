using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class housingMarketItemController : MonoBehaviour
{
    [Header("Button Variables")]
    [SerializeField] GameObject BuyButton;
    [SerializeField] Image buttonImageSlot;
    [SerializeField] LocalizeStringEvent ButtonString;
    [Header("Description Variavles")]
    [SerializeField] setMarketItemsInfoTextsForString _nameText;
    [SerializeField] setMarketItemsInfoTextsForString _statusText;
    [SerializeField] setMarketItemsInfoTextsForInt _priceText;
    [Header("Icon Variables")]
    [SerializeField] Image iconImageSlot;
    [SerializeField] Image lockImage;
    [Header("Resources")]
    [SerializeField] Sprite buyImage;
    [SerializeField] Sprite equippedImage;
    [SerializeField] Sprite releaseImage;
    Housing housing;
    // Start is called before the first frame update
    void Awake()
    {
        //BuyButton.GetComponent<Button>().onClick.AddListener(() => BuyWeapon());
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void setHousing(Housing _housing)
    {
        housing = null;
        housing = _housing;
        setObject();
    }
    public void setObject()
    {
        if (housing.isLock)
        {
            lockImage.gameObject.SetActive(true);
            BuyButton.GetComponent<Button>().interactable = false;
        }
        if (SaveLoad_Manager.gameData.playerInfos.BoughthousingList.Contains(housing._name))
        {
            housing.status = housing.status = BaseItem.Status.Bought;
            if (SaveLoad_Manager.gameData.playerInfos.equippedHousing == housing._name)
            {
                housing.isEquipped = true;
            }
        }
        string _key = housing._name;
        //string _fameKey = "housing_fame_Noble";
        //Debug.Log(_fameKey + "_Text");
        _nameText.setText("Housing_Names", _key + "_Text");
        //_statusText.setText("housing_Properties", _fameKey + "_Text");
        _priceText.setText(housing.weaklyCost);
        iconImageSlot.overrideSprite = housing.sprite;
        if (housing.status.Equals(BaseItem.Status.Bought))
        {
            if (housing.isEquipped)
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
        if (housing.status.Equals(BaseItem.Status.Bought))
        {
            if (housing.isEquipped)
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
        //Debug.Log("Player Money: " + SaveLoad_Manager.gameData.playerInfos.playerGoldCount + " Housing Cost: " + housing.Cost);
        if (SaveLoad_Manager.gameData.playerInfos.playerGoldCount >= housing.weaklyCost)
        {
            if (!string.IsNullOrEmpty(SaveLoad_Manager.gameData.playerInfos.equippedHousing))
            {
                //Debug.Log("itemList count " + WeaponaryMarketController.marketHousingItemLists.Count);
                var exHousing = GameObject.FindGameObjectWithTag("Managements").GetComponent<WeaponaryMarketController>().marketHousingItemLists.Find(x => x.GetComponent<housingMarketItemController>().housing._name == SaveLoad_Manager.gameData.playerInfos.equippedHousing);
                if (exHousing != null)
                {
                    //Debug.Log("Ex Sword" + exHousing._name);
                    exHousing.GetComponent<housingMarketItemController>().housing.isEquipped = false;
                    exHousing.GetComponent<housingMarketItemController>().updateButtonText();
                }

            }
            housing.status = BaseItem.Status.Bought;
            housing.isEquipped = true;
            SaveLoad_Manager.gameData.playerInfos.equippedHousing = housing._name;
            if (!SaveLoad_Manager.gameData.playerInfos.BoughthousingList.Contains(housing._name))
            {
                SaveLoad_Manager.gameData.playerInfos.BoughthousingList.Add(housing._name);
                SaveLoad_Manager.gameData.playerInfos.playerGoldCount -= housing.weaklyCost;
                GameObject.FindGameObjectWithTag("Managements").GetComponent<mainMenu_Controller>().updateGold();
                GameObject.FindGameObjectWithTag("Managements").GetComponent<WeaponaryMarketController>().updateGoldTexts();
            }
            if (!SaveLoad_Manager.gameData.isHousingBought)
            {
                SaveLoad_Manager.gameData.isHousingBought = true;
                tutorialController.TInstance.activateHousingHomeArrow(GameObject.FindGameObjectWithTag("Managements").gameObject);
            }
            

            updateButtonText();
        }
        else
        {
            GameObject.FindGameObjectWithTag("Managements").GetComponent<mainMenu_Controller>().activateHousingWarningPanel();
        }

    }

    public void equipWeapon()
    {
        SoundManager.SInstance.playSfx(SoundManager.SInstance.wearSFX);
        if (SaveLoad_Manager.gameData.playerInfos.BoughthousingList.Contains(housing._name))
        {
            if (!string.IsNullOrEmpty(SaveLoad_Manager.gameData.playerInfos.equippedHousing))
            {
                //Debug.Log("itemList count " + GameObject.FindGameObjectWithTag("Managements").GetComponent<WeaponaryMarketController>().marketHousingItemLists.Count+"Equipped Housing:"+ SaveLoad_Manager.gameData.playerInfos.equippedHousing);
                var exSword = GameObject.FindGameObjectWithTag("Managements").GetComponent<WeaponaryMarketController>().marketHousingItemLists.Find(x => x.GetComponent<housingMarketItemController>().housing._name == SaveLoad_Manager.gameData.playerInfos.equippedHousing);
                if (exSword != null)
                {
                    //Debug.Log("Ex Sword" + exSword.name);
                    exSword.GetComponent<housingMarketItemController>().housing.isEquipped = false;
                    exSword.GetComponent<housingMarketItemController>().updateButtonText();
                }

            }
            SaveLoad_Manager.gameData.playerInfos.equippedHousing = housing._name;
            housing.isEquipped = true;
        }
        updateButtonText();
    }
    public void wearItem()
    {
        GameObject.FindGameObjectWithTag("Managements").GetComponent<mainMenu_Controller>().updateHousingBG();
        SaveLoad_Manager.SInstance.saveJson();
    }
    public void releaseWeapon()
    {
        SoundManager.SInstance.playSfx(SoundManager.SInstance.wearSFX);
        if (SaveLoad_Manager.gameData.playerInfos.equippedHousing == housing._name)
        {
            //Debug.Log(SaveLoad_Manager.gameData.playerInfos.equippedSword);
            SaveLoad_Manager.gameData.playerInfos.equippedHousing =string.Empty;
        }
        housing.isEquipped = false;
        updateButtonText();
    }
}
