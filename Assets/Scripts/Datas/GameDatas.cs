using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RK.GameDatas
{
    public class GameDatas
    {
        #region Tutorial Variables
        public bool isFirstPlay = true;
        public bool isApeperanceSet = false;
        public bool isArmoryEquipped = false;
        public bool isClashedOnce = false;
        public bool isHousingBought = false;
        public bool isHousingTutorialEnd = false;
        public bool isCardGameActivated = false;
        public bool isCardGamePlayedOnce = false;
        public bool isTutorialPlayed = false;
        #endregion
        public float themeVolume=0.5f;
        public float sfxVolume=0.5f;
        public float sliderValue;
        public string currentLevelName="1";
        public string playingLevelName;
        public int localID = 0;
        public playerSaveInfos playerInfos = new playerSaveInfos();
        public List<levelSaveInfos> levelInfos = new List<levelSaveInfos>();
        public class playerSaveInfos
        {
            public string playerName;
            public float playerBaseAttack=0;
            public float playerAttack;
            public float playerCurrentAttack;
            public float playerBaseHealth=100;
            public float playerHealth;
            public float playerCurrentHealth;
            public float playerBaseShield=0;
            public float playerShiled;
            public float playerCurrentShield;
            public Title title;
            public int playerGoldCount=100;
            public int earnedGold;
            public int expPoint;
            public string equippedFood;
            public string equippedSword;
            public string equippedArmor;
            public string equippedShield;
            public string equippedHousing;
            public string equippedNation;
            public string equippedHair;
            public string equippedVehicle;
            public List<string> BoughtfoodList = new List<string>();
            public List<string> BoughtswordList = new List<string>();
            public List<string> BoughtshieldList = new List<string>();
            public List<string> BoughthousingList = new List<string>();
            public List<string> BoughtvehicleList = new List<string>();
            public List<string> BoughtarmorList = new List<string>();
            public enum Title
            {
                Peasant,
                Fighter,
                Knight,
                Paladin,
                Lord,
                King
            }
        }
        [System.Serializable]
        public class levelSaveInfos
        {
            public string levelName;
            public bool isLock = true;
            public bool isBossDead;
            public int earnedstarCount;
            public int requiredPlayerExp;
            Vehicle.transportType requiredVehicleType = new Vehicle.transportType();
            Vehicle.Spacialty requireVehicleSpaciality = new Vehicle.Spacialty();
            Housing.Status requiredHousingType = new Housing.Status();
            public bool expRequires;
            public bool HousingRequires;
            public bool VehicleRequires;

        }
    }

}

