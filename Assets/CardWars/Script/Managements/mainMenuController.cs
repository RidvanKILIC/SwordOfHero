using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class mainMenuController : MonoBehaviour
{
    #region Variables
    [Header("Difficulty Variables")]
    bool openCloseDifficulty = true;
    [SerializeField] GameObject difficultyPanel;
    [SerializeField] TMPro.TMP_Text difficultyText;
    [SerializeField] gameInfos.difficulty menuselectedDifficulty;
    //[SerializeField] List<gameInfos.decks> gameModes = new List<gameInfos.decks>();
    [Header("Panels & GameObjects ")]
    [SerializeField] GameObject loadingPanel;
    [Header("References")]
    createDecks _createDecks;
    #endregion
    // Start is called before the first frame update
    void Start()
    {

        Time.timeScale = 1;

        setDifficultyText(_createDecks.getSelectedDifficulty().ToString());

        _createDecks = GameObject.FindObjectOfType<createDecks>();
        activateDeactivateLoadingPanel(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void difficultyButton()
    {
        SoundManager.SInstance.playSfx(cardSounds.SInstance.buttonSFX);
        difficultyPanel.gameObject.GetComponent<Animator>().SetBool("openCloseMenu", openCloseDifficulty);
        openCloseDifficulty=!openCloseDifficulty;
    }
    public void difficultySelectionButton(GameObject _btn)
    {
        SoundManager.SInstance.playSfx(cardSounds.SInstance.buttonSFX);
        gameInfos.difficulty _selectedDifficulty;
        System.Enum.TryParse(_btn.gameObject.name, out _selectedDifficulty);
        Debug.Log(_selectedDifficulty);
        menuselectedDifficulty = _selectedDifficulty;
        setDifficultyText(_btn.gameObject.name);
        difficultyPanel.gameObject.GetComponent<Animator>().SetBool("openCloseMenu", false);
        openCloseDifficulty = true;
    }
    public void setDifficultyText(string _text)
    {
        difficultyText.text = "Difficulty " + "(" + _text + ")";
    }
    public void startButton(GameObject _btn)
    {
        SoundManager.SInstance.playSfx(cardSounds.SInstance.buttonSFX);
        _btn.GetComponent<UnityEngine.UI.Button>().interactable = false;
        gameInfos.decks choosenDeck;
        choosenDeck = _createDecks.getGameModesList().Find(x => x.gameDifficulty.Equals(menuselectedDifficulty));
        if (choosenDeck.deckSize > 0)
        {
            _createDecks.createRandonDeck(choosenDeck);
            //_sceneManagment.sceneManager.loadStartScene();
        }
        else
        {
            _btn.GetComponent<UnityEngine.UI.Button>().interactable = true;
            Debug.Log("Something wrong");
        }
          
    }
    public void stopStartIconAnim(bool _state)
    {
        loadingPanel.transform.Find("Icon").GetComponent<Animator>().SetBool("startTimer", _state);
    }
    public void quitButton()
    {
        SoundManager.SInstance.playSfx(cardSounds.SInstance.buttonSFX);
        sceneManager.exitGame();
    }
    public void activateDeactivateLoadingPanel(bool _state)
    {
        loadingPanel.SetActive(_state);
    }
}
