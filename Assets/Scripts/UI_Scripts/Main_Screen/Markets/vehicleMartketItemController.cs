using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class vehicleMartketItemController : MonoBehaviour
{
    [Header("Button Variables")]
    [SerializeField] GameObject BuyButton;
    [SerializeField] Image buttonImageSlot;
    [SerializeField] LocalizeStringEvent ButtonString;
    [Header("Description Variavles")]
    [SerializeField] setMarketItemsInfoTextsForString _nameText;
    [SerializeField] setMarketItemsInfoTextsForString _typeText;
    [SerializeField] TMP_Text _enduranceText;
    [SerializeField] setMarketItemsInfoTextsForInt _priceText;

    [Header("Icon Variables")]
    [SerializeField] Image iconImageSlot;
    [SerializeField] Image lockImage;
    [Header("Resources")]
    [SerializeField] Sprite buyImage;
    [SerializeField] Sprite equippedImage;
    [SerializeField] Sprite releaseImage;
    Vehicle vehicle;
    // Start is called before the first frame update
    void Awake()
    {
        BuyButton.GetComponent<Button>().onClick.AddListener(() => BuyWeapon());
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void setVehicle(Vehicle _vehicle)
    {
        vehicle = null;
        vehicle = _vehicle;
        setObject();
    }
    public void setObject()
    {
        if (vehicle.isLock)
        {
            lockImage.gameObject.SetActive(true);
            BuyButton.GetComponent<Button>().interactable = false;
        }
            
        string _key = vehicle._name;
        //string _vehicleTypeKey = "vehicle_transport_type_" + vehicle._transportType.ToString();
        //string _spacialtyKey = "vehicle_transport_spacialty_" + vehicle.spacialty.ToString();
        //Debug.Log(_vehicleTypeKey);
        //Debug.Log(_spacialtyKey);
        _nameText.setText("Vehicles_Names", _key + "_Text");
        //_typeText.setText("vehicle_Properties", _vehicleTypeKey + "_Text");
        //_spacialtyText.setText("vehicle_Properties", _spacialtyKey + "_Text");
        _priceText.setText(vehicle.weaklyCost);
        _enduranceText.text=(vehicle.endurance.ToString());
        iconImageSlot.overrideSprite = vehicle.sprite;
        if (vehicle.status.Equals(BaseItem.Status.Bought))
        {
            if (vehicle.isEquipped)
            {
                buttonImageSlot.sprite = releaseImage;
                ButtonString.StringReference.SetReference("marketItem_Texts", "itemButton_Text_Release");
            }
            else
            {
                buttonImageSlot.sprite = equippedImage;
                ButtonString.StringReference.SetReference("marketItem_Texts", "itemButton_Text_Equip");
            }
        }
        else
        {
            buttonImageSlot.sprite = buyImage;
            ButtonString.StringReference.SetReference("marketItem_Texts", "itemButton_Text_Buy");
        }
    }
    public void updateButtonText()
    {
        if (vehicle.status.Equals(BaseItem.Status.Bought))
        {
            if (vehicle.isEquipped)
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
    }

    public void BuyWeapon()
    {
        if (!string.IsNullOrEmpty(SaveLoad_Manager.gameData.playerInfos.equippedVehicle))
        {
            //Debug.Log("itemList count " + WeaponaryMarketController.marketHousingItemLists.Count);
            var exVehicle = GameObject.FindGameObjectWithTag("Managements").GetComponent<WeaponaryMarketController>().marketVehicleItemLists.Find(x => x.GetComponent<vehicleMartketItemController>().vehicle._name == SaveLoad_Manager.gameData.playerInfos.equippedVehicle);
            if (exVehicle != null)
            {
                //Debug.Log("Ex Sword" + exHousing._name);
                exVehicle.GetComponent<vehicleMartketItemController>().vehicle.isEquipped = false;
                exVehicle.GetComponent<vehicleMartketItemController>().updateButtonText();
            }

        }
        vehicle.status = BaseItem.Status.Bought;
        vehicle.isEquipped = true;
        SaveLoad_Manager.gameData.playerInfos.equippedVehicle = vehicle._name;
        SaveLoad_Manager.gameData.playerInfos.BoughtvehicleList.Add(vehicle._name);
        updateButtonText();
    }

    public void equipWeapon()
    {

        if (SaveLoad_Manager.gameData.playerInfos.BoughtvehicleList.Contains(vehicle._name))
        {
            if (!string.IsNullOrEmpty(SaveLoad_Manager.gameData.playerInfos.equippedVehicle))
            {
                //Debug.Log("itemList count " + WeaponaryMarketController.marketVehicleItemLists.Count);
                var exVehicle = GameObject.FindGameObjectWithTag("Managements").GetComponent<WeaponaryMarketController>().marketVehicleItemLists.Find(x => x.GetComponent<vehicleMartketItemController>().vehicle._name == SaveLoad_Manager.gameData.playerInfos.equippedVehicle);
                if (exVehicle != null)
                {
                    //Debug.Log("Ex Sword" + exSword.name);
                    exVehicle.GetComponent<vehicleMartketItemController>().vehicle.isEquipped = false;
                    exVehicle.GetComponent<vehicleMartketItemController>().updateButtonText();
                }

            }
            SaveLoad_Manager.gameData.playerInfos.equippedVehicle = vehicle._name;
            vehicle.isEquipped = true;
        }
        updateButtonText();
    }

    public void releaseWeapon()
    {
        if (SaveLoad_Manager.gameData.playerInfos.equippedVehicle == vehicle._name)
        {
            //Debug.Log(SaveLoad_Manager.gameData.playerInfos.equippedSword);
            SaveLoad_Manager.gameData.playerInfos.equippedVehicle = "";
        }
        vehicle.isEquipped = false;
        updateButtonText();
    }
}
