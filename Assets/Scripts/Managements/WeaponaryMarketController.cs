using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RK.GameDatas;
using TMPro;

public class WeaponaryMarketController : MonoBehaviour
{
    [Header("Transport Varialbles")]
    [SerializeField] GameObject vehicleMarketContent;
    [SerializeField] GameObject marketVehiclePrafab;
    [SerializeField] public List<GameObject> marketVehicleItemLists = new List<GameObject>();
    public static bool isVehicleFilled = false;
    [Header("Housing Varialbles")]
    [SerializeField] GameObject housingMarketContent;
    [SerializeField] GameObject marketHousingPrafab;
    [SerializeField] public List<GameObject> marketHousingItemLists = new List<GameObject>();
    public static bool isHousingFilled = false;
    [Header("Weapon Varialbles")]
    [SerializeField] GameObject weaponMarketContent;
    [SerializeField] GameObject marketWeopanPrafab;
    [SerializeField] public List<GameObject> marketWeaponItemLists = new List<GameObject>();
    public static bool isWeaponFilled = false;
    [Header("Shield Varialbles")]
    [SerializeField] GameObject shieldMarketContent;
    [SerializeField] GameObject marketShieldPrafab;
    [SerializeField] public List<GameObject> marketShieldItemLists = new List<GameObject>();
    public static bool isShieldFilled = false;
    [Header("Armor Varialbles")]
    [SerializeField] GameObject armorMarketContent;
    [SerializeField] GameObject marketArmorPrafab;
    [SerializeField] public List<GameObject> marketArmorItemLists = new List<GameObject>();
    public static bool isArmorFilled = false;
    [Header("Others")]
    [SerializeField] List<TMP_Text> marketGoldTexts = new List<TMP_Text>();
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.SInstance.adjustTheme();
        //marketVehicleItemLists = new List<GameObject>();
        //marketVehicleItemLists = new List<GameObject>();
        //marketWeaponItemLists = new List<GameObject>();
        //marketShieldItemLists = new List<GameObject>();
        //marketArmorItemLists = new List<GameObject>();

        fillMarketItemLists();
        updateGoldTexts();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void fillMarketItemLists()
    {
        foreach (var item in ObjectsLists.swords)
        {
           if (marketWeaponItemLists.Count == ObjectsLists.swords.Count)
                isWeaponFilled = true;

          GameObject obj = Instantiate(marketWeopanPrafab, weaponMarketContent.transform.position, Quaternion.identity);
          obj.transform.parent = weaponMarketContent.transform;
          obj.transform.localScale = Vector3.one;
          obj.GetComponent<weaponMarketItemController>().setWeapon(item);

          //if(!isWeaponFilled)
            marketWeaponItemLists.Add(obj);
        }

            foreach (var item in ObjectsLists.shields)
            {
            if (marketShieldItemLists.Count == ObjectsLists.shields.Count)
                isShieldFilled = true;

                //Debug.Log(item.name);
                GameObject obj = Instantiate(marketShieldPrafab, shieldMarketContent.transform.position, Quaternion.identity);
                obj.transform.parent = shieldMarketContent.transform;
                obj.transform.localScale = Vector3.one;
                obj.GetComponent<shieldMarketIItemController>().setShield(item);
            //if(!isShieldFilled)
                marketShieldItemLists.Add(obj);
            }


   
            foreach (var item in ObjectsLists.armors)
            {
            if (marketArmorItemLists.Count == ObjectsLists.armors.Count)
                isArmorFilled = true;
                //Debug.Log(item.name);
                GameObject obj = Instantiate(marketArmorPrafab, armorMarketContent.transform.position, Quaternion.identity);
                obj.transform.parent = armorMarketContent.transform;
                obj.transform.localScale = Vector3.one;
                obj.GetComponent<armorMarketItemController>().setArmor(item);
            //if(!isArmorFilled)
                marketArmorItemLists.Add(obj);
            }


            foreach (var item in ObjectsLists.housings)
            {
                if (marketHousingItemLists.Count == ObjectsLists.housings.Count)
                    isHousingFilled = true;
                //Debug.Log(item.name);
                GameObject obj = Instantiate(marketHousingPrafab, housingMarketContent.transform.position, Quaternion.identity);
                obj.transform.parent = housingMarketContent.transform;
                obj.transform.localScale = Vector3.one;
                obj.GetComponent<housingMarketItemController>().setHousing(item);
                //if(!isHousingFilled)
                    marketHousingItemLists.Add(obj);
            }

    }
    public void updateGoldTexts()
    {
        foreach(var item in marketGoldTexts)
        {
            item.text = SaveLoad_Manager.gameData.playerInfos.playerGoldCount.ToString();
        }
    }
}
