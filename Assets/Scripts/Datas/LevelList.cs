using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RK.GameDatas;
namespace RK.GameDatas.levelItems
{
    public class LevelList : MonoBehaviour
    {
        public string currentLevelItem;
        public List<GameObject> levelObjects = new List<GameObject>();
        public  List<GameDatas.levelSaveInfos> levelList = new List<GameDatas.levelSaveInfos>();
        private static LevelList instance;
        public static LevelList LInstance
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
        }
        public void InitList()
        {
            if (SaveLoad_Manager.gameData.levelInfos.Count > 0)
            {
                for(int i=0;i< SaveLoad_Manager.gameData.levelInfos.Count; i++)
                {
                    if (levelList[i] != null)
                        levelList[i] = SaveLoad_Manager.gameData.levelInfos[i];
                    else
                        levelList.Add(SaveLoad_Manager.gameData.levelInfos[i]);

                }
            }
        }
        public void writeList()
        {
            SaveLoad_Manager.gameData.levelInfos.Clear();
            SaveLoad_Manager.gameData.levelInfos = levelList;
        }
        public void resetList()
        {
            foreach(var item in levelList)
            {
                if (item.levelName == "1")
                {
                    item.isLock = false;
                }
                else
                {
                    item.isLock = true;
                }
                item.isBossDead = false;
            }
        }
        //public void setCurrentLevel(levelItem _level)
        //{
        //    //currentLevelItem = _level;
        //    currentLevelItem.BGSprite = _level.BGSprite;
        //    currentLevelItem.enemyList = _level.enemyList;
        //    currentLevelItem.enemyBoss = _level.enemyBoss;
        //}
    }
}

