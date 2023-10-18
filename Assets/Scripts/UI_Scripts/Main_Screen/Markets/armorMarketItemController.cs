using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class armorMarketItemController : MonoBehaviour
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
    Armor armor;
    // Start is called before the first frame update
    void Awake()
    {
        //BuyButton.GetComponent<Button>().onClick.AddListener(() => BuyWeapon());
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void setArmor(Armor _armor)
    {
        armor = null;
        armor = _armor;
        setObject();
    }
    public void setObject()
    {
        if (armor.isLock)
        {
            lockImage.gameObject.SetActive(true);
            BuyButton.GetComponent<Button>().interactable = false;
        }
        string _key = armor._name;
        _nameText.setText("Armor_Names", _key + "_Text");
        _defenseText.setText(armor.defense);
        _priceText.setText(armor.Cost);
        iconImageSlot.overrideSprite = armor.sprite;
        if (SaveLoad_Manager.gameData.playerInfos.BoughtarmorList.Contains(armor._name))
        {
            armor.status = armor.status = BaseItem.Status.Bought;
            if(SaveLoad_Manager.gameData.playerInfos.equippedArmor == armor._name)
            {
                armor.isEquipped = true;
            }
        }


        if (armor.status.Equals(BaseItem.Status.Bought))
        {
            if (armor.isEquipped)
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
        if (armor.status.Equals(BaseItem.Status.Bought))
        {
            if (armor.isEquipped)
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
        if (SaveLoad_Manager.gameData.playerInfos.playerGoldCount >= armor.Cost)
        {
            if (!string.IsNullOrEmpty(SaveLoad_Manager.gameData.playerInfos.equippedArmor))
            {
                var exArmor = GameObject.FindGameObjectWithTag("Managements").GetComponent<WeaponaryMarketController>().marketArmorItemLists.Find(x => x.GetComponent<armorMarketItemController>().armor._name == SaveLoad_Manager.gameData.playerInfos.equippedArmor);
                if (exArmor != null)
                {
                    SaveLoad_Manager.gameData.playerInfos.playerBaseShield -= exArmor.GetComponent<armorMarketItemController>().armor.defense;
                    exArmor.GetComponent<armorMarketItemController>().armor.isEquipped = false;
                    exArmor.GetComponent<armorMarketItemController>().updateButtonText();
                }

            }
            armor.status = BaseItem.Status.Bought;
            armor.isEquipped = true;
            SaveLoad_Manager.gameData.playerInfos.equippedArmor = armor._name;
            //Debug.Log(armor._name + " equipped");
            if (!SaveLoad_Manager.gameData.playerInfos.BoughtarmorList.Contains(armor._name))
            {
                SaveLoad_Manager.gameData.playerInfos.BoughtarmorList.Add(armor._name);
                SaveLoad_Manager.gameData.playerInfos.playerGoldCount -= armor.Cost;
                SaveLoad_Manager.gameData.playerInfos.playerBaseShield += armor.defense;
                GameObject.FindGameObjectWithTag("Managements").GetComponent<mainMenu_Controller>().updateShieldValue();
                GameObject.FindGameObjectWithTag("Managements").GetComponent<mainMenu_Controller>().updateGold();
                GameObject.FindGameObjectWithTag("Managements").GetComponent<WeaponaryMarketController>().updateGoldTexts();
            }
            if (!SaveLoad_Manager.gameData.isArmoryEquipped)
            {
                tutorialController.TInstance.deActivateArmorArrow();
                SaveLoad_Manager.gameData.isArmoryEquipped = true;
                tutorialController.TInstance.ActivateHomeArrow(GameObject.FindGameObjectWithTag("Managements").gameObject);
            }
            updateButtonText();
        }
        else
        {
            GameObject.FindGameObjectWithTag("Managements").GetComponent<mainMenu_Controller>().activateMArketWarningPanel();
        }

    }
    public void wearItem()
    {
        GameObject.FindGameObjectWithTag("Managements").GetComponent<mainMenu_Controller>().updatePlayer();
        GameObject.FindGameObjectWithTag("Managements").GetComponent<mainMenu_Controller>().updateShieldValue();
        SaveLoad_Manager.SInstance.saveJson();
    }

    public void equipWeapon()
    {
        SoundManager.SInstance.playSfx(SoundManager.SInstance.wearSFX);
        if (SaveLoad_Manager.gameData.playerInfos.BoughtarmorList.Contains(armor._name))
        {
            if (!string.IsNullOrEmpty(SaveLoad_Manager.gameData.playerInfos.equippedArmor))
            {
                var exArmor = GameObject.FindGameObjectWithTag("Managements").GetComponent<WeaponaryMarketController>().marketArmorItemLists.Find(x => x.GetComponent<armorMarketItemController>().armor._name == SaveLoad_Manager.gameData.playerInfos.equippedArmor);
                if (exArmor != null)
                {
                    SaveLoad_Manager.gameData.playerInfos.playerBaseShield -= exArmor.GetComponent<armorMarketItemController>().armor.defense;
                    exArmor.GetComponent<armorMarketItemController>().armor.isEquipped = false;
                    exArmor.GetComponent<armorMarketItemController>().updateButtonText();
                }

            }
            SaveLoad_Manager.gameData.playerInfos.playerBaseShield += armor.defense;
            SaveLoad_Manager.gameData.playerInfos.equippedArmor = armor._name;
            armor.isEquipped = true;
        }
        updateButtonText();
    }

    public void releaseWeapon()
    {
        SoundManager.SInstance.playSfx(SoundManager.SInstance.wearSFX);
        if (SaveLoad_Manager.gameData.playerInfos.equippedArmor == armor._name)
        {
            SaveLoad_Manager.gameData.playerInfos.playerBaseShield -= armor.defense;
            SaveLoad_Manager.gameData.playerInfos.equippedArmor =string.Empty;
        }
        armor.isEquipped = false;
        updateButtonText();
    }
}
