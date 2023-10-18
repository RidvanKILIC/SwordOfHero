using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RK.GameDatas;
using RK.Security;
using Newtonsoft.Json;
using System.IO;
using RK.GameDatas.levelItems;
public class SaveLoad_Manager:MonoBehaviour
    {
    public static GameDatas gameData;
        private static SaveLoad_Manager instance;
        public static SaveLoad_Manager SInstance
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
        // Start is called before the first frame update
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
        //Application.targetFrameRate = 60;
        }
        /// <summary>
        /// Covert levelinfolist content to a json string and saves it to the given path
        /// </summary>
        public void saveJson()
        {
        //Debug.Log("Json Saving");
        var json = JsonConvert.SerializeObject(gameData);
        //json = Cryptography.EnCrypt(json, "^1662QUef%9b");
        File.WriteAllText(Application.persistentDataPath + "gameInfo.json", json);
        readJson();
        }
        /// <summary>
        /// Reads the json file from the given path if exist if not creates new one and turn the read file into a levelInfos object then assign it to the levelinfo list
        /// </summary>
        public void readJson()
        {
            gameData = new GameDatas();
            var json = "";
            if (File.Exists(Application.persistentDataPath + "gameInfo.json"))
            {
               //Debug.Log("Json Exist");
                json = File.ReadAllText(Application.persistentDataPath + "gameInfo.json");
                //json = Cryptography.DeCrypt(json, "^1662QUef%9b");
                gameData = JsonConvert.DeserializeObject<GameDatas>(json);
                LevelList.LInstance.InitList();
            }
            else
            {
                //Debug.Log("Json Not Exsist");
                saveJson();
            }
        }
        /// <summary>
        /// Deletes json file and calls saveJson to create new one
        /// </summary>
        public void deleteJson()
        {
            //Debug.Log("Json Not Deleting");
            File.Delete(Application.persistentDataPath + "gameInfo.json");
            LevelList.LInstance.resetList();
            if (!File.Exists(Application.persistentDataPath + "gameInfo.json"))
            {
                gameData = new GameDatas();
                gameData.isFirstPlay = true;
                saveJson();
            }
        }
    }
