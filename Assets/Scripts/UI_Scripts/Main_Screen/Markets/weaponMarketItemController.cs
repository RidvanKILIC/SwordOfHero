using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class weaponMarketItemController : MonoBehaviour
{
    [Header("Button Variables")]
    [SerializeField] GameObject BuyButton;
    [SerializeField] Image buttonImageSlot;
    [SerializeField] LocalizeStringEvent ButtonString;
    [Header("Description Variavles")]
    [SerializeField] setMarketItemsInfoTextsForString _nameText;
    [SerializeField] setMarketItemsInfoTextsForInt _attackText;
    [SerializeField] setMarketItemsInfoTextsForInt _priceText;
    [Header("Icon Variables")]
    [SerializeField] Image iconImageSlot;
    [SerializeField] Image lockImage;
    [Header("Resources")]
    [SerializeField] Sprite buyImage;
    [SerializeField] Sprite equippedImage;
    [SerializeField] Sprite releaseImage;
    Weapon weapon;
    // Start is called before the first frame update
    void Awake()
    {
        //BuyButton.GetComponent<Button>().onClick.AddListener(()=> BuyWeapon());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void setWeapon(Weapon _weapon)
    {
        weapon = null;
        weapon = _weapon;
        setObject();
    }
    public void setObject() 
    {
        if (weapon.isLock)
        {
            lockImage.gameObject.SetActive(true);
            BuyButton.GetComponent<Button>().interactable = false;
        }
        if (SaveLoad_Manager.gameData.playerInfos.BoughtswordList.Contains(weapon._name))
        {
            weapon.status = weapon.status = BaseItem.Status.Bought;
            if (SaveLoad_Manager.gameData.playerInfos.equippedSword == weapon._name)
            {
                weapon.isEquipped = true;
            }
        }
        string _key = weapon._name;
        _nameText.setText("Weapon_Names",_key+"_Text");
        _attackText.setText(weapon.attack);
        _priceText.setText(weapon.Cost);
        iconImageSlot.overrideSprite = weapon.sprite;
        if (weapon.status.Equals(BaseItem.Status.Bought))
        {
            if (weapon.isEquipped)
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
        if (weapon.status.Equals(BaseItem.Status.Bought))
        {
            if (weapon.isEquipped)
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
        //Debug.Log("Player Gold: " + SaveLoad_Manager.gameData.playerInfos.playerGoldCount + "Cost: " + weapon.Cost);
        if (SaveLoad_Manager.gameData.playerInfos.playerGoldCount >= weapon.Cost)
        {
            if (!string.IsNullOrEmpty(SaveLoad_Manager.gameData.playerInfos.equippedSword))
            {
                //Debug.Log("itemList count " + WeaponaryMarketController.marketWeaponItemLists.Count);
                var exSword = GameObject.FindGameObjectWithTag("Managements").GetComponent<WeaponaryMarketController>().marketWeaponItemLists.Find(x => x.GetComponent<weaponMarketItemController>().weapon._name == SaveLoad_Manager.gameData.playerInfos.equippedSword);
                if (exSword != null)
                {
                    //Debug.Log("Ex Sword" + exSword.name);
                    SaveLoad_Manager.gameData.playerInfos.playerBaseAttack -= exSword.GetComponent<weaponMarketItemController>().weapon.attack;
                    exSword.GetComponent<weaponMarketItemController>().weapon.isEquipped = false;
                    exSword.GetComponent<weaponMarketItemController>().updateButtonText();
                }

            }
            weapon.status = BaseItem.Status.Bought;
            weapon.isEquipped = true;
            SaveLoad_Manager.gameData.playerInfos.equippedSword = weapon._name;
            if (!SaveLoad_Manager.gameData.playerInfos.BoughtswordList.Contains(weapon._name))
            {
                SaveLoad_Manager.gameData.playerInfos.BoughtswordList.Add(weapon._name);
                SaveLoad_Manager.gameData.playerInfos.playerGoldCount -= weapon.Cost;
                SaveLoad_Manager.gameData.playerInfos.playerBaseAttack += weapon.attack;

                GameObject.FindGameObjectWithTag("Managements").GetComponent<mainMenu_Controller>().updateGold();
                GameObject.FindGameObjectWithTag("Managements").GetComponent<WeaponaryMarketController>().updateGoldTexts();
            }
            if (!SaveLoad_Manager.gameData.isArmoryEquipped)
            {
                tutorialController.TInstance.activateShieldArrow(GameObject.FindGameObjectWithTag("Managements").gameObject);
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
        SaveLoad_Manager.SInstance.saveJson();
    }
    public void equipWeapon()
    {
        SoundManager.SInstance.playSfx(SoundManager.SInstance.wearSFX);
        if (SaveLoad_Manager.gameData.playerInfos.BoughtswordList.Contains(weapon._name))
        {
            if (!string.IsNullOrEmpty(SaveLoad_Manager.gameData.playerInfos.equippedSword))
            {
                //Debug.Log("itemList count "+WeaponaryMarketController.marketWeaponItemLists.Count);
                var exSword = GameObject.FindGameObjectWithTag("Managements").GetComponent<WeaponaryMarketController>().marketWeaponItemLists.Find(x => x.GetComponent<weaponMarketItemController>().weapon._name == SaveLoad_Manager.gameData.playerInfos.equippedSword);
                if (exSword != null)
                {
                    //Debug.Log("Ex Sword" + exSword.name);
                    SaveLoad_Manager.gameData.playerInfos.playerBaseAttack -= exSword.GetComponent<weaponMarketItemController>().weapon.attack;
                    exSword.GetComponent<weaponMarketItemController>().weapon.isEquipped = false;
                    exSword.GetComponent<weaponMarketItemController>().updateButtonText();
                }

            }
            SaveLoad_Manager.gameData.playerInfos.equippedSword = weapon._name;
            SaveLoad_Manager.gameData.playerInfos.playerBaseAttack += weapon.attack;
            weapon.isEquipped = true;
        }
        updateButtonText();
    }

    public void releaseWeapon()
    {
        SoundManager.SInstance.playSfx(SoundManager.SInstance.wearSFX);
        if (SaveLoad_Manager.gameData.playerInfos.equippedSword == weapon._name)
        {
            //Debug.Log(SaveLoad_Manager.gameData.playerInfos.equippedSword);
            SaveLoad_Manager.gameData.playerInfos.playerBaseAttack -= weapon.attack;
            SaveLoad_Manager.gameData.playerInfos.equippedSword = string.Empty;
        }
        weapon.isEquipped = false;
        updateButtonText();
    }
}
