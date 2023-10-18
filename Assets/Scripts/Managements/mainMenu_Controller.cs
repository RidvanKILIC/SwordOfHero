using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mainMenu_Controller : MonoBehaviour
{
    [Header("Tutorial Variables")]
    [SerializeField] public GameObject Canvas;
    [SerializeField] public List<GameObject> menuToggles = new List<GameObject>();
    [SerializeField] public List<GameObject> weaponaryToggles = new List<GameObject>();
    [SerializeField] public GameObject weaponaryArrowPos;
    [SerializeField] public GameObject housingHomeArrowPos;
    [SerializeField] public GameObject HomeArrowPos;
    [SerializeField] public GameObject shieldArrowPos;
    [SerializeField] public GameObject armorArrowPos;
    [SerializeField] public GameObject mapArrowPos;
    [SerializeField] public GameObject housingArrowPos;
    [SerializeField] public GameObject tavernArrowPos;
    [Header("Mainmenu Variables")]
    [SerializeField] GameObject player_HUD;
    [SerializeField] GameObject tavernButton;
    [SerializeField] GameObject marketWarningPanel;
    [SerializeField] GameObject hausingWarningPanel;
    [SerializeField] public GameObject player_OBJ;
    [SerializeField] GameObject player_Prefab;
    [SerializeField] public GameObject playerSpawnPos;
    [SerializeField] Image BGImage;
    [SerializeField] Sprite BaseBG;
    [SerializeField] CollectGoldAnim _collectGold;
    // Start is called before the first frame update
    void Start()
    {
        InitPlayerHUD();
        updateHousingBG();
        updatePlayer();
        playIdleAnim();
        initHousing();
        if (SaveLoad_Manager.gameData.playerInfos.earnedGold > 0)
        {
            //Debug.Log("Earned Gold: " + SaveLoad_Manager.gameData.playerInfos.earnedGold);
            _collectGold.collectGolds();
        }
           
        checkTutorial();
        
    }
    public void checkTutorial()
    {
        if (!SaveLoad_Manager.gameData.isArmoryEquipped)
        {
            tutorialController.TInstance.mainMenuArmoryTutorial(this.gameObject);
        }
        else if (SaveLoad_Manager.gameData.isArmoryEquipped && !SaveLoad_Manager.gameData.isClashedOnce)
        {
            tutorialController.TInstance.mainMenuFirstClashTutorial(GameObject.FindGameObjectWithTag("Managements").gameObject);
        }
        else if (SaveLoad_Manager.gameData.isArmoryEquipped && SaveLoad_Manager.gameData.isArmoryEquipped && SaveLoad_Manager.gameData.isClashedOnce && !SaveLoad_Manager.gameData.isHousingBought)
        {
            tutorialController.TInstance.mainMenuHousingTutorial(GameObject.FindGameObjectWithTag("Managements").gameObject);
        }
        else if (SaveLoad_Manager.gameData.isArmoryEquipped && SaveLoad_Manager.gameData.isArmoryEquipped && SaveLoad_Manager.gameData.isClashedOnce && SaveLoad_Manager.gameData.isHousingBought && !SaveLoad_Manager.gameData.isCardGameActivated)
        {
            tutorialController.TInstance.mainMenuActivateCardTutorial(GameObject.FindGameObjectWithTag("Managements").gameObject);
        }
        if (SaveLoad_Manager.gameData.isCardGameActivated)
        {
            openTavern();
        }

        
    }
    public void activateMArketWarningPanel()
    {
        SoundManager.SInstance.playSfx(SoundManager.SInstance.deniedSFX);
        marketWarningPanel.SetActive(true);
    }
    public void deactivateMArketWarningPanel()
    {
        marketWarningPanel.SetActive(false);
    }
    public void activateHousingWarningPanel()
    {
        hausingWarningPanel.SetActive(true);
    }
    public void deactivateHousingWarningPanel()
    {
        hausingWarningPanel.SetActive(false);
    }
    public void updateHousingBG()
    {
        if (!string.IsNullOrEmpty(SaveLoad_Manager.gameData.playerInfos.equippedHousing))
        {
            Housing currentHousing = ObjectsLists.housings.Find(x => x._name == SaveLoad_Manager.gameData.playerInfos.equippedHousing);
            if(currentHousing != null)
            {
                BGImage.overrideSprite = currentHousing.BG;
            }
            else
            {
                BGImage.overrideSprite = BaseBG;
            }
        }
        else
        {
            BGImage.overrideSprite = BaseBG;
        }
    }
    public void updatePlayer()
    {
        if(player_OBJ!=null)
            Destroy(player_OBJ.gameObject);


        GameObject obj = Instantiate(player_Prefab, playerSpawnPos.transform.position, Quaternion.identity);
        obj.transform.SetParent(playerSpawnPos.transform.parent.transform);
        obj.transform.localScale = Vector3.one;
        player_OBJ = obj;
        InitPlayerOBJ();
    }
    public void initHousing()
    {
        if (!string.IsNullOrEmpty(SaveLoad_Manager.gameData.playerInfos.equippedHousing))
        {
            Housing currentHousing = ObjectsLists.housings.Find(x => x._name == SaveLoad_Manager.gameData.playerInfos.equippedHousing);
            if (currentHousing != null)
            {
                BGImage.overrideSprite = currentHousing.BG;
            }
            else
            {
                BGImage.overrideSprite = BaseBG;
            }
        }
        else
        {
            BGImage.overrideSprite = BaseBG;
        }

    }
    public void InitPlayerHUD()
    {
        player_HUD.GetComponent<PlayerHUD_Controller>().Init();
    }
    public void InitPlayerOBJ()
    {
        player_OBJ.GetComponent<PlayerSkinController>().Init();
        playIdleAnim();
    }
    public void updatePlayerWeapon()
    {
        player_OBJ.GetComponent<PlayerSkinController>().setWeaponSkin();
    }
    public void updatePlayerShield()
    {
        player_OBJ.GetComponent<PlayerSkinController>().setShieldSkin();
    }
    public void updatePlayerArmor()
    {
        player_OBJ.GetComponent<PlayerSkinController>().setArmorSkin();
    }
    public void updateShieldValue()
    {
        player_HUD.GetComponent<PlayerHUD_Controller>().updateShield(0);
    }
    public void updateGold()
    {
        player_HUD.GetComponent<PlayerHUD_Controller>().updateGold();
    }
    public void playIdleAnim()
    {
        player_OBJ.GetComponent<PlayerAnimationController>().playBodyAnim(player_OBJ.GetComponent<PlayerSkinController>().playerParts,"Defaults/Default_Default_Idle",true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void goToTavern()
    {
        if (!SaveLoad_Manager.gameData.isCardGameActivated)
        {
            SaveLoad_Manager.gameData.isCardGameActivated = true;
            tutorialController.TInstance.endMainMenuActivateCardTutorialTutorial(GameObject.FindGameObjectWithTag("Managements").gameObject);
            SaveLoad_Manager.SInstance.saveJson();
        }
        SoundManager.SInstance.playSfx(SoundManager.SInstance.buttonSFX);
        sceneManager.currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        sceneManager.sceneToLoad = "cardGame_Scene";
        sceneManager.loadScene("loadingScene");
    }
    public void openTavern()
    {
        //Debug.Log("Open Tavern");
        tavernButton.gameObject.SetActive(true);
        tavernButton.GetComponent<Animator>().SetTrigger("OpenCardGame");
    }
    public void closeTaven()
    {
        tavernButton.gameObject.SetActive(false);
    }

}
