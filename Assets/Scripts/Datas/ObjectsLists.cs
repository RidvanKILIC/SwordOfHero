using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RK.GameDatas;
using System.Linq;

public class ObjectsLists : MonoBehaviour
{
    public static List<Weapon> swords = new List<Weapon>();
    public static List<Shield> shields = new List<Shield>();
    public static List<Armor> armors = new List<Armor>();
    public static List<Hair> hair = new List<Hair>();
    public static List<Housing> housings = new List<Housing>();
    public static List<Nation> nations = new List<Nation>();
    public static List<Vehicle> vehicles = new List<Vehicle>(); 
    private static ObjectsLists instance;
    public static ObjectsLists ObjInstance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("Save Load Manager's instance is null");
            }
            return instance;
        }
    }
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        if (instance == null)
        {
            instance = this;

        }
        else
        {
            Destroy(this.gameObject);
        }
        //fillLists();
    }
    public void fillLists()
    {
        swords.Clear();
        swords.AddRange(Resources.LoadAll<Weapon>("Weapons"));
        swords = swords.OrderBy(x => x.Cost).ToList();
        shields.Clear();
        shields.AddRange(Resources.LoadAll<Shield>("Shields"));
        shields = shields.OrderBy(x => x.Cost).ToList();
        armors.Clear();
        armors.AddRange(Resources.LoadAll<Armor>("Armors"));
        armors = armors.OrderBy(x => x.Cost).ToList();
        hair.Clear();
        hair.AddRange(Resources.LoadAll<Hair>("Hair"));
        housings.Clear();
        housings.AddRange(Resources.LoadAll<Housing>("Housings"));
        housings = housings.OrderBy(x => x.weaklyCost).ToList();
        nations.Clear();
        nations.AddRange(Resources.LoadAll<Nation>("Nations"));
        vehicles.Clear();
        vehicles.AddRange(Resources.LoadAll<Vehicle>("Vehicles"));
        //Debug.Log("Swords Cont:" +swords.Count);
        //Debug.Log("Shields Cont:" + shields.Count);
        //Debug.Log("Armors Cont:" + armors.Count);
        //Debug.Log("Hair Cont:" + hair.Count);
        //Debug.Log("Housing Cont:" + housings.Count);
        //Debug.Log("Nations Cont:" + nations.Count);
        //Debug.Log("Vehicles Cont:" + vehicles.Count);
        organizeLists();
    }
    public void organizeLists()
    {
        organizeArmorsLists();
        organizeHoungsLists();
        organizeShieldsLists();
        organizeSwordLists();
        organizeVehiclesLists();
    }
    public void organizeSwordLists()
    {
        foreach(var item in swords)
        {
            if (SaveLoad_Manager.gameData.playerInfos.BoughtswordList.Contains(item._name))
            {
                item.status = BaseItem.Status.Bought;
                if (SaveLoad_Manager.gameData.playerInfos.equippedSword==item._name)
                {
                    item.isEquipped = true;
                }
                else
                {
                    item.isEquipped = false;
                }
            }
            else
            {
                item.status = BaseItem.Status.Not_Bought;
                item.isEquipped = false;
            }
        }
    }

    public void organizeShieldsLists()
    {
        foreach (var item in shields)
        {
            if (SaveLoad_Manager.gameData.playerInfos.BoughtshieldList.Contains(item._name))
            {
                item.status = BaseItem.Status.Bought;
                if (SaveLoad_Manager.gameData.playerInfos.equippedShield == item._name)
                {
                    item.isEquipped = true;
                }
                else
                {
                    item.isEquipped = false;
                }
            }
            else
            {
                item.status = BaseItem.Status.Not_Bought;
                item.isEquipped = false;
            }
        }
    }
    public void organizeArmorsLists()
    {
        foreach (var item in armors)
        {
            if (SaveLoad_Manager.gameData.playerInfos.BoughtarmorList.Contains(item._name))
            {
                item.status = BaseItem.Status.Bought;
                if (SaveLoad_Manager.gameData.playerInfos.equippedArmor == item._name)
                {
                    item.isEquipped = true;
                }
                else
                {
                    item.isEquipped = false;
                }
            }
            else
            {
                item.status = BaseItem.Status.Not_Bought;
                item.isEquipped = false;
            }
        }
    }
    public void organizeHoungsLists()
    {
        foreach (var item in housings)
        {
            if (SaveLoad_Manager.gameData.playerInfos.BoughthousingList.Contains(item._name))
            {
                item.status = BaseItem.Status.Bought;
                if (SaveLoad_Manager.gameData.playerInfos.equippedHousing == item._name)
                {
                    item.isEquipped = true;
                }
                else
                {
                    item.isEquipped = false;
                }
            }
            else
            {
                item.status = BaseItem.Status.Not_Bought;
                item.isEquipped = false;
            }
        }
    }
    public void organizeVehiclesLists()
    {
        foreach (var item in vehicles)
        {
            if (SaveLoad_Manager.gameData.playerInfos.BoughtvehicleList.Contains(item._name))
            {
                item.status = BaseItem.Status.Bought;
                if (SaveLoad_Manager.gameData.playerInfos.equippedVehicle == item._name)
                {
                    item.isEquipped = true;
                }
                else
                {
                    item.isEquipped = false;
                }
            }
            else
            {
                item.status = BaseItem.Status.Not_Bought;
                item.isEquipped = false;
            }
        }
    }
}
