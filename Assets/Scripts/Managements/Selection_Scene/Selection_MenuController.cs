using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Components;
using TMPro;
using System.Text.RegularExpressions;
using RK.Animations.panelTransitions;
public class Selection_MenuController : MonoBehaviour
{
    [Header("Animation Objects")]
    [SerializeField] GameObject loadingPanel;
    [SerializeField] GameObject panelDownPos;
    [SerializeField] GameObject panelUpsPos;
    [Header("Panels & Objects")]
    [SerializeField] public GameObject canvas;
    [SerializeField] GameObject registorPanel;
    [SerializeField] GameObject nationaltyPanel;
    [SerializeField] GameObject appearancePanel;
    [Header("Select Nation Variables")]
    [SerializeField] Image flagIamge;
    List<Nation> nationList = new List<Nation>();
    [SerializeField] Button nationBackBtn;
    //add a class variable:
    [SerializeField] LocalizeStringEvent nation_localizedStringEvent;
    int nationIndex = 0;
    [Header("Register Name Variables")]
    [SerializeField] LocalizeStringEvent player_localizedStringEvent;
    [SerializeField] TMP_InputField nameField;
    [SerializeField] TMP_Text warningText;
    string _playerName;
    [Header("Appearence Variables")]
    [SerializeField] Image appearanceMirrorImage;
    [SerializeField] Image appearanceCharImage;
    [SerializeField]List<AppearenceVariables> appearences = new List<AppearenceVariables>();
    List<Hair> hair = new List<Hair>();
    int apperanceIndex = 0;
    [SerializeField] Button appearanceBackBtn;
    [SerializeField] Button generalBackButton;
    // Start is called before the first frame update
    void Start()
    {
        fillApperances();
        activateDeactivateBackButton(false);
        closeAppearancePanel();
        //closeNationPanel();
        generalBackButton.interactable = false;
        activateDeactivateBackButton(false);
        nationaltyPanel.SetActive(false);
        closeRegisterPanel();
        nationList.AddRange(ObjectsLists.nations);
        hair.AddRange(ObjectsLists.hair);
        openUpFirstPanel();
    }
    public void fillApperances()
    {
        appearences = new List<AppearenceVariables>(ObjectsLists.hair.Count);
        foreach (var item in ObjectsLists.hair)
        {
            AppearenceVariables _hair = new AppearenceVariables();
            _hair._name = item._name;
            _hair.sprite = item.sprite;
            appearences.Add(_hair);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    #region Handle transitions
    public void openUpFirstPanel()
    {
        if (SaveLoad_Manager.gameData.isApeperanceSet)
        {
            openRegisterPanel();
        }
        else
        {
            tutorialController.TInstance.registerPanelTutorial(this.gameObject);
        }
        
    }
    public void openUpSeceondPanel()
    {

        //closeRegisterPanel();
        openNationPanel();
    }
    public void openUpThirdPanel()
    {
        //closeNationPanel();
        openAppearancePanel();
    }
    #endregion
    #region Panel Methods

    public void openRegisterPanel()
    {
        Debug.Log("called");
        activateDeactivateBackButton(false);
        registorPanel.SetActive(true);
        panelTransitions.moveObj(registorPanel.transform.GetChild(0).gameObject, Vector3.zero, 1f,0f);
    }
    public void closeRegisterPanel()
    {
        registorPanel.SetActive(false);
    }

    public void openNationPanel()
    {
        
        generalBackButton.interactable = false;
        //nationBackBtn.interactable = true;
        generalBackButton.onClick.AddListener(() => nationBack());
        setNationChanges();
        nationaltyPanel.SetActive(true);
        panelTransitions.moveObjAndClosePanel(registorPanel.transform.GetChild(0).gameObject,panelDownPos.transform.position,1f, registorPanel.transform.GetChild(0).gameObject, nationaltyPanel.transform.GetChild(0).gameObject);
        Invoke("setInteractableBackButton", 1f);
    }
    public void closeNationPanel()
    {
        generalBackButton.interactable = false;
        activateDeactivateBackButton(false);
        panelTransitions.moveObjAndClosePanel(nationaltyPanel.transform.GetChild(0).gameObject, panelUpsPos.transform.position, 1f, nationaltyPanel.transform.GetChild(0).gameObject, registorPanel.transform.GetChild(0).gameObject);
        nationaltyPanel.SetActive(false);
    }
    public void openAppearancePanel()
    {
        appearanceBackBtn.interactable = true;
        setAppeareanceChanges();
        loadingPanel.SetActive(true);
        panelTransitions.changeAlphaAndPanels(loadingPanel.GetComponent<Image>(),1f,1f,LeanTweenType.linear,nationaltyPanel,appearancePanel);
       
        //appearancePanel.SetActive(true);
    }
    public void closeAppearancePanel()
    {
        appearancePanel.SetActive(false);
    }

    #endregion
    #region Button Methods
    public void incrementNationIndex() 
    {
        if (nationIndex < nationList.Count - 1)
            nationIndex++;
        else
            nationIndex = 0;
        setNationChanges();
    }
    public void decrementNationIndex()
    {
        if (nationIndex > 0)
            nationIndex--;
        else
            nationIndex = nationList.Count - 1;
        setNationChanges();
    }
    public void setNationChanges()
    {
        changeNationSprite();
        changeNationText();
    }
    public int getNationIndex()
    {
        return nationIndex;
    }
    public string getCurrentNation()
    {
        return nationList[nationIndex]._name;
    }
    public void changeNationSprite()
    {
        flagIamge.sprite = nationList[nationIndex].sprite;
    }
    public void changeNationText()
    {
        string key = getCurrentNation() + "_Text";
        nation_localizedStringEvent.StringReference.SetReference("uý_Texts_Start_and_Selection_Scenes", key);
    }
    public void selectNation()
    {
        SaveLoad_Manager.gameData.playerInfos.equippedNation = getCurrentNation();
        //Close Nation
        openUpThirdPanel();
    }
    public void registerName()
    {
        SoundManager.SInstance.playSfx(SoundManager.SInstance.buttonSFX);
        _playerName = nameField.text;
        //Debug.Log("Player Name" + _playerName);
        if (_playerName.Length <= 0)
        {
            player_localizedStringEvent.StringReference.SetReference("Warning_texts_table", "registerName_inputField_empty_warning");
        }
        else if(_playerName.Length < 2 || _playerName.Length > 15)
        {
            player_localizedStringEvent.StringReference.SetReference("Warning_texts_table", "registerName_inputField_lenght_warning");
        }
        else
        {
            warningText.text = "";
            SaveLoad_Manager.gameData.playerInfos.playerName = _playerName;
            //Close Name
            openUpSeceondPanel();
        }
    

    }
    public void CleanInput()
    {
        // Replace invalid characters with empty strings.
        try
        {
            string clearInput = nameField.text;
            nameField.text = Regex.Replace(clearInput, @"[^\w\.@-]", "",
                                 RegexOptions.None, System.TimeSpan.FromSeconds(1.5));
        }
        catch
        {
            Debug.Log("String Empty");
        }
    }
    public void incrementApearanceIndex()
    {
        if (apperanceIndex < appearences.Count - 1)
            apperanceIndex++;
        else
            apperanceIndex = 0;
        setAppeareanceChanges();
    }
    public void decrementApearanceIndex()
    {
        if (apperanceIndex > 0)
            apperanceIndex--;
        else
            apperanceIndex = appearences.Count - 1;
        setAppeareanceChanges();
    }
    void setAppeareanceChanges()
    {
        appearanceMirrorImage.sprite = appearences[apperanceIndex].sprite;
        appearanceCharImage.sprite = appearences[apperanceIndex].sprite;
    }
    public void activateDeactivateBackButton(bool state)
    {
        generalBackButton.gameObject.SetActive(state);
    }
    public void setInteractableBackButton()
    {
        activateDeactivateBackButton(true);
        generalBackButton.interactable = true;
    }
    public void selectApearances()
    {
        Hair selectedHair = hair.Find(x => x._name == appearences[apperanceIndex]._name);
        SaveLoad_Manager.gameData.playerInfos.equippedHair = selectedHair._name;
        //Close Appearances
        SaveLoad_Manager.gameData.isFirstPlay = true;
        SaveLoad_Manager.gameData.isApeperanceSet=true;
        SaveLoad_Manager.SInstance.saveJson();
        sceneManager.currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        sceneManager.sceneToLoad = "main_Scene";
        sceneManager.loadScene("loadingScene");
        //UnityEngine.SceneManagement.SceneManager.LoadScene("main_Scene");
    }
    public void nationBack()
    {
        SoundManager.SInstance.playSfx(SoundManager.SInstance.buttonSFX);
        activateDeactivateBackButton(false);
        //nationBackBtn.interactable = false;
        panelTransitions.moveObjAndClosePanel(nationaltyPanel.transform.GetChild(0).gameObject, panelUpsPos.transform.position, 1f, nationaltyPanel.transform.GetChild(0).gameObject, registorPanel.transform.GetChild(0).gameObject);
    }
    public void appearanceBack()
    {
        
        appearanceBackBtn.interactable = false;
        loadingPanel.SetActive(true);
        panelTransitions.changeAlphaAndPanels(loadingPanel.GetComponent<Image>(), 1f, 1f, LeanTweenType.linear, appearancePanel, nationaltyPanel);
    }
    #endregion
    [System.Serializable]
    public struct AppearenceVariables
    {
        public string _name;
        public Sprite sprite;
    }
}
