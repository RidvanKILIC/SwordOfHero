using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using RK.GameDatas.levelItems;
public class MiniGame_Manager : MonoBehaviour
{
    [Header("Screen Variables")]
    [SerializeField]public GameObject Canvas;
    [SerializeField]public GameObject arrowPos;
    [SerializeField] Image BgImage;
    [SerializeField] GameObject warningPopUP;
    [Header("Player Variables")]
    [SerializeField] float playerAttackRate;
    [SerializeField] public GameObject currentPlayer;
    [SerializeField] GameObject playerSpawnPos;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject player;
    [SerializeField]public GameObject playerSpacial_1;
    [SerializeField] GameObject playerSpacial_2;
    [SerializeField] int earnedGold;
    [SerializeField] int earnedXP;
    [Header("Enemy Variables")]
    [SerializeField] float enemyAttackRate;
    [SerializeField] List<GameObject> EnemyList = new List<GameObject>();
    [SerializeField]List<GameObject> enemiesToDestroy = new List<GameObject>();
    [SerializeField] GameObject bossEnemy;
    [SerializeField]public GameObject currentEnemy;
    [SerializeField] GameObject pastEnemy=null;
    [SerializeField] int minienemyCount;
    [SerializeField] GameObject enemySpawnPos;
    [SerializeField] public GameObject enemyArenaPos;
    [SerializeField] GameObject dropGoldItem;
    [SerializeField] GameObject dropGemItem;
    [Header("Other Variables")]
    [SerializeField] GameObject scoreBoard;
    [SerializeField] List<GameObject> playerList = new List<GameObject>();
    [SerializeField] TMP_Text goldText;
    bool bossSpawned;
    int minionKiled=0;
    int currentTurn=0;
    int roadToBoss = 0;
    public bool isGameOver = false;

    private void Start()
    {
        //SoundManager.SInstance.adjustTheme();
        var currentLevel = LevelList.LInstance.levelObjects.Find(x => x.GetComponent<levelItem>().levelName == LevelList.LInstance.currentLevelItem);
        if (currentLevel != null)
        {
            EnemyList = currentLevel.GetComponent<levelItem>().enemyList;
            bossEnemy = currentLevel.GetComponent<levelItem>().enemyBoss;
            BgImage.overrideSprite = currentLevel.GetComponent<levelItem>().BGSprite;
        }
        else
        {
            Debug.Log("null");
        }
        goldText.text = SaveLoad_Manager.gameData.playerInfos.playerGoldCount.ToString();
        scoreBoard.SetActive(false);
        minionKiled = 0;
        roadToBoss = 0;
        playerSpacial_1.GetComponent<playerSpacial_Controller>().setSpacialNeedGemCount(10);
        minienemyCount = EnemyList.Count;
        spawnPlayer();
        Init();
    }
    public void unlockNextLevel()
    {
        var currentLevel = LevelList.LInstance.levelObjects.Find(x => x.GetComponent<levelItem>().levelName == LevelList.LInstance.currentLevelItem);
        if (currentLevel != null)
        {
            //Debug.Log(currentLevel.GetComponent<levelItem>().levelName);
            int currentIndex = LevelList.LInstance.levelList.FindIndex(x=>x.levelName==currentLevel.GetComponent<levelItem>().levelName);
            if (currentIndex >= 0 && currentIndex < LevelList.LInstance.levelList.Count)
            {
                LevelList.LInstance.levelList[currentIndex].isBossDead = true;
                SaveLoad_Manager.gameData.currentLevelName = currentLevel.GetComponent<levelItem>().levelName;
                //Debug.Log(LevelList.LInstance.levelList[currentIndex + 1].levelName);
                if(currentIndex+1 < LevelList.LInstance.levelList.Count)
                    LevelList.LInstance.levelList[currentIndex+1].isLock = false;
                LevelList.LInstance.writeList();
                SaveLoad_Manager.SInstance.saveJson();
            }
                
        }
    }
    void Init()
    {
        //Debug.Log(minienemyCount);
        //spawnPlayer();
        Invoke("spawnEnemy",1); /*spawnEnemy();*/
        setSpacialButton();
        UIManager.UInstance.setProgressSliderMax(minienemyCount+1);
        startAttack();
    }
    public void dropPlayerAttackRate(float _value)
    {
        playerAttackRate -= _value;
    }
    public void increasePlayerAttackRate(float _value)
    {
        playerAttackRate += _value;
    }
    public void refreshPlayerList()
    {
        playerList.Clear();
        playerList.Add(currentPlayer);
        playerList.Add(currentEnemy);
    }
    public void updateEarnedGold(int _value)
    {
        earnedGold += _value;
    }
    public void updateEarnedXp(int _value)
    {
        earnedXP += _value;
    }
    public void spawnPlayer()
    {
        //GameObject player = Instantiate(playerPrefab, playerSpawnPos.transform.position, Quaternion.identity);
        player.name = playerPrefab.name;
        player.transform.position = playerSpawnPos.transform.position;
        player.transform.SetParent(playerSpawnPos.transform.root);
        player.transform.localScale = Vector3.one;
        player.GetComponent<PlayerSkinController>().Init();
        player.GetComponent<Player>().Init();      
        //player.SetActive(true);
        currentPlayer = player;

    }
    public void spawnEnemy()
    {
        
        GameObject _enemy= null;
        currentEnemy = null;
        if (minienemyCount > 0)
        {
            _enemy = Instantiate(EnemyList[minienemyCount-1], enemySpawnPos.transform.position, Quaternion.identity);
            currentEnemy = _enemy;
            _enemy.name = EnemyList[minienemyCount-1].name+ (minienemyCount - 1).ToString();
            _enemy.transform.SetParent(enemySpawnPos.transform.root);
            _enemy.transform.localScale = Vector3.one;
            currentEnemy.gameObject.GetComponent<EnemyContainer>().init();
            currentEnemy.gameObject.GetComponent<EnemyContainer>()._enemyScriptable.target = currentPlayer.gameObject;
            currentEnemy.gameObject.GetComponent<EnemyContainer>().EnterArena(enemyArenaPos.transform);
            minienemyCount--;
            UIManager.UInstance.setProgressSliderValue(roadToBoss);
            roadToBoss++;
        }
        else if (!bossSpawned)
        {

            _enemy = Instantiate(bossEnemy, enemySpawnPos.transform.position, Quaternion.identity);
            currentEnemy = _enemy;
            _enemy.name = bossEnemy.name;
            _enemy.transform.SetParent(enemySpawnPos.transform.root);
            _enemy.transform.localScale = Vector3.one;
            bossSpawned = true;
            currentEnemy.gameObject.GetComponent<EnemyContainer>().init();
            currentEnemy.gameObject.GetComponent<EnemyContainer>()._enemyScriptable.target = currentPlayer.gameObject;
            currentEnemy.GetComponent<EnemyContainer>().EnterArena(enemyArenaPos.transform);
            UIManager.UInstance.setProgressSliderValue(roadToBoss);
            //roadToBoss++;
        }
        else
        {
            isGameOver = true;
            gameOver();
            //gameOver();
            //Debug.Log("Game Over");
        }
        if (currentEnemy != null)
        {
            refreshPlayerList();
            eraseEnemies();
            enemiesToDestroy.Add(_enemy);
        }
        
        //Destroy(pastEnemy.gameObject,1f);
    }
    public void eraseEnemies()
    {
        if (enemiesToDestroy.Count > 0)
        {
            foreach(var item in enemiesToDestroy)
            {
              Destroy(item.gameObject, 1f);       
            }
            enemiesToDestroy.Clear();
        }
    }
    public void setEnemyDeath(string _name)
    {
        refreshPlayerList();
        eraseEnemies();
        currentEnemy.gameObject.GetComponent<EnemyContainer>().isDead = true;
        dropExp(Random.Range(currentEnemy.gameObject.GetComponent<EnemyContainer>()._enemyScriptable.dropBottomExp, currentEnemy.gameObject.GetComponent<EnemyContainer>()._enemyScriptable.dropTopExp));
        dropGold(Random.Range(currentEnemy.gameObject.GetComponent<EnemyContainer>()._enemyScriptable.dropBottomGold, currentEnemy.gameObject.GetComponent<EnemyContainer>()._enemyScriptable.dropTopGold));
        dropGem(Random.Range(currentEnemy.gameObject.GetComponent<EnemyContainer>()._enemyScriptable.dropBottomGem, currentEnemy.gameObject.GetComponent<EnemyContainer>()._enemyScriptable.dropTopGem));
        if(currentEnemy.GetComponent<EnemyContainer>()._enemyScriptable.enemyType.Equals(EnemyType.minion))
            minionKiled++;

        Invoke("spawnEnemy", 1);
        //spawnEnemy();
        //if (bossEnemy.gameObject.name == _name)
        //{
        //    var enemy = EnemyList.Find(x => x.gameObject.name == _name);
        //    if (enemy != null)
        //    {
        //        //isGameOver = true;
        //        enemy.GetComponent<EnemyContainer>().isDead = true;
        //        dropExp(enemy.GetComponent<EnemyContainer>()._enemyScriptable.dropExp);
        //        dropGold(enemy.GetComponent<EnemyContainer>()._enemyScriptable.dropGold);
        //        dropGem(enemy.GetComponent<EnemyContainer>()._enemyScriptable.dropGem);
        //        gameOver();
        //    }
               

        //}
        //else
        //{
        //    var enemy = EnemyList.Find(x => x.gameObject.name == _name);
        //    if (enemy != null)
        //    {
        //        enemy.GetComponent<EnemyContainer>().isDead = true;
        //        dropExp(enemy.GetComponent<EnemyContainer>()._enemyScriptable.dropExp);
        //        dropGold(enemy.GetComponent<EnemyContainer>()._enemyScriptable.dropGold);
        //        dropGem(enemy.GetComponent<EnemyContainer>()._enemyScriptable.dropGem);
        //    }
        //}
        
    }
    public void startAttack()
    {
        //if (!isGameOver)
        //{
        if (!SaveLoad_Manager.gameData.isClashedOnce)
        {
            tutorialController.TInstance.miniGameTutorial(GameObject.FindGameObjectWithTag("Managements").gameObject, true);
        }
        InvokeRepeating("playerAttack", 3, playerAttackRate);
            InvokeRepeating("EnemyAttack", 1, enemyAttackRate);
        //}

    }
    public void spacial1(float rate,bool startStop)
    {
        if (startStop)
            InvokeRepeating("playerSpeedUpAttack", 0, rate);
        else
            CancelInvoke("playerSpeedUpAttack");
    }
    public void playerSpeedUpAttack()
    {
        if (!isGameOver && !currentEnemy.GetComponent<EnemyContainer>().isDead && currentEnemy.GetComponent<EnemyContainer>().enterdArena && currentPlayer.GetComponent<Player>().spacialUsing)
            currentPlayer.gameObject.GetComponent<IDamagable>().IAttack();
    }
    public void playerAttack()
    {
        if(!isGameOver && !currentEnemy.GetComponent<EnemyContainer>().isDead && currentEnemy.GetComponent<EnemyContainer>().enterdArena && !currentPlayer.GetComponent<Player>().spacialUsing)
            currentPlayer.gameObject.GetComponent<IDamagable>().IAttack();
    }
    public void EnemyAttack()
    {
        if (currentEnemy != null)
        {
            if (!isGameOver && !currentPlayer.GetComponent<Player>().isDead && currentEnemy.GetComponent<EnemyContainer>().enterdArena)
                currentEnemy.gameObject.GetComponent<IDamagable>().IAttack();
        }

    }
    //public void changeTurn()
    //{
    //    //Debug.Log("Changing Turn Called");
    //    if (currentEnemy.gameObject.GetComponent<EnemyContainer>().isDead == false)
    //    {
    //        if (!isGameOver)
    //        {
    //            //Debug.Log("Changing Turn");
    //            playerList[currentTurn].gameObject.GetComponent<IDamagable>().IAttack();
    //            currentTurn++;
    //            if (currentTurn > playerList.Count - 1)
    //                currentTurn = 0;
    //        }
    //    }
    //    else
    //    {
    //        if (!isGameOver)
    //            spawnEnemy();
    //        else
    //            gameOver();
    //    }

    //}
    public async void dropGold(int gold)
    {
        for(int i = 0; i < gold;i++)
        {
            //Debug.Log("Drop Gold");
            GameObject _gold = Instantiate(dropGoldItem, new Vector2(currentEnemy.transform.position.x+Random.Range(1f,-1f),currentEnemy.transform.position.y), Quaternion.identity);
            _gold.transform.SetParent(currentEnemy.transform.parent);
            //_gold.transform.position = currentEnemy.transform.position;
            _gold.transform.localScale = Vector3.one;
            //_gold.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(0, 3), Random.Range(1, 5)), ForceMode2D.Impulse);
            collectGold(_gold);
        }

    }
    public async void dropGem(int Gem)
    {
        for(int i = 0; i < Gem; i++)
        {
            //Debug.Log("Drop Gem");
            GameObject _gem = Instantiate(dropGemItem, new Vector2(currentEnemy.transform.position.x + Random.Range(-1f, 1f), currentEnemy.transform.position.y), Quaternion.identity);
            _gem.transform.SetParent(currentEnemy.transform.parent);
            //_gem.transform.position = currentEnemy.transform.position;
            _gem.transform.localScale = Vector3.one;
            collectGem(_gem);
        }

    }
    public async void dropExp(int exp)
    {
        earnedXP += exp;
    }
    public async void collectGold(GameObject gold)
    {
        LeanTween.move(gold, currentPlayer.transform.Find("collectPoint").gameObject.transform.position, 1f).setDelay(0.2f).setEase(LeanTweenType.easeOutBack).setOnComplete(() => { increaseGold(); Destroy(gold); });

    }
    public async void collectGem(GameObject gem)
    {
        LeanTween.move(gem, currentPlayer.transform.Find("collectPoint").gameObject.transform.position, 1f).setDelay(0.2f).setEase(LeanTweenType.easeOutBack).setOnComplete(() => { increaseGem(); Destroy(gem);});
    }
    public void increaseGem()
    {
        playerSpacial_1.GetComponent<playerSpacial_Controller>().setSlidersValue(1);
    }
    public void increaseGold()
    {
        earnedGold++;
        //SaveLoad_Manager.gameData.playerInfos.playerGoldCount++;
        goldText.text = SaveLoad_Manager.gameData.playerInfos.playerGoldCount.ToString();
    }
    public void spacialUsed()
    {
        playerSpacial_1.GetComponent<playerSpacial_Controller>().setSpacialUsed();
    }
    public void setSpacialButton()
    {
        playerSpacial_1.GetComponent<playerSpacial_Controller>().getSpacialButton().onClick.AddListener(() => currentPlayer.GetComponent<Player>().spacial1Attack());
    }
    public void gameOver()
    {
        if (currentPlayer.GetComponent<Player>().isDead)
        {
            currentEnemy.GetComponent<EnemyContainer>().enemyWin();
        }
        else
        {
            unlockNextLevel();
            currentPlayer.GetComponent<Player>().playerWin();
        }
        Invoke("gameOverDelayed", 2);

    }
    public void gameOverDelayed()
    {
        if (!SaveLoad_Manager.gameData.isClashedOnce)
            SaveLoad_Manager.gameData.isClashedOnce = true;
        //Debug.Log("Game Over");
        //Time.timeScale = 0;
        if (currentPlayer.GetComponent<Player>().isDead)
        {
            //Debug.Log("Lose");
            scoreBoard.SetActive(true);
            scoreBoard.GetComponent<ScoreBoard>().gameOver(0, 0, 0);
        }
        else
        {
            int starCount = 0;
            if (bossEnemy.GetComponent<EnemyContainer>().isDead)
            {
               
                starCount = 3;
            }
            else if (minionKiled >= minienemyCount)
            {
                starCount = 2;
            }
            else
            {
                starCount = 1;
            }
            scoreBoard.SetActive(true);
            scoreBoard.GetComponent<ScoreBoard>().gameOver(starCount, earnedGold, earnedXP);
        }
    }
    public void leaveGame()
    {
        SoundManager.SInstance.playSfx(SoundManager.SInstance.buttonSFX);
        Time.timeScale = 1;
        isGameOver = true;
        sceneManager.currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        sceneManager.sceneToLoad = "main_Scene";
        sceneManager.loadScene("loadingScene");
    }
    public void activatePopUP()
    {
        isGameOver = true;
        SoundManager.SInstance.playSfx(SoundManager.SInstance.deniedSFX);
        warningPopUP.SetActive(true);
        Time.timeScale = 0;
    }
    public void deactivatePopUP()
    {
        SoundManager.SInstance.playSfx(SoundManager.SInstance.buttonSFX);
        isGameOver = false;
        warningPopUP.SetActive(false);
        Time.timeScale = 1;
    }

}
