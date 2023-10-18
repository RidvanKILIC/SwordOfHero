using RK.GameDatas;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class levelItem : MonoBehaviour
{
    [SerializeField] public string levelName; 
    [SerializeField] Image BossImageSlot;
    [SerializeField] public Sprite BGSprite;
    [SerializeField] GameObject deathObject;
    [SerializeField] GameObject LockObject;
    [SerializeField] bool isLock = true;
    [SerializeField] bool isDeath = true;
    [SerializeField] List<GameObject> starList = new List<GameObject>();
    [SerializeField] public List<GameObject> enemyList = new List<GameObject>();
    [SerializeField] public GameObject enemyBoss;
    [SerializeField] int earnedStar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void initItem(GameDatas.levelSaveInfos matchedItem)
    {
        isLock = matchedItem.isLock;
        isDeath = matchedItem.isBossDead;
        setBossImage(BGSprite);
        activeDeactiveDeath(isDeath);
        activeDeactiveLock(isLock);
        //activeDeactiveStars(earnedStar,true);
    }
    public void setBossImage(Sprite _boss)
    {
        BossImageSlot.overrideSprite = _boss;
    }
    public void setIslock(bool _state)
    {
        isLock = _state;
    }
    public bool getIsLock()
    {
        return isLock;
    }
    public void setIsDeath(bool _state)
    {
        isDeath = _state;
    }
    public bool getIsDeath()
    {
        return isDeath;
    }
    public void setEarnedStar(int _star)
    {
        earnedStar = _star;
    }
    public int getEarnedStar()
    {
        return earnedStar;
    }
    void activeDeactiveDeath(bool _state)
    {
        deathObject.SetActive(_state);
    }
    void activeDeactiveLock(bool _state)
    {
        LockObject.SetActive(_state);
    }
    void activeDeactiveStars(int starCount,bool _state)
    {
        for(int i = 0; i < starCount; i++)
        {
            starList[i].SetActive(_state);
        }
    }
}
