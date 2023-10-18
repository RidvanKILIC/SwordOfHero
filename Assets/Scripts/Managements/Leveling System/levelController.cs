using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RK.GameDatas.levelItems;
public class levelController : MonoBehaviour
{
    [SerializeField] public List<GameObject> levelObjects = new List<GameObject>();
    [SerializeField] levelMapSpline levelPaths;
    [SerializeField] string currentLevelName;
    [SerializeField] string nextLevelName;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        setPlayerBage();
        levelPaths.initPaths();
        levelPaths.setSelectedLevel(SaveLoad_Manager.gameData.currentLevelName);
        setLevelObjects();
        //levelPaths.setNextLevel(nextLevelName);
        

    }
    void setPlayerBage()
    {
        levelPaths.setPlayer();
    }
    public void setLevelObjects()
    {
        foreach(var levelItem in levelObjects)
        {
            //Debug.Log("Level Object Name: " + levelItem.GetComponent<levelItem>().levelName);
            var matchedLevel = LevelList.LInstance.levelList.Find(x => x.levelName == levelItem.GetComponent<levelItem>().levelName);
            if (matchedLevel != null)
            {
                //Debug.Log("matched: " + matchedLevel.levelName);
                levelItem.GetComponent<levelItem>().initItem(matchedLevel);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
