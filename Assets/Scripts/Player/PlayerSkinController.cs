using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using RK.Skeleton;
public class PlayerSkinController : MonoBehaviour
{
    [Header("Head Variables")]
    [SerializeField] GameObject Hair;
    [SerializeField] GameObject Beard;
    [SerializeField] GameObject BeardExtension;
    [SerializeField] GameObject Wound;
    [SerializeField] GameObject Mustache;
    [SerializeField] GameObject Helmet;
    [Header("BodyVariables")]
    [SerializeField] GameObject Body;
    [SerializeField] GameObject BodyArmor;
    [Header("ArmVariables")]
    [SerializeField] GameObject HandL;
    [SerializeField] GameObject HandR;
    [SerializeField] GameObject ArmL;
    [SerializeField] GameObject ArmR;
    [SerializeField] GameObject ShoulderL;
    [SerializeField] GameObject ShoulderR;
    [SerializeField] GameObject UpperArmL;
    [SerializeField] GameObject UpperArmR;
    [Header("Leg Variables")]
    [SerializeField] GameObject Pants;
    [SerializeField] GameObject Shoes;
    [Header("Weapons & Shield")]
    [SerializeField] GameObject Weapon;
    [SerializeField] GameObject Shield;
    public List<GameObject> playerPartsWithouWeapons = new List<GameObject>();
    public List<GameObject> playerParts = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Init()
    {
        setHeadSkin();
        setShieldSkin();
        setArmorSkin();
        setWeaponSkin();
    }
    public GameObject getWeapon()
    {
        return Weapon;
    }
    public GameObject getShield()
    {
        return Shield;
    }
    public void setHeadSkin()
    {
        if (!string.IsNullOrEmpty(SaveLoad_Manager.gameData.playerInfos.equippedHair))
        {
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
    public void setArmorSkin()
    {
        if (!string.IsNullOrEmpty(SaveLoad_Manager.gameData.playerInfos.equippedArmor))
        {
            Armor choosenArmor = ObjectsLists.armors.Find(x => x.name == SaveLoad_Manager.gameData.playerInfos.equippedArmor);
            if (choosenArmor != null)
            {
                if (!string.IsNullOrEmpty(choosenArmor.helmet))
                {
                    changeSkin.changeSkeletonSkin(Helmet.GetComponent<SkeletonAnimation>(), "Armor/" + choosenArmor._name + "/" + choosenArmor.helmet);
                    //Helmet.GetComponent<changeSkin>().changeSkeletonSkin("Armors/" + choosenArmor._name + "/" + choosenArmor.helmet);
                }
                else
                {
                    Helmet.SetActive(false);
                }
                if (!string.IsNullOrEmpty(choosenArmor.body))
                {
                    changeSkin.changeSkeletonSkin(BodyArmor.GetComponent<SkeletonAnimation>(), "Armor/" + choosenArmor._name + "/" + choosenArmor.body);
                    //BodyArmor.GetComponent<changeSkin>().changeSkeletonSkin("Armors/" + choosenArmor._name + "/" + choosenArmor.body);
                }
                else
                {
                    BodyArmor.SetActive(false);
                }
                if (!string.IsNullOrEmpty(choosenArmor.handL))
                {
                    changeSkin.changeSkeletonSkin(HandL.GetComponent<SkeletonAnimation>(), "Armor/" + choosenArmor._name + "/" + choosenArmor.handL);
                    //HandL.GetComponent<changeSkin>().changeSkeletonSkin("Armors/" + choosenArmor._name + "/" + choosenArmor.handL);
                }
                else
                {
                    changeSkin.changeSkeletonSkin(HandL.GetComponent<SkeletonAnimation>(), "Armor/" + "Adventure_Armor/Adventure_Armor_Glove_L");
                    //Debug.Log("Default Hand");
                    //HandL.SetActive(false);
                }
                if (!string.IsNullOrEmpty(choosenArmor.handR))
                {
                    changeSkin.changeSkeletonSkin(HandR.GetComponent<SkeletonAnimation>(), "Armor/" + choosenArmor._name + "/" + choosenArmor.handR);
                    //HandR.GetComponent<changeSkin>().changeSkeletonSkin("Armor/" + choosenArmor._name + "/" + choosenArmor.handR);
                }
                else
                {
                    changeSkin.changeSkeletonSkin(HandR.GetComponent<SkeletonAnimation>(), "Armor/" + "Adventure_Armor/Adventure_Armor_Glove_R");
                    //Debug.Log("Default Hand");
                    //HandR.SetActive(false);
                }
                if (!string.IsNullOrEmpty(choosenArmor.armL))
                {
                    changeSkin.changeSkeletonSkin(ArmL.GetComponent<SkeletonAnimation>(), "Armor/" + choosenArmor._name + "/" + choosenArmor.armL);
                    //ArmL.GetComponent<changeSkin>().changeSkeletonSkin("Armor/" + choosenArmor._name + "/" + choosenArmor.armL);
                }
                else
                {
                    //Debug.Log("Default Hand");
                    ArmL.SetActive(false);
                }
                if (!string.IsNullOrEmpty(choosenArmor.armR))
                {
                    changeSkin.changeSkeletonSkin(ArmR.GetComponent<SkeletonAnimation>(), "Armor/" + choosenArmor._name + "/" + choosenArmor.armR);
                    //ArmR.GetComponent<changeSkin>().changeSkeletonSkin("Armor/" + choosenArmor._name + "/" + choosenArmor.armR);
                }
                else
                {
                    //Debug.Log("Default Hand");
                    ArmR.SetActive(false);
                }
                if (!string.IsNullOrEmpty(choosenArmor.shoulderL))
                {
                    changeSkin.changeSkeletonSkin(ShoulderL.GetComponent<SkeletonAnimation>(), "Armor/" + choosenArmor._name + "/" + choosenArmor.shoulderL);
                    //ShoulderL.GetComponent<changeSkin>().changeSkeletonSkin("Armor/" + choosenArmor._name + "/" + choosenArmor.shoulderL);
                }
                else
                {
                    ShoulderL.SetActive(false);
                }
                if (!string.IsNullOrEmpty(choosenArmor.shoulderR))
                {
                    changeSkin.changeSkeletonSkin(ShoulderR.GetComponent<SkeletonAnimation>(), "Armor/" + choosenArmor._name + "/" + choosenArmor.shoulderR);
                    //ShoulderR.GetComponent<changeSkin>().changeSkeletonSkin("Armor/"+ choosenArmor._name+"/"+ choosenArmor.shoulderR);
                }
                else
                {
                    ShoulderR.SetActive(false);
                }
                if (!string.IsNullOrEmpty(choosenArmor.upperArmL))
                {
                    changeSkin.changeSkeletonSkin(UpperArmR.GetComponent<SkeletonAnimation>(), "Armor/" + choosenArmor._name + "/" + choosenArmor.upperArmL);
                    //UpperArmL.GetComponent<changeSkin>().changeSkeletonSkin("Armor/" + choosenArmor._name + "/" + choosenArmor.upperArmL);
                }
                else
                {
                    UpperArmL.SetActive(false);
                }
                if (!string.IsNullOrEmpty(choosenArmor.upperArmR))
                {
                    changeSkin.changeSkeletonSkin(UpperArmR.GetComponent<SkeletonAnimation>(), "Armor/" + choosenArmor._name + "/" + choosenArmor.upperArmR);
                    //UpperArmR.GetComponent<changeSkin>().changeSkeletonSkin("Armor/" + choosenArmor._name + "/" + choosenArmor.upperArmR);
                }
                else
                {
                    UpperArmR.SetActive(false);
                }
                if (!string.IsNullOrEmpty(choosenArmor.pants))
                {
                    changeSkin.changeSkeletonSkin(Pants.GetComponent<SkeletonAnimation>(), "Armor/" + choosenArmor._name + "/" + choosenArmor.pants);
                    //Pants.GetComponent<changeSkin>().changeSkeletonSkin("Armor/" + choosenArmor._name + "/" + choosenArmor.pants);
                }
                else
                {
                    changeSkin.changeSkeletonSkin(Pants.GetComponent<SkeletonAnimation>(), "Armor/" + "Adventure_Armor/Adventure_Armor_Pants");
                    //Pants.SetActive(false);
                }
                if (!string.IsNullOrEmpty(choosenArmor.shoes))
                {
                    changeSkin.changeSkeletonSkin(Shoes.GetComponent<SkeletonAnimation>(), "Armor/" + choosenArmor._name + "/" + choosenArmor.shoes);
                    //Shoes.GetComponent<changeSkin>().changeSkeletonSkin("Armor/" + choosenArmor._name + "/" + choosenArmor.shoes);
                }
                else
                {
                    Shoes.SetActive(false);
                }
            }
        }
        else
        {
            Helmet.SetActive(false);
            BodyArmor.SetActive(false);
            changeSkin.changeSkeletonSkin(HandL.GetComponent<SkeletonAnimation>(), "Armor/" + "Adventure_Armor/Adventure_Armor_Glove_L");
            changeSkin.changeSkeletonSkin(HandR.GetComponent<SkeletonAnimation>(), "Armor/" + "Adventure_Armor/Adventure_Armor_Glove_R");
            ArmL.SetActive(false);
            ArmR.SetActive(false);
            ShoulderL.SetActive(false);
            ShoulderR.SetActive(false);
            UpperArmL.SetActive(false);
            UpperArmR.SetActive(false);
            changeSkin.changeSkeletonSkin(Pants.GetComponent<SkeletonAnimation>(), "Armor/" + "Adventure_Armor/Adventure_Armor_Pants");
            Shoes.SetActive(false);
        }
        
    }
    public void setWeaponSkin()
    {
        //Debug.Log(SaveLoad_Manager.gameData.playerInfos.equippedSword);
        if (!string.IsNullOrEmpty(SaveLoad_Manager.gameData.playerInfos.equippedSword))
        {
            Weapon chooseWeapon = ObjectsLists.swords.Find(x => x._name == SaveLoad_Manager.gameData.playerInfos.equippedSword);
            if (chooseWeapon != null)
            {
                if (!string.IsNullOrEmpty(chooseWeapon._name))
                {
                    //Debug.Log("Weapon / " + chooseWeapon._weaponType.ToString() + " / " + chooseWeapon._name);
                    changeSkin.changeSkeletonSkin(Weapon.GetComponent<SkeletonAnimation>(), "Weapon/" + chooseWeapon._weaponType.ToString() + "/" + chooseWeapon._name);
                    //Weapon.GetComponent<changeSkin>().changeSkeletonSkin("Weapon/"+chooseWeapon._weaponType.ToString()+"/" + chooseWeapon._name);
                }
                else
                {
                    Weapon.SetActive(false);
                }
            }
        }
        else
        {
            Weapon.SetActive(false);
        }

    }
    public void setShieldSkin()
    {
        if (!string.IsNullOrEmpty(SaveLoad_Manager.gameData.playerInfos.equippedShield))
        {
            Shield choosenShield = ObjectsLists.shields.Find(x => x.name == SaveLoad_Manager.gameData.playerInfos.equippedShield);
            if (choosenShield != null)
            {
                if (!string.IsNullOrEmpty(choosenShield._name))
                {
                    changeSkin.changeSkeletonSkin(Shield.GetComponent<SkeletonAnimation>(), "Shields/" + choosenShield._name);
                    //Shield.GetComponent<changeSkin>().changeSkeletonSkin("Shields/" + choosenShield._name);
                }
                else
                {
                    Shield.SetActive(false);
                }
            }
        }
        else
        {
            Shield.SetActive(false);
        }
    }
}
